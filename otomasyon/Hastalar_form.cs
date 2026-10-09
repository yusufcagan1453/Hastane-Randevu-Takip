using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;

namespace otomasyon
{
    public partial class Hastalar_form : Form
    {
        string connectionString;
        public Hastalar_form()
        {
            InitializeComponent();
            connectionString = ConfigurationManager.ConnectionStrings["randevutakipConnectionString"].ConnectionString;
        }

        private void button4_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            hastalariGoster();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                try
                {
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        using (SqlCommand cmd = new SqlCommand("SP_HASTALAR_DELETE", connection))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@ID", dataGridView1.SelectedRows[0].Cells[0].Value);
                            connection.Open();
                            cmd.ExecuteNonQuery();
                            MessageBox.Show("Hasta Silindi");
                            hastalariGoster();
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Hata = " + ex);
                }
            }
            else
            {
                MessageBox.Show("Lütfen silinecek hastayı seçiniz.");
            }
        }

        void hastalariGoster()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("SP_HASTALAR_SELECT", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        DataTable dt = new DataTable();
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        connection.Open();
                        da.Fill(dt);
                        dataGridView1.DataSource = dt;

                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata = " + ex.Message);
            }
        }

        private void Hastalar_form_Load(object sender, EventArgs e)
        {
            hastalariGoster();
            ComboLoadData();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string tcno = textBox1.Text;
            string ad = textBox2.Text;
            string soyad = textBox3.Text;
            int cinsiyetId = Convert.ToInt32(comboBox1.SelectedValue);
            DateTime dogumtarihi = dateTimePicker1.Value;
            string telefon = textBox4.Text;
            string eposta = textBox5.Text;
            int yas = Convert.ToInt32(textBox6.Text);

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("SP_HASTALAR_INSERT", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@TC_KIMLIK_NO", tcno);
                        cmd.Parameters.AddWithValue("@AD",ad);
                        cmd.Parameters.AddWithValue("@SOYAD",soyad);
                        cmd.Parameters.AddWithValue("@CINSIYET_ID",cinsiyetId);
                        cmd.Parameters.AddWithValue("@DOGUM_TARİHİ",dogumtarihi);
                        cmd.Parameters.AddWithValue("@TELEFON",telefon);
                        cmd.Parameters.AddWithValue("@EPOSTA",eposta);
                        cmd.Parameters.AddWithValue("@YAS",yas);

                        connection.Open();
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Yeni Hasta Eklendi");
                        hastalariGoster();

                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata = " + ex);
            }
        }

        void ComboLoadData()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("sp_CinsiyetListele", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        DataTable dt = new DataTable();
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        connection.Open();
                        da.Fill(dt);
                        comboBox1.DisplayMember = "CINSIYET";
                        comboBox1.ValueMember = "ID";
                        comboBox1.DataSource = dt;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata = " + ex.Message);
            }
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if(dataGridView1.SelectedRows.Count>0)
            {
                textBox1.Text = dataGridView1.SelectedRows[0].Cells["TC_KIMLIK_NO"].Value.ToString();
                textBox2.Text = dataGridView1.SelectedRows[0].Cells["AD"].Value.ToString();
                textBox3.Text = dataGridView1.SelectedRows[0].Cells["SOYAD"].Value.ToString();
                comboBox1.SelectedIndex = comboBox1.FindStringExact(dataGridView1.SelectedRows[0].Cells["CINSIYET"].Value.ToString());
                dateTimePicker1.Value = Convert.ToDateTime(dataGridView1.SelectedRows[0].Cells["DOGUM_TARİHİ"].Value);
                textBox4.Text = dataGridView1.SelectedRows[0].Cells["TELEFON"].Value.ToString();
                textBox5.Text = dataGridView1.SelectedRows[0].Cells["EPOSTA"].Value.ToString();
                textBox6.Text = dataGridView1.SelectedRows[0].Cells["YAS"].Value.ToString();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string tcno = textBox1.Text;
            string ad = textBox2.Text;
            string soyad = textBox3.Text;
            int cinsiyetId = Convert.ToInt32(comboBox1.SelectedValue);
            DateTime dogumtarihi = dateTimePicker1.Value;
            string telefon = textBox4.Text;
            string eposta = textBox5.Text;
            int yas = Convert.ToInt32(textBox6.Text);

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("SP_HASTALAR_UPDATE", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ID", int.Parse(dataGridView1.SelectedRows[0].Cells[0].Value.ToString()));
                        cmd.Parameters.AddWithValue("@TC_KIMLIK_NO", tcno);
                        cmd.Parameters.AddWithValue("@AD", ad);
                        cmd.Parameters.AddWithValue("@SOYAD", soyad);
                        cmd.Parameters.AddWithValue("@CINSIYET_ID", cinsiyetId);
                        cmd.Parameters.AddWithValue("@DOGUM_TARİHİ", dogumtarihi);
                        cmd.Parameters.AddWithValue("@TELEFON", telefon);
                        cmd.Parameters.AddWithValue("@EPOSTA", eposta);
                        cmd.Parameters.AddWithValue("@YAS", yas);

                        connection.Open();
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Hasta Bilgileri Güncellendi");
                        hastalariGoster();

                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata = " + ex);
            }
        }
    }
}

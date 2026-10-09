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
    public partial class Randevular_form : Form
    {
        string connectionString;

        public Randevular_form()
        {
            InitializeComponent();
            connectionString = ConfigurationManager.ConnectionStrings["randevutakipConnectionString"].ConnectionString;
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void Randevular_form_Load(object sender, EventArgs e)
        {
            randevulariGoster();
            ComboLoadData();
        }

        void randevulariGoster()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("SP_RANDEVULAR_SELECT", connection))
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

        void ComboLoadData()
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
                        comboBox1.DisplayMember = "TC_KIMLIK_NO";
                        comboBox1.ValueMember = "ID";
                        comboBox1.DataSource = dt;
                        
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata = " + ex.Message);
            }

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("sp_DoktorListele", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        DataTable dt = new DataTable();
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        connection.Open();
                        da.Fill(dt);
                        comboBox2.DisplayMember = "TC Kimlik No";
                        comboBox2.ValueMember = "ID";
                        comboBox2.DataSource = dt;
                        
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata = " + ex.Message);
            }
        }

        
        private void button1_Click(object sender, EventArgs e)
        {
            int hastaId = (int)comboBox1.SelectedValue;
            int doktorId = (int)comboBox2.SelectedValue;
            DateTime randevuTarihi = dateTimePicker1.Value;
            string baslangicSaati = comboBox3.SelectedItem.ToString();
            string bitisSaati = comboBox4.SelectedItem.ToString();
            string notlar = richTextBox1.Text;

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("SP_RANDEVULAR_INSERT", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@HASTA_ID", hastaId);
                        cmd.Parameters.AddWithValue("@DOKTOR_ID", doktorId);
                        cmd.Parameters.AddWithValue("@RANDEVU_TARIHI", randevuTarihi);
                        cmd.Parameters.AddWithValue("@BASLANGIC_SAATİ", baslangicSaati);
                        cmd.Parameters.AddWithValue("@BITIS_SAATİ", bitisSaati);
                        cmd.Parameters.AddWithValue("@NOTLAR", notlar);

                        connection.Open();
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Yeni Randevu Eklendi");
                        randevulariGoster();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata = " + ex.Message);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            randevulariGoster();
            ComboLoadData();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count >0)
            {
                try
                {
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        using (SqlCommand cmd = new SqlCommand("SP_RANDEVULAR_DELETE", connection))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@ID", dataGridView1.SelectedRows[0].Cells[0].Value);
                            connection.Open();
                            cmd.ExecuteNonQuery();
                            MessageBox.Show("Randevu Silindi");
                            randevulariGoster();
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Hata = " + ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Lütfen silinecek randevuyu seçiniz.");
            }
        }

        
        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count >0)
            {
                comboBox1.SelectedIndex = comboBox1.FindStringExact(dataGridView1.SelectedRows[0].Cells["Hasta TC"].Value.ToString());

                comboBox2.SelectedIndex = comboBox2.FindStringExact(dataGridView1.SelectedRows[0].Cells["Doktor TC"].Value.ToString());

                dateTimePicker1.Value = Convert.ToDateTime(dataGridView1.SelectedRows[0].Cells["Tarih"].Value);

                comboBox3.SelectedIndex = comboBox3.FindStringExact(dataGridView1.SelectedRows[0].Cells["Başlangıç"].Value.ToString());
                comboBox4.SelectedIndex = comboBox4.FindStringExact(dataGridView1.SelectedRows[0].Cells["Bitiş"].Value.ToString());

                richTextBox1.Text = dataGridView1.SelectedRows[0].Cells["Notlar"].Value.ToString();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        { 
            try
            {
                int id = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value);
                int hastaId = (int)comboBox1.SelectedValue;
                int doktorId = (int)comboBox2.SelectedValue;
                DateTime randevuTarihi = dateTimePicker1.Value;
                string baslangicSaati = comboBox3.SelectedItem.ToString();
                string bitisSaati = comboBox4.SelectedItem.ToString();
                string notlar = richTextBox1.Text;

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("SP_RANDEVULAR_UPDATE", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ID", id);
                        cmd.Parameters.AddWithValue("@HASTA_ID", hastaId);
                        cmd.Parameters.AddWithValue("@DOKTOR_ID", doktorId);
                        cmd.Parameters.AddWithValue("@RANDEVU_TARIHI", randevuTarihi);
                        cmd.Parameters.AddWithValue("@BASLANGIC_SAATİ", baslangicSaati);
                        cmd.Parameters.AddWithValue("@BITIS_SAATİ", bitisSaati);
                        cmd.Parameters.AddWithValue("@NOTLAR", notlar);

                        connection.Open();
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Randevu Güncellendi");
                        randevulariGoster();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata = " + ex.Message);
            }
            
        }
    }
}

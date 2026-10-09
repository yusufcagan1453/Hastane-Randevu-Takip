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
    public partial class Doktorlar_form : Form
    {
       
        string connectionString;
        public Doktorlar_form()
        {
            InitializeComponent();
            connectionString = ConfigurationManager.ConnectionStrings["randevutakipConnectionString"].ConnectionString;
        }

        private void Doktorlar_form_Load(object sender, EventArgs e)
        {
            doktorlariGoster();
            ComboLoadData();
        }

        void doktorlariGoster()
        {
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

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("SP_BRANS_Listele", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        DataTable dt1 = new DataTable();
                        SqlDataAdapter da1 = new SqlDataAdapter(cmd);
                        connection.Open();
                        da1.Fill(dt1);
                        comboBox2.DisplayMember = "BRANS";
                        comboBox2.ValueMember = "ID";
                        comboBox2.DataSource = dt1;
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
            doktorlariGoster();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            string tcno = textBox1.Text;
            string ad = textBox2.Text;
            string soyad = textBox3.Text;
            int cinsiyetId = (int)comboBox1.SelectedValue;
            int bransId = (int)comboBox2.SelectedValue;
            string calismaSaati = textBox4.Text;
            string telefon = textBox5.Text;

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("SP_DOKTORLAR_INSERT", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@TC_KIMLIK_NO", tcno);
                        cmd.Parameters.AddWithValue("@AD", ad);
                        cmd.Parameters.AddWithValue("@SOYAD", soyad);
                        cmd.Parameters.AddWithValue("@CINSIYET_ID", cinsiyetId);
                        cmd.Parameters.AddWithValue("@BRANS_ID", bransId);
                        cmd.Parameters.AddWithValue("@CALISMA_SAATI", calismaSaati);
                        cmd.Parameters.AddWithValue("@TELEFON", telefon);

                        connection.Open();
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Yeni Doktor Eklendi");
                        doktorlariGoster();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata = " + ex.Message);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count >0)
            {
                try
                {
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        using (SqlCommand cmd = new SqlCommand("SP_DOKTORLAR_DELETE", connection))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@ID", dataGridView1.SelectedRows[0].Cells[0].Value);
                            connection.Open();
                            cmd.ExecuteNonQuery();
                            MessageBox.Show("Doktor Silindi");
                            doktorlariGoster();
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
                MessageBox.Show("Lütfen silinecek doktoru seçiniz.");
            }
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count >0)
            {
                textBox1.Text = dataGridView1.SelectedRows[0].Cells["Tc Kimlik No"].Value.ToString();
                textBox2.Text = dataGridView1.SelectedRows[0].Cells["Ad"].Value.ToString();
                textBox3.Text = dataGridView1.SelectedRows[0].Cells["Soyad"].Value.ToString();
                comboBox1.SelectedIndex = comboBox1.FindStringExact(dataGridView1.SelectedRows[0].Cells["Cinsiyet"].Value.ToString());
                comboBox2.SelectedIndex = comboBox2.FindStringExact(dataGridView1.SelectedRows[0].Cells["Branş"].Value.ToString());
                textBox4.Text = dataGridView1.SelectedRows[0].Cells["Çalışma Saati"].Value.ToString();
                textBox5.Text = dataGridView1.SelectedRows[0].Cells["Telefon"].Value.ToString();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                int id = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value);
                string tcno = textBox1.Text;
                string ad = textBox2.Text;
                string soyad = textBox3.Text;
                int cinsiyetId = (int)comboBox1.SelectedValue;
                int bransId = (int)comboBox2.SelectedValue;
                string calismaSaati = textBox4.Text;
                string telefon = textBox5.Text;

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("SP_DOKTORLAR_UPDATE", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ID", id);
                        cmd.Parameters.AddWithValue("@TC_KIMLIK_NO", tcno);
                        cmd.Parameters.AddWithValue("@AD", ad);
                        cmd.Parameters.AddWithValue("@SOYAD", soyad);
                        cmd.Parameters.AddWithValue("@CINSIYET_ID", cinsiyetId);
                        cmd.Parameters.AddWithValue("@BRANS_ID", bransId);
                        cmd.Parameters.AddWithValue("@CALISMA_SAATI", calismaSaati);
                        cmd.Parameters.AddWithValue("@TELEFON", telefon);

                        connection.Open();
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Doktor Güncellendi");
                        doktorlariGoster();
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

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace otomasyon
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Hastalar_form hastalar_Form = new Hastalar_form();
            hastalar_Form.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Doktorlar_form doktorlar_Form = new Doktorlar_form();
            doktorlar_Form.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Randevular_form randevular_Form = new Randevular_form();
            randevular_Form.ShowDialog();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.BackColor=Color.FromArgb(46, 63, 81);
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}

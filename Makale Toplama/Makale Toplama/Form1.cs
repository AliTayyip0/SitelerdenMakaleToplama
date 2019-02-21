using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Makale_Toplama
{
    public partial class Form1 : Form
    {

        public static string URL;
        
        public Form1()
        {

            InitializeComponent();
           
        }

        private void label2_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Sayfanın adresini kopyalayıp aşağıdaki boşluğa yapıştırınız.","BİLGİLENDİRME" ,MessageBoxButtons.OK , MessageBoxIcon.Asterisk);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            label4.Visible = true;
            try
            {
                if (textBox1.Text != "")
                {
                    URL = textBox1.Text;
                    FormMakaleler frmac = new FormMakaleler();
                    frmac.Show();  
                    
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("URL giriniz..");
                }
            }
            catch 
            {
                MessageBox.Show("İnternet Bağlantınızı Kontrol Ediniz.");
            }
            
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
           
        }
    }
}

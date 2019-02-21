using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.IO;
using System.Web;
using System.Collections;

namespace Makale_Toplama
{
    public partial class FormMakaleler : Form
    {
        //------------------------------------------------
        ArrayList kelimelist = new ArrayList();
        public static string çevrilecek_metin;
        string adres = Form1.URL;
        int arraynerde = 0;
        //------------------------------------------------


        public FormMakaleler()
        {
            InitializeComponent();

            webBrowser1.Navigate("https://translate.google.com.tr/#auto/tr/");
            while (webBrowser1.ReadyState != WebBrowserReadyState.Complete)
            {
                Application.DoEvents();
            }
            HtmlCek();

            

        }

        
        void HtmlCek()
        {
            WebRequest istek = HttpWebRequest.Create(adres);
            WebResponse cevap;
            cevap = istek.GetResponse();
            StreamReader donenBilgiler = new StreamReader(cevap.GetResponseStream());
            string gelen = donenBilgiler.ReadToEnd();
            //richTextBox2.Text=gelen;
            paragraflariBulmaBaslangic(gelen);
        }


        void paragraflariBulmaBaslangic(string gelen)
        {
            string[] a = { "<p>", "<br>", "<pre>", "<span>" };
            string[] b = { "</p>", "</br>", "</pre>", "</span>" };

            for (int i = 0; i < a.Length; i++)
            {
                ParagraflarıBulma(gelen, a[i], b[i]);
            }
        }
        void ParagraflarıBulma(string gelen, string bas, string son)
        {
            string baslik = "";
            int konum = gelen.IndexOf(bas);
            int konum3 = gelen.IndexOf(son);

            while (konum != -1 && konum3 != -1)
            {

                int titleIndexBaslangici = gelen.IndexOf(bas, konum) + 3;
                konum = gelen.IndexOf(bas, konum + 1);

                int titleIndexBitisi = gelen.IndexOf(son, konum3);
                konum3 = gelen.IndexOf(son, konum3 + 1);

                //int titleIndexBitisi = gelen.Substring(titleIndexBaslangici).IndexOf(son);

                if (titleIndexBitisi > titleIndexBaslangici)
                {
                    baslik = baslik + gelen.Substring(titleIndexBaslangici, titleIndexBitisi - titleIndexBaslangici)+" ";

                }
                else
                {
                    titleIndexBitisi = gelen.IndexOf(son, konum3);
                    konum3 = gelen.IndexOf(son, konum3 + 1);
                    if (titleIndexBitisi > titleIndexBaslangici)
                    {
                        baslik = baslik + gelen.Substring(titleIndexBaslangici, titleIndexBitisi - titleIndexBaslangici)+" ";

                    }
                }

                //baslik.Trim();

                //richTextBox1.Text = richTextBox1.Text + baslik+"\n" ;
                //richTextBox2.Text = baslik + "\n";

                // richTextBox1.Text.Trim();


                //if (baslik.IndexOf("<strong>") != -1)
                //{
                //    //int strongkes = gelen.IndexOf("<strong>") + 7;
                //    //int strongbit = gelen.Substring(strongkes).IndexOf("</strong>");
                //    //baslik = gelen.Substring(strongkes, strongbit);
                //    richTextBox1.Text = richTextBox1.Text + baslik;
                //    //string Replace(string s1, string s2)
                //}
                //else
                //{
                //}

            }
            if (baslik != "")
            {
                temizlemebaslangici(baslik);
            }
            //temizlemebaslangici(baslik);
        }



        void temizlemebaslangici(string metin)
        {
            int digerinegec = 0;
            int sonageldimi = 0;
            string[] a = { "<script", "<style", "<noscript", "<iframe", "{", "/*" };
            string[] b = { "script>", "style>", "noscript>", "iframe>", "}", "*/" };

            for (int i = 0; i < a.Length; i++)
            {

                sonageldimi++;
                if (sonageldimi == a.Length)
                {
                    digerinegec = 1;
                }
                textTemizle1(metin, a[i], b[i], digerinegec);
            }

        }
        void textTemizle1(string metin, string bas, string son, int digerinegec)
        {

            try
            {


                int konum = metin.IndexOf(bas);
                int konum2 = metin.IndexOf(son);
                while (konum != -1 && konum2 != -1)
                {
                    if (konum2 > konum)
                    {
                        metin = metin.Remove(konum, konum2 - konum + 1);
                        konum = metin.IndexOf(bas);
                        konum2 = metin.IndexOf(son);
                    }
                    else
                    {
                        metin = metin.Remove(konum2, 1);
                        konum = metin.IndexOf(bas);
                        konum2 = metin.IndexOf(son);
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
            if (digerinegec == 1)
            {
                textBoxTemizleme2(metin);
            }

        }
        void textBoxTemizleme2(string metin)
        {
            try
            {


                int konum = metin.IndexOf("<");
                int konum2 = metin.IndexOf(">");
                while (konum != -1 && konum2 != -1)
                {
                    if (konum2 > konum)
                    {
                        metin = metin.Remove(konum, konum2 - konum + 1);
                        konum = metin.IndexOf("<");
                        konum2 = metin.IndexOf(">");
                    }
                    else
                    {
                        metin = metin.Remove(konum2, 1);
                        konum = metin.IndexOf("<");
                        konum2 = metin.IndexOf(">");
                    }

                    //konum = metin.IndexOf("<a href", konum + 1);
                    //konum2 = metin.IndexOf("</a>", konum2 + 1);
                    //konum = metin.IndexOf("<p>", konum + 1);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
            richTextBox1.Text = richTextBox1.Text + metin.Trim() + " ";
        }



        


        private void FormMakaleler_FormClosing(object sender, FormClosingEventArgs e)
        {
            Form1 formac = new Form1();
            formac.Show();
        }
        
        
        //--------------------------------------------------------------------
        private void btnkayit1_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            dialog.Description = "Lütfen aşağıdaki listeden bir dizin seçiniz.";
            dialog.RootFolder = Environment.SpecialFolder.Desktop;
            dialog.SelectedPath = @"C:\Program Files";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                StreamWriter dosya1 = new StreamWriter(dialog.SelectedPath + "\\" + textBox1.Text + ".txt", false);
                dosya1.Write(richTextBox1.Text);
                dosya1.Close();
                MessageBox.Show("Kayıt Tamamlanmıştır.\nKaydedilen dosyanın yolu:" + dialog.SelectedPath + "\\" + textBox1.Text + ".txt");
            }


        }

        private void btnkayit2_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            dialog.Description = "Lütfen aşağıdaki listeden bir dizin seçiniz.";
            dialog.RootFolder = Environment.SpecialFolder.Desktop;
            dialog.SelectedPath = @"C:\Program Files";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                StreamWriter dosya1 = new StreamWriter(dialog.SelectedPath + "\\" + textBox1.Text + ".txt", false);
                dosya1.Write(richTextBox1.Text + richTextBox2.Text);
                dosya1.Close();
                MessageBox.Show("Kayıt Tamamlanmıştır.\nKaydedilen dosyanın yolu:" + dialog.SelectedPath + "\\" + textBox1.Text + ".txt");
            }
        }

        private void btnkayit3_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            dialog.Description = "Lütfen aşağıdaki listeden bir dizin seçiniz.";
            dialog.RootFolder = Environment.SpecialFolder.Desktop;
            dialog.SelectedPath = @"C:\Program Files";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                StreamWriter dosya1 = new StreamWriter(dialog.SelectedPath + "\\" + textBox1.Text + "(Orjinal).txt", false);
                dosya1.Write(richTextBox1.Text);
                dosya1.Close();
                MessageBox.Show("Kayıt Tamamlanmıştır.\nKaydedilen dosyanın yolu:" + dialog.SelectedPath + "\\" + textBox1.Text + "(Orjinal).txt");

                StreamWriter dosya2 = new StreamWriter(dialog.SelectedPath + "\\" + textBox1.Text + "(Çeviri).txt", false);
                dosya2.Write(richTextBox2.Text);
                dosya2.Close();
                MessageBox.Show("Kayıt Tamamlanmıştır.\nKaydedilen dosyanın yolu:" + dialog.SelectedPath + "\\" + textBox1.Text + "(Çeviri).txt");
            }
        }
        //--------------------------------------------------------------------

        //--------------------------------------------------------------------
        void çeviri(string gelen)
        {
            try
            {
                webBrowser1.Document.GetElementById("source").InnerText = gelen;
                webBrowser1.Document.GetElementById("gt-submit").InvokeMember("click");
                webBrowser1.ScriptErrorsSuppressed = true;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }

            timer1.Start();

            //timer1.Start();

            //while (richTextBox2.Text!="")
            //{
            //    richTextBox2.Text = richTextBox2.Text + webBrowser1.Document.GetElementById("result_box").InnerText;

            //}

            /*
            string bas = "<span ";
            string son = "</span>";
            string baslik = "";
            int konum = gelen.IndexOf(bas);
            int konum3 = gelen.IndexOf(son);
            while (konum != -1 && konum3 != -1)
            {

                int titleIndexBaslangici = gelen.IndexOf(bas, konum) + 3;
                konum = gelen.IndexOf(bas, konum + 1);
                int titleIndexBitisi = gelen.IndexOf(son, konum3);
                konum3 = gelen.IndexOf(son, konum3 + 1);
                if (titleIndexBitisi > titleIndexBaslangici)
                {
                    baslik = baslik + gelen.Substring(titleIndexBaslangici, titleIndexBitisi - titleIndexBaslangici);

                }
            }

            int konum1 = baslik.IndexOf("<");
            int konum2 = baslik.IndexOf(">");
            while (konum1 != -1 && konum2 != -1)
            {
                if (konum2 > konum1)
                {
                    baslik = baslik.Remove(konum1, konum2 - konum1 + 1);
                    konum1 = baslik.IndexOf("<");
                    konum2 = baslik.IndexOf(">");
                }
                else
                {
                    baslik = baslik.Remove(konum2, 1);
                    konum1 = baslik.IndexOf("<");
                    konum2 = baslik.IndexOf(">");
                }
            }
            richTextBox2.Text = baslik;
*/
        }

        private void btnceviri_Click(object sender, EventArgs e)
        {


            int mod;
            int uzunluğu = 0;
            string asılmetin = richTextBox1.Text;
            mod = asılmetin.Length % 1444;
            if (asılmetin.Length > 1444)
            {
                uzunluğu = asılmetin.Length / 1444;

            }


            for (int i = 0; i <= uzunluğu; i++)
            {

                if (i != uzunluğu)
                {
                    kelimelist.Add(richTextBox1.Text.Substring(i * 1444, 1443));
                }
                else
                {
                    kelimelist.Add(richTextBox1.Text.Substring(i * 1444, mod - 1));
                }
            }
            Clipboard.SetText(" ");
            çeviri(kelimelist[arraynerde].ToString());


            //string a = webBrowser1.Document.Body.InnerHtml.ToString();
            //richTextBox2.Text = a;
            //çeviri(a);


            //Clipboard.SetText(richTextBox1.Text);
            //çevrilecek_metin = richTextBox1.Text;
            //ÇEVİRİ FORMAC = new ÇEVİRİ();
            //FORMAC.Show();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            
            webBrowser1.Document.GetElementById("gt-res-copy").InvokeMember("click");
            if (Clipboard.GetText()!=" ")
            {
                arraynerde++;
                richTextBox2.Text = richTextBox2.Text + Clipboard.GetText();
                Clipboard.SetText(" ");
                int listeuzunlugu = kelimelist.Count-1;
                if (listeuzunlugu >= arraynerde)
                {
                    çeviri(kelimelist[arraynerde].ToString());

                }
                else
                {
                    timer1.Stop();
                    Clipboard.SetText(Form1.URL);
                }
            }



            //timer++;
            //if (timer%3==0)
            //{
            //string çevrilen = webBrowser1.Document.GetElementById("result_box").InnerText;

            //if (çevrilen != null)
            //{
            //    arraynerde++;
            //    richTextBox2.Text = richTextBox2.Text + çevrilen;
            //    çevrilen = null;
            //    int listeuzunlugu = kelimelist.Count;
            //    if (listeuzunlugu + 1 != arraynerde)
            //    {
            //        çeviri(kelimelist[arraynerde].ToString());

            //    }
            //    timer1.Stop();
            //    //http://www.bizimyol.info/news/93152.html

            //}

            //}   gt-res-copy

        }
        //--------------------------------------------------------------------

    }
}


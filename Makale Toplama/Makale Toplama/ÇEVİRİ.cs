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
    public partial class ÇEVİRİ : Form
    {
        public ÇEVİRİ()
        {
            InitializeComponent();
            webBrowser1.Navigate("https://translate.google.com.tr/#auto/tr/");
        }

        private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {

            while (webBrowser1.ReadyState != WebBrowserReadyState.Complete)
            {
                Application.DoEvents();
            }
            webBrowser1.Document.GetElementById("source").InnerText = FormMakaleler.çevrilecek_metin;

        }
    }
}

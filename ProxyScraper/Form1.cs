using System;
using System.ComponentModel;
using System.Text.RegularExpressions;
using System.Net;
using System.Windows.Forms;
using System.IO;
using System.Net;

namespace ProxyScraper
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        WebClient Wc = new WebClient();
        Defaults DF = new Defaults();

        public bool test(string proxy)
        {
            string []a=proxy.Split(':');    
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://google.com");
            request.Proxy = new WebProxy(a[0],Convert.ToInt32(a[1]));
            request.UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/39.0.2171.95 Safari/537.36";
            request.Timeout = 1500;
            try
            {

                WebResponse rp = request.GetResponse();
                return true;

            }
            catch (Exception)
            {
                return false;
                
            }
        }


        private void ProxyAl()
        {
            try
            {
                foreach (string kaynak in richTextBox1.Lines)
                {
                    string webkaynak = Wc.DownloadString(kaynak);
                    MatchCollection MC = DF.REGEX.Matches(webkaynak);
                    foreach (Match Proxy in MC)
                    {
                        string uProxy = Proxy.ToString();
                            uProxy=uProxy.Replace("</td><td>", ":");
                        // Çalışanları test edip eklemek için.
                        if (test(uProxy) == true)
                            listBox1.Items.Add(uProxy);
                        
                    }
                }

            }
            catch(Exception)
            {

            }

      

        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            if ((backgroundWorker1.CancellationPending == true))
            {
                e.Cancel = true;
            }
            ProxyAl();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            CheckForIllegalCrossThreadCalls = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Ahmed Yılmaz tarafından kodlanmıştır. Çoğu ücretsiz proxy siteleri çalışmayabilir sebebi RegEx desteklenmiyor olmasıdır.");
        }

        private void BtnIslem_Click(object sender, EventArgs e)
        {
            try
            {
                backgroundWorker1.RunWorkerAsync();
            }
            catch (Exception)
            {
                MessageBox.Show("Şu anda zaten işlem yapılmakta bekleyiniz!");
            }
        }

        private void BtnKaydet_Click(object sender, EventArgs e)
        {
            //string path;
            saveFileDialog1.Filter = "Metin Belgesi|*.txt";
            saveFileDialog1.CreatePrompt = true;
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                StreamWriter sw = new StreamWriter(saveFileDialog1.FileName);
                foreach (var px in listBox1.Items)
                {
                    sw.WriteLine(px);
                }
                sw.Close();
                MessageBox.Show("Dosya başarıyla kaydedildi!");
            }
        }


        private void button1_Click_1(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}

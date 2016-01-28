using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace robotdownload
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string[] aryString = (!String.IsNullOrEmpty(textBox1.Text.Trim())) ? textBox1.Lines : null;
            string errid = "";
            using (var client = new WebClient())
            {
                try
                {
                    client.DownloadProgressChanged += Client_DownloadProgressChanged;
                    client.DownloadDataCompleted += Client_DownloadDataCompleted;
                    this.Cursor = Cursors.WaitCursor;
                    foreach (string s in aryString)
                    {
                        if (string.IsNullOrEmpty(s)) continue;
                        errid = s;
                        
                        string flvfile = textBox3.Text + s + ".flv";
                        if (!Directory.Exists(textBox3.Text)) Directory.CreateDirectory(textBox3.Text);
                        if (!File.Exists(flvfile))
                        {
                            //client.DownloadFileAsync(new Uri(textBox2.Text + s + ".flv"), flvfile);
                            client.DownloadFile(textBox2.Text + s + ".flv", flvfile);
                            listBox1.Items.Add(s);
                        }
                        else
                        {
                            listBox1.Items.Add(s);
                        }
                        textBox1.Text = textBox1.Text.Remove(textBox1.Text.IndexOf(s), s.Length+2);
                    }

                    textBox1.AppendText("\n");
                    textBox1.AppendText("-------Finish!--------\n");
                }
                catch (Exception err)
                {
                    listBox2.Items.Add("Game ID:" + errid + ", ErrMsg:" + err.Message);
                    if (textBox1.Text.Length <= errid.Length)
                        textBox1.Text = textBox1.Text.Remove(textBox1.Text.IndexOf(errid), errid.Length);
                    else
                        textBox1.Text = textBox1.Text.Remove(textBox1.Text.IndexOf(errid), errid.Length + 2);
                }
                finally
                {
                    this.Cursor = Cursors.Default;
                }
                //client.DownloadFile("http://playback.hointeractive.com:444/Asia/25-08-15/BJ/2dpte5oizvvh8r2jt4hkzl0j.flv", "D:\\download\\a.flv");
            }
        }

        private void Client_DownloadDataCompleted(object sender, DownloadDataCompletedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void Client_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            //progressBar1.Value = e.ProgressPercentage;
            Application.DoEvents();
        }

        private void doDownLoadProcess()
        {

        }

        private void clearSuccessToolStripMenuItem_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
        }

        private void clearFailToolStripMenuItem_Click(object sender, EventArgs e)
        {
            listBox2.Items.Clear();
        }
    }
}

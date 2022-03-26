using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace Bunebea
{
    public partial class Form1 : Form
    {
        public string dosya;
        public Form1()
        {
            InitializeComponent();
            this.AllowDrop = true;
            this.DragEnter += Form1_DragEnter;
            this.DragDrop += Form1_DragDrop;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            timer1.Enabled = true;
        }

        private void Form1_DragDrop(object sender, DragEventArgs e)
        {
            
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            foreach (string file in files) dosya = file;
            string output;
            using (var md5 = MD5.Create())
            {
                using (var stream = File.OpenRead(dosya))
                {
                    byte[] checksum = md5.ComputeHash(stream);
                    output = BitConverter.ToString(checksum).Replace("-", String.Empty).ToLower();
                    MessageBox.Show(output);
                }
            }
        }

        private void Form1_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop)) e.Effect = DragDropEffects.Copy;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (intkontrolet())
                label1.Text = "İnternet Bağlantısı Başarılı!";
            else label1.Text = "İnternet Bağlantısı Sağlanamadı!";
        }
        public static bool intkontrolet()
        {
            try
            {
                using (var client = new WebClient())
                using (var stream = client.OpenRead("http://www.google.com"))
                {
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }
    }
}

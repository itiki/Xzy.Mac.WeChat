using IPADDemo.Model;
using IPADDemo.WeChat;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IPADDemo
{
    public partial class FormDemo : Form
    {
        XzyWeChatThread weChatThread;

        public FormDemo()
        {
            InitializeComponent();
            WxDelegate.qrCode += new QrCode(this.calback_qrcode);
            WxDelegate.show += new Show(this.calback_show);
            CheckForIllegalCrossThreadCalls = false;
        }

        private void FormDemo_Load(object sender, EventArgs e)
        {
            weChatThread = new XzyWeChatThread();
        }

        void calback_qrcode(string qrcode)
        {
            byte[] arr2 = Convert.FromBase64String(qrcode);
            using (MemoryStream ms2 = new MemoryStream(arr2))
            {
                System.Drawing.Bitmap bmp2 = new System.Drawing.Bitmap(ms2);
                pictureBox1.Image = bmp2;
            }
        }

        void calback_show(string str)
        {
            textBox1.Text = str;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            weChatThread.Wx_SendMsg(txt_msgWxid.Text, txt_msgText.Text);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();

            ofd.Filter = "图片文件 |*.jpg;*.png";
            ofd.ValidateNames = true;
            ofd.CheckPathExists = true;
            ofd.CheckFileExists = true;
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                string FileName = ofd.FileName;
                weChatThread.Wx_SendImg(txt_msgWxid.Text, FileName);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            weChatThread.Wx_SendMoment(txt_snsText.Text);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            List<string> imgPath = new List<string>();
            foreach (var a in listBox1.Items) {
                imgPath.Add(ImgToBase64String(a.ToString()));
            }
            weChatThread.Wx_SendMoment(txt_snsText.Text, imgPath);

        }
        private string ImgToBase64String(string Imagefilename)
        {
            try
            {
                Bitmap bmp = new Bitmap(Imagefilename);

                MemoryStream ms = new MemoryStream();
                bmp.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                byte[] arr = new byte[ms.Length];
                ms.Position = 0;
                ms.Read(arr, 0, (int)ms.Length);
                ms.Close();
                return Convert.ToBase64String(arr);
            }
            catch (Exception ex)
            {
                return null;
            }
        }


        private void button5_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Multiselect = true;

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                listBox1.Items.AddRange(ofd.FileNames);
            }
        }


    }
}

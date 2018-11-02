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
        public FormDemo()
        {
            InitializeComponent();

            WxDelegate.qrCode += new QrCode(this.calback_qrcode);

            WxDelegate.show += new Show(this.calback_show);

            WxDelegate.receiveMsg += new ReceiveMsg(this.calback_receiveMsg);

            WxDelegate.getContacts += new GetContacts(this.calback_getContacts);

            WxDelegate.getGroups += new GetGroups(this.calback_getGroups);
        }

        private void FormDemo_Load(object sender, EventArgs e)
        {
            XzyWeChatThread weChatThread = new XzyWeChatThread();

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

        /// <summary>
        /// 在文本框内显示信息
        /// </summary>
        /// <param name="str">待显示的信息</param>
        void calback_show(string str)
        {
            
        }

        /// <summary>
        /// 接收消息的信息
        /// </summary>
        /// <param name="msg"></param>
        void calback_receiveMsg(string msg)
        {

        }

        /// <summary>
        /// 获取到通讯录列表时发生，此通讯录，包括好友和群、公众号等全部
        /// </summary>
        /// <param name="contacts">通讯录列表</param>
        void calback_getContacts(List<Contact> contacts)
        {

        }

        /// <summary>
        /// 获取到群列表时发生
        /// </summary>
        /// <param name="contacts">群列表</param>
        void calback_getGroups(List<WxGroup> contacts)
        {

        }

    }
}

using IPADDemo.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPADDemo.Model
{
    /// <summary>
    /// 委托存放
    /// </summary>
    public static class WxDelegate
    {
        public static QrCode qrCode { get; set; }
        public static Show show { get; set; }
        public static ReceiveMsg receiveMsg { get; set; }
        public static GetContacts getContacts { get; set; }
        public static GetGroups getGroups { get; set; }
    }

    /// <summary>
    /// 二维码
    /// </summary>
    /// <param name="qrcode"></param>
    public delegate void QrCode(string qrcode);
    /// <summary>
    /// 系统通知
    /// </summary>
    public delegate void Show(string str);
    /// <summary>
    /// 接收文字消息
    /// </summary>
    /// <param name="msg"></param>
    public delegate void ReceiveMsg(string msg);
    /// <summary>
    /// 获取联系人
    /// </summary>
    /// <param name="jsonObj"></param>
    public delegate void GetContacts(List<Contact> contacts);
    /// <summary>
    /// 获取群组
    /// </summary>
    /// <param name="groups"></param>
    public delegate void GetGroups(List<WxGroup> groups);
}

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

        public static GetContact getContact { get; set; }

        public static GetGroup getGroup { get; set; }
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
    /// 获取联系人
    /// </summary>
    public delegate void GetContact(Contact contact);

    /// <summary>
    /// 获取群组
    /// </summary>
    public delegate void GetGroup(Contact group);
}

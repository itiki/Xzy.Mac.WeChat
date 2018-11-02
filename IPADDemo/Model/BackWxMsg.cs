using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPADDemo.Model
{
    /// <summary>
    /// 微信消息
    /// </summary>
    public class BackWxMsg
    {
        public string content { get; set; }

        public string wxid { get; set; }

        public string wxname { get; set; }

        public string groupid { get; set; }

        public string groupname { get; set; }

        public string msgid { get; set; }

        public int inputtime { get; set; }

        public string chmsgid { get; set; }
    }
}

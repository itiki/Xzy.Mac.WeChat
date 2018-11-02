using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPADDemo.Model
{
    /// <summary>
    /// Tcp消息
    /// </summary>
    public class MsgContent
    {
        public string content { get; set; }

        public bool Img { get; set; }

        public string ImgPath { get; set; }

        public string towxid { get; set; }
    }
}

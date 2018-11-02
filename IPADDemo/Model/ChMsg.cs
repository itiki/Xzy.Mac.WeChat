using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPADDemo.Model
{

    /// <summary>
    /// 撤回消息
    /// </summary>
    public class ChMsg
    {
        public string wxid { get; set; }

        public string groupid { get; set; }

        public string msgid { get; set; }

        public string chmsgid { get; set; }
    }
}

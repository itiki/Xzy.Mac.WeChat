using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPADDemo.Model
{
    public class WxUser
    {
        /// <summary>
        /// 句柄
        /// </summary>
        public object hander { get; set; }

        /// <summary>
        /// 微信ID
        /// </summary>
        public string wxid { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public int type { get; set; }

        /// <summary>
        /// 功能
        /// </summary>
        public int code { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string name { get; set; }
    }
}

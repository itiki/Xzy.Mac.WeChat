using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPADDemo.Model
{
    public class WxGroup
    {

        /// <summary>
        /// 群id
        /// </summary>
        public string groupid { get; set; }


        /// <summary>
        /// 群名称
        /// </summary>
        public string groupname { get; set; }

        /// <summary>
        /// 微信ID
        /// </summary>
        public string WxUser { get; set; }

        /// <summary>
        /// 归属句柄
        /// </summary>
        public string hander { get; set; }


        public int membercount { get; set; }

        public List<WxMember> member { get; set; }
    }
}

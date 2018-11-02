using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPADDemo.Model
{
    public static class TcpMsg
    {
        /// <summary>
        /// 小号上线消息
        /// </summary>
        public const string OL = "user_login";

        /// <summary>
        /// 小号下线
        /// </summary>
        public const string Out = "user_out";

        /// <summary>
        /// 接收群组
        /// </summary>
        public const string Group = "Groupitem";

        /// <summary>
        /// 接收群成员
        /// </summary>
        public const string GroupMember = "GroupMember";

        /// <summary>
        /// 接收微信消息
        /// </summary>
        public const string content = "content";

        /// <summary>
        /// 接收红包数据
        /// </summary>
        public const string redpack = "redpack";

        /// <summary>
        /// 接收图片消息
        /// </summary>
        public const string img = "img";

        /// <summary>
        /// 撤回消息
        /// </summary>
        public const string chmsg = "chmsg";

        public const string BankerConfig = "BankerConfig";
    }
}

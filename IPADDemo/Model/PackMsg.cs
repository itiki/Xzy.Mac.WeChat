using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPADDemo.Model
{
    public class PackMsg
    {
        public string fromuser { get; set; }
        /// <summary>
        /// 红包key
        /// </summary>
        public string key { get; set; }

        /// <summary>
        /// 消息内容
        /// </summary>
        public string msg { get; set; }

        /// <summary>
        /// 第几页
        /// </summary>
        public int page { get; set; }

        /// <summary>
        /// 是否读完
        /// </summary>
        public bool ok { get; set; }

        /// <summary>
        /// 红包数据
        /// </summary>
        public Dictionary<string, packitme> packitme { get; set; }

        public int Timestamp { get; set; }
    }

    public class packitme
    {

        [JsonProperty("receiveAmount")]
        public int ReceiveAmount { get; set; }

        [JsonProperty("receiveTime")]
        public string ReceiveTime { get; set; }

        [JsonProperty("answer")]
        public string Answer { get; set; }

        [JsonProperty("receiveId")]
        public string ReceiveId { get; set; }

        [JsonProperty("state")]
        public int State { get; set; }

        [JsonProperty("gameTips")]
        public string GameTips { get; set; }

        [JsonProperty("receiveOpenId")]
        public string ReceiveOpenId { get; set; }

        [JsonProperty("userName")]
        public string UserName { get; set; }

        public int xh { get; set; }
    }

}

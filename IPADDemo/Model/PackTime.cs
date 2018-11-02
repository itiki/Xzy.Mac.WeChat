using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPADDemo.Model
{
    public class PackTime
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

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPADDemo.Model
{
    public class WxTtsMsg
    {

        [JsonProperty("content")]
        public string Content { get; set; }

        [JsonProperty("continue")]
        public int Continue { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("from_user")]
        public string FromUser { get; set; }

        [JsonProperty("msg_id")]
        public string MsgId { get; set; }

        [JsonProperty("msg_source")]
        public string MsgSource { get; set; }

        [JsonProperty("msg_type")]
        public int MsgType { get; set; }

        [JsonProperty("status")]
        public int Status { get; set; }

        [JsonProperty("sub_type")]
        public int SubType { get; set; }

        [JsonProperty("timestamp")]
        public int Timestamp { get; set; }

        [JsonProperty("to_user")]
        public string ToUser { get; set; }

        [JsonProperty("uin")]
        public Int64 Uin { get; set; }
    }
}

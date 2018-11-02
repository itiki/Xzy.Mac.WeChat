using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPADDemo.Model
{
    public class RedPacketJson
    {
        [JsonProperty("retcode")]
        public int Retcode { get; set; }

        [JsonProperty("retmsg")]
        public string Retmsg { get; set; }

        [JsonProperty("sendId")]
        public string SendId { get; set; }

        [JsonProperty("wishing")]
        public string Wishing { get; set; }

        [JsonProperty("isSender")]
        public int IsSender { get; set; }

        [JsonProperty("receiveStatus")]
        public int ReceiveStatus { get; set; }

        [JsonProperty("hbStatus")]
        public int HbStatus { get; set; }

        [JsonProperty("statusMess")]
        public string StatusMess { get; set; }

        [JsonProperty("hbType")]
        public int HbType { get; set; }

        [JsonProperty("watermark")]
        public string Watermark { get; set; }

        [JsonProperty("agree_duty")]
        public AgreeDuty AgreeDuty { get; set; }

        [JsonProperty("sendUserName")]
        public string SendUserName { get; set; }

        [JsonProperty("timingIdentifier")]
        public string TimingIdentifier { get; set; }
    }
}

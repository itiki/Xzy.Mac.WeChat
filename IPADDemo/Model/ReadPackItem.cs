using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPADDemo.Model
{

    public class ReadPackItem
    {

        [JsonProperty("retcode")]
        public int Retcode { get; set; }

        [JsonProperty("retmsg")]
        public string Retmsg { get; set; }

        [JsonProperty("recNum")]
        public int RecNum { get; set; }

        [JsonProperty("totalNum")]
        public int TotalNum { get; set; }

        [JsonProperty("totalAmount")]
        public int TotalAmount { get; set; }

        [JsonProperty("sendId")]
        public string SendId { get; set; }

        [JsonProperty("amount")]
        public int Amount { get; set; }

        [JsonProperty("wishing")]
        public string Wishing { get; set; }

        [JsonProperty("isSender")]
        public int IsSender { get; set; }

        [JsonProperty("receiveId")]
        public string ReceiveId { get; set; }

        [JsonProperty("hasWriteAnswer")]
        public int HasWriteAnswer { get; set; }

        [JsonProperty("operationHeader")]
        public object[] OperationHeader { get; set; }

        [JsonProperty("hbType")]
        public int HbType { get; set; }

        [JsonProperty("isContinue")]
        public int IsContinue { get; set; }

        [JsonProperty("hbStatus")]
        public int HbStatus { get; set; }

        [JsonProperty("receiveStatus")]
        public int ReceiveStatus { get; set; }

        [JsonProperty("statusMess")]
        public string StatusMess { get; set; }

        [JsonProperty("headTitle")]
        public string HeadTitle { get; set; }

        [JsonProperty("canShare")]
        public int CanShare { get; set; }

        [JsonProperty("hbKind")]
        public int HbKind { get; set; }

        [JsonProperty("recAmount")]
        public int RecAmount { get; set; }

        [JsonProperty("record")]
        public object[] Record { get; set; }

        [JsonProperty("operationTail")]
        public OperationTail OperationTail { get; set; }

        [JsonProperty("atomicFunc")]
        public AtomicFunc AtomicFunc { get; set; }

        [JsonProperty("jumpChange")]
        public int JumpChange { get; set; }

        [JsonProperty("changeWording")]
        public string ChangeWording { get; set; }

        [JsonProperty("sendUserName")]
        public string SendUserName { get; set; }
    }
}

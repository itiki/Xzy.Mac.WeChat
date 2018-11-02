using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPADDemo.Model
{
    public class Contact
    {
        [JsonProperty("big_head")]
        public string BigHead { get; set; }

        [JsonProperty("bit_mask")]
        public object BitMask { get; set; }

        [JsonProperty("bit_value")]
        public int BitValue { get; set; }

        [JsonProperty("chatroom_id")]
        public int ChatroomId { get; set; }

        [JsonProperty("chatroom_owner")]
        public string ChatroomOwner { get; set; }

        [JsonProperty("city")]
        public string City { get; set; }

        [JsonProperty("continue")]
        public int Continue { get; set; }

        [JsonProperty("country")]
        public string Country { get; set; }

        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("img_flag")]
        public int ImgFlag { get; set; }

        [JsonProperty("intro")]
        public string Intro { get; set; }

        [JsonProperty("label")]
        public string Label { get; set; }

        [JsonProperty("level")]
        public int Level { get; set; }

        [JsonProperty("max_member_count")]
        public int MaxMemberCount { get; set; }

        [JsonProperty("member_count")]
        public int MemberCount { get; set; }

        [JsonProperty("msg_type")]
        public int MsgType { get; set; }

        [JsonProperty("nick_name")]
        public string NickName { get; set; }

        [JsonProperty("provincia")]
        public string Provincia { get; set; }

        [JsonProperty("py_initial")]
        public string PyInitial { get; set; }

        [JsonProperty("quan_pin")]
        public string QuanPin { get; set; }

        [JsonProperty("remark")]
        public string Remark { get; set; }

        [JsonProperty("remark_py_initial")]
        public string RemarkPyInitial { get; set; }

        [JsonProperty("remark_quan_pin")]
        public string RemarkQuanPin { get; set; }

        [JsonProperty("sex")]
        public int Sex { get; set; }

        [JsonProperty("signature")]
        public string Signature { get; set; }

        [JsonProperty("small_head")]
        public string SmallHead { get; set; }

        [JsonProperty("source")]
        public int Source { get; set; }

        [JsonProperty("status")]
        public int Status { get; set; }

        [JsonProperty("stranger")]
        public string Stranger { get; set; }

        [JsonProperty("uin")]
        public Int64 Uin { get; set; }

        [JsonProperty("user_name")]
        public string UserName { get; set; }

        [JsonProperty("member")]
        public string Member { get; set; }
    }
}

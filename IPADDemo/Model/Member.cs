using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPADDemo.Model
{
    public class Member
    {

        [JsonProperty("big_head")]
        public string BigHead { get; set; }

        [JsonProperty("chatroom_nick_name")]
        public string ChatroomNickName { get; set; }

        [JsonProperty("invited_by")]
        public string InvitedBy { get; set; }

        [JsonProperty("nick_name")]
        public string NickName { get; set; }

        [JsonProperty("small_head")]
        public string SmallHead { get; set; }

        [JsonProperty("user_name")]
        public string UserName { get; set; }
    }
}

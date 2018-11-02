using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPADDemo.Model
{
    public class GroupMember
    {
        [JsonProperty("chatroom_id")]
        public int ChatroomId { get; set; }

        [JsonProperty("count")]
        public int Count { get; set; }

        [JsonProperty("member")]
        public string Member { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }

        [JsonProperty("status")]
        public int Status { get; set; }

        [JsonProperty("user_name")]
        public string UserName { get; set; }
    }
}

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPADDemo.Model
{
    public class WxQrCode
    {

        [JsonProperty("qr_code")]
        public string QrCodeStr { get; set; }
    }
}

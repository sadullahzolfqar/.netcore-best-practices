using System;
using System.Collections.Generic;
using System.Text;

namespace WcfClientService.DespatchService.HTTP.Request
{
    public class CreateClientRequest
    {
        public string serviceUri { get; set; }
        public string userServiceUri { get; set; }
        public string userName { get; set; }
        public string passWord { get; set; }
        public string customerCode { get; set; }
        public string vkn { get; set; }
        public string langCode { get; set; } = "tr";
    }
}

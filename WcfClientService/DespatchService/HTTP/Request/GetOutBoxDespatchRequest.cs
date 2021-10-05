using System;
using System.Collections.Generic;
using System.Text;

namespace WcfClientService.DespatchService.HTTP.Request
{
    public class GetOutBoxDespatchRequest
    {
        public string despatchNumber { get; set; }
        public string despatchId { get; set; }
    }
}

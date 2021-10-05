using System;
using System.Collections.Generic;
using System.Text;

namespace WcfClientService.DespatchService.HTTP
{
    public class DespatchResult
    {
        public bool isSuccess { get; set; } = true;
        public string message { get; set; }
        public string statusCode { get; set; }
        public object result { get; set; }
    }
}

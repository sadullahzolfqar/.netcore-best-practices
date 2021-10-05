using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WcfClientService.DespatchService.HTTP;
using WcfClientService.DespatchService.HTTP.Request;

namespace WcfClientService.DespatchService
{
    public interface IDespatchConnector
    {
        Task<DespatchResult> GetOutBoxDespatch(GetOutBoxDespatchRequest request);
    }
}

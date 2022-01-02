using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using UyumSoftDespatchConnect;
using WcfClientService.DespatchService.HTTP;
using WcfClientService.DespatchService.HTTP.Request;

namespace WcfClientService.DespatchService.UyumSoft
{
    public class UyumSoftDespatchConnector : IDespatchConnector
    {
        private DespatchIntegrationClient client;
        public UyumSoftDespatchConnector()
        {
        }
        public UyumSoftDespatchConnector(CreateClientRequest clientRequest)
        {
            BasicHttpBinding binding = new BasicHttpBinding();
            binding.MaxReceivedMessageSize = 2000000000;
            binding.AllowCookies = true;
            binding.Security.Mode = BasicHttpSecurityMode.TransportWithMessageCredential;

            EndpointAddress endPointAddress = new EndpointAddress(clientRequest.serviceUri);

            client = new DespatchIntegrationClient(binding, endPointAddress);
            client.Endpoint.Address = new System.ServiceModel.EndpointAddress(clientRequest.serviceUri);
            client.ClientCredentials.UserName.UserName = clientRequest.userName;
            client.ClientCredentials.UserName.Password = clientRequest.passWord;
        }

        public async Task<DespatchResult> GetOutBoxDespatch(GetOutBoxDespatchRequest request)
        {
            DespatchResult result = new DespatchResult();

            try
            {
                DespatchesDataResponse despatchResponse = await client.GetOutboxDespatchPdfAsync(request.despatchId);

                if (despatchResponse == null)
                {
                    result.isSuccess = false;
                    result.message = "No Reply From Service";

                    return result;
                }

                if (despatchResponse.IsSucceded == false)
                {
                    result.isSuccess = false;
                    result.message = despatchResponse.Message;

                    return result;
                }

                byte[] responseData = despatchResponse.Value.Items[0].Data;

                result.result = Convert.ToBase64String(responseData);
                result.isSuccess = true;

            }
            catch (Exception ex)
            {
                result.isSuccess = false;
                result.message = ex.Message;
            }

            return result;
        }
    }
}

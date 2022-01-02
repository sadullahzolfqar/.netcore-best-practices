using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WcfClientService.DespatchService;
using WcfClientService.DespatchService.HTTP.Request;

namespace BestPractices.Controllers
{
    [Route("api/wcfclient")]
    [ApiController]
    public class WcfTestController : ControllerBase
    {
        private IDespatchConnector despatchConnector;
        private string serviceType = "UYUMSOFT";
        public IActionResult Index()
        {
            SetDespatchObject();

            var despatch = despatchConnector.GetOutBoxDespatch(new GetOutBoxDespatchRequest
            {
                despatchId = "3336836b-a3bc-43c4-a126-9c23ec174ce1"
            });

            return Ok();
        }

        private void SetDespatchObject()
        {
            if (serviceType.Equals("UYUMSOFT"))
            {
                despatchConnector = new WcfClientService.DespatchService.UyumSoft.UyumSoftDespatchConnector(new CreateClientRequest
                {
                    serviceUri = "https://efatura-test.uyumsoft.com.tr/Services/DespatchIntegration",
                    userName = "uyumsoft",
                    passWord = "uyumsoft",
                });
            }
            else if (serviceType.Equals("EFINANS"))
            {
                despatchConnector = new WcfClientService.DespatchService.EFinans.EFinansConnector(new CreateClientRequest
                {
                    serviceUri = "https://erpefaturatest.cs.com.tr:8043/efatura/ws/connectorService?wsdl",
                    userServiceUri = "https://erpefaturatest.cs.com.tr:8043/efatura/ws/userService?wsdl",
                    userName = "USERNAME",
                    passWord = "PASSWORD",
                    customerCode = "KAS30499",
                    vkn = "0201830497",
                });
            }
        }
    }
}

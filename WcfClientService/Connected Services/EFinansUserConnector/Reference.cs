﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Bu kod bir araç tarafından oluşturuldu.
//
//     Bu dosyada yapılacak değişiklikler hatalı davranışa neden olabilir ve
//     kod yeniden oluşturulursa kaybolur.
// </auto-generated>
//------------------------------------------------------------------------------

namespace EFinansUserConnector
{
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.2")]
    [System.ServiceModel.ServiceContractAttribute(Namespace="http://service.csap.cs.com.tr/", ConfigurationName="EFinansUserConnector.UserService")]
    public interface UserService
    {
        
        // CODEGEN:  ad alanındaki userId öğe adı sıfırlanabilir olarak işaretlenmediğinden, ileti anlaşması oluşturuluyor
        [System.ServiceModel.OperationContractAttribute(Action="http://service.csap.cs.com.tr/UserService/wsLoginRequest", ReplyAction="http://service.csap.cs.com.tr/UserService/wsLoginResponse")]
        EFinansUserConnector.wsLoginResponse wsLogin(EFinansUserConnector.wsLoginRequest request);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://service.csap.cs.com.tr/UserService/wsLoginRequest", ReplyAction="http://service.csap.cs.com.tr/UserService/wsLoginResponse")]
        System.Threading.Tasks.Task<EFinansUserConnector.wsLoginResponse> wsLoginAsync(EFinansUserConnector.wsLoginRequest request);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://service.csap.cs.com.tr/UserService/logoutRequest", ReplyAction="http://service.csap.cs.com.tr/UserService/logoutResponse")]
        void logout();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://service.csap.cs.com.tr/UserService/logoutRequest", ReplyAction="http://service.csap.cs.com.tr/UserService/logoutResponse")]
        System.Threading.Tasks.Task logoutAsync();
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.2")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class wsLoginRequest
    {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="wsLogin", Namespace="http://service.csap.cs.com.tr/", Order=0)]
        public EFinansUserConnector.wsLoginRequestBody Body;
        
        public wsLoginRequest()
        {
        }
        
        public wsLoginRequest(EFinansUserConnector.wsLoginRequestBody Body)
        {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.2")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="")]
    public partial class wsLoginRequestBody
    {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string userId;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=1)]
        public string password;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=2)]
        public string lang;
        
        public wsLoginRequestBody()
        {
        }
        
        public wsLoginRequestBody(string userId, string password, string lang)
        {
            this.userId = userId;
            this.password = password;
            this.lang = lang;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.2")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class wsLoginResponse
    {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="wsLoginResponse", Namespace="http://service.csap.cs.com.tr/", Order=0)]
        public EFinansUserConnector.wsLoginResponseBody Body;
        
        public wsLoginResponse()
        {
        }
        
        public wsLoginResponse(EFinansUserConnector.wsLoginResponseBody Body)
        {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.2")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute()]
    public partial class wsLoginResponseBody
    {
        
        public wsLoginResponseBody()
        {
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.2")]
    public interface UserServiceChannel : EFinansUserConnector.UserService, System.ServiceModel.IClientChannel
    {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.2")]
    public partial class UserServiceClient : System.ServiceModel.ClientBase<EFinansUserConnector.UserService>, EFinansUserConnector.UserService
    {
        
        /// <summary>
        /// Hizmet uç noktasını yapılandırmak için bu kısmi metodu uygulayın.
        /// </summary>
        /// <param name="serviceEndpoint">Yapılandırılacak uç nokta</param>
        /// <param name="clientCredentials">İstemci kimlik bilgileri</param>
        static partial void ConfigureEndpoint(System.ServiceModel.Description.ServiceEndpoint serviceEndpoint, System.ServiceModel.Description.ClientCredentials clientCredentials);
        
        public UserServiceClient() : 
                base(UserServiceClient.GetDefaultBinding(), UserServiceClient.GetDefaultEndpointAddress())
        {
            this.Endpoint.Name = EndpointConfiguration.UserServicePort.ToString();
            ConfigureEndpoint(this.Endpoint, this.ClientCredentials);
        }
        
        public UserServiceClient(EndpointConfiguration endpointConfiguration) : 
                base(UserServiceClient.GetBindingForEndpoint(endpointConfiguration), UserServiceClient.GetEndpointAddress(endpointConfiguration))
        {
            this.Endpoint.Name = endpointConfiguration.ToString();
            ConfigureEndpoint(this.Endpoint, this.ClientCredentials);
        }
        
        public UserServiceClient(EndpointConfiguration endpointConfiguration, string remoteAddress) : 
                base(UserServiceClient.GetBindingForEndpoint(endpointConfiguration), new System.ServiceModel.EndpointAddress(remoteAddress))
        {
            this.Endpoint.Name = endpointConfiguration.ToString();
            ConfigureEndpoint(this.Endpoint, this.ClientCredentials);
        }
        
        public UserServiceClient(EndpointConfiguration endpointConfiguration, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(UserServiceClient.GetBindingForEndpoint(endpointConfiguration), remoteAddress)
        {
            this.Endpoint.Name = endpointConfiguration.ToString();
            ConfigureEndpoint(this.Endpoint, this.ClientCredentials);
        }
        
        public UserServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress)
        {
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        EFinansUserConnector.wsLoginResponse EFinansUserConnector.UserService.wsLogin(EFinansUserConnector.wsLoginRequest request)
        {
            return base.Channel.wsLogin(request);
        }
        
        public void wsLogin(string userId, string password, string lang)
        {
            EFinansUserConnector.wsLoginRequest inValue = new EFinansUserConnector.wsLoginRequest();
            inValue.Body = new EFinansUserConnector.wsLoginRequestBody();
            inValue.Body.userId = userId;
            inValue.Body.password = password;
            inValue.Body.lang = lang;
            EFinansUserConnector.wsLoginResponse retVal = ((EFinansUserConnector.UserService)(this)).wsLogin(inValue);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<EFinansUserConnector.wsLoginResponse> EFinansUserConnector.UserService.wsLoginAsync(EFinansUserConnector.wsLoginRequest request)
        {
            return base.Channel.wsLoginAsync(request);
        }
        
        public System.Threading.Tasks.Task<EFinansUserConnector.wsLoginResponse> wsLoginAsync(string userId, string password, string lang)
        {
            EFinansUserConnector.wsLoginRequest inValue = new EFinansUserConnector.wsLoginRequest();
            inValue.Body = new EFinansUserConnector.wsLoginRequestBody();
            inValue.Body.userId = userId;
            inValue.Body.password = password;
            inValue.Body.lang = lang;
            return ((EFinansUserConnector.UserService)(this)).wsLoginAsync(inValue);
        }
        
        public void logout()
        {
            base.Channel.logout();
        }
        
        public System.Threading.Tasks.Task logoutAsync()
        {
            return base.Channel.logoutAsync();
        }
        
        public virtual System.Threading.Tasks.Task OpenAsync()
        {
            return System.Threading.Tasks.Task.Factory.FromAsync(((System.ServiceModel.ICommunicationObject)(this)).BeginOpen(null, null), new System.Action<System.IAsyncResult>(((System.ServiceModel.ICommunicationObject)(this)).EndOpen));
        }
        
        public virtual System.Threading.Tasks.Task CloseAsync()
        {
            return System.Threading.Tasks.Task.Factory.FromAsync(((System.ServiceModel.ICommunicationObject)(this)).BeginClose(null, null), new System.Action<System.IAsyncResult>(((System.ServiceModel.ICommunicationObject)(this)).EndClose));
        }
        
        private static System.ServiceModel.Channels.Binding GetBindingForEndpoint(EndpointConfiguration endpointConfiguration)
        {
            if ((endpointConfiguration == EndpointConfiguration.UserServicePort))
            {
                System.ServiceModel.BasicHttpBinding result = new System.ServiceModel.BasicHttpBinding();
                result.MaxBufferSize = int.MaxValue;
                result.ReaderQuotas = System.Xml.XmlDictionaryReaderQuotas.Max;
                result.MaxReceivedMessageSize = int.MaxValue;
                result.AllowCookies = true;
                result.Security.Mode = System.ServiceModel.BasicHttpSecurityMode.Transport;
                return result;
            }
            throw new System.InvalidOperationException(string.Format("\'{0}\' adlı uç nokta bulunamadı.", endpointConfiguration));
        }
        
        private static System.ServiceModel.EndpointAddress GetEndpointAddress(EndpointConfiguration endpointConfiguration)
        {
            if ((endpointConfiguration == EndpointConfiguration.UserServicePort))
            {
                return new System.ServiceModel.EndpointAddress("https://efaturaconnector.efinans.com.tr/connector/ws/userService");
            }
            throw new System.InvalidOperationException(string.Format("\'{0}\' adlı uç nokta bulunamadı.", endpointConfiguration));
        }
        
        private static System.ServiceModel.Channels.Binding GetDefaultBinding()
        {
            return UserServiceClient.GetBindingForEndpoint(EndpointConfiguration.UserServicePort);
        }
        
        private static System.ServiceModel.EndpointAddress GetDefaultEndpointAddress()
        {
            return UserServiceClient.GetEndpointAddress(EndpointConfiguration.UserServicePort);
        }
        
        public enum EndpointConfiguration
        {
            
            UserServicePort,
        }
    }
}

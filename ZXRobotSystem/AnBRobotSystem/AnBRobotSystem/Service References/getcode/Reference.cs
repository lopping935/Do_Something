//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.42000
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------

namespace AnBRobotSystem.getcode {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(Namespace="http://192.168.100.24:9801/webservices/", ConfigurationName="getcode.WebServicezkd_pcdSoap")]
    public interface WebServicezkd_pcdSoap {
        
        // CODEGEN: 命名空间 http://192.168.100.24:9801/webservices/ 的元素名称 text 以后生成的消息协定未标记为 nillable
        [System.ServiceModel.OperationContractAttribute(Action="http://192.168.100.24:9801/webservices/GetJiaMistr", ReplyAction="*")]
        AnBRobotSystem.getcode.GetJiaMistrResponse GetJiaMistr(AnBRobotSystem.getcode.GetJiaMistrRequest request);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://192.168.100.24:9801/webservices/GetJiaMistr", ReplyAction="*")]
        System.Threading.Tasks.Task<AnBRobotSystem.getcode.GetJiaMistrResponse> GetJiaMistrAsync(AnBRobotSystem.getcode.GetJiaMistrRequest request);
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class GetJiaMistrRequest {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="GetJiaMistr", Namespace="http://192.168.100.24:9801/webservices/", Order=0)]
        public AnBRobotSystem.getcode.GetJiaMistrRequestBody Body;
        
        public GetJiaMistrRequest() {
        }
        
        public GetJiaMistrRequest(AnBRobotSystem.getcode.GetJiaMistrRequestBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://192.168.100.24:9801/webservices/")]
    public partial class GetJiaMistrRequestBody {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string text;
        
        public GetJiaMistrRequestBody() {
        }
        
        public GetJiaMistrRequestBody(string text) {
            this.text = text;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class GetJiaMistrResponse {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="GetJiaMistrResponse", Namespace="http://192.168.100.24:9801/webservices/", Order=0)]
        public AnBRobotSystem.getcode.GetJiaMistrResponseBody Body;
        
        public GetJiaMistrResponse() {
        }
        
        public GetJiaMistrResponse(AnBRobotSystem.getcode.GetJiaMistrResponseBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://192.168.100.24:9801/webservices/")]
    public partial class GetJiaMistrResponseBody {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string GetJiaMistrResult;
        
        public GetJiaMistrResponseBody() {
        }
        
        public GetJiaMistrResponseBody(string GetJiaMistrResult) {
            this.GetJiaMistrResult = GetJiaMistrResult;
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface WebServicezkd_pcdSoapChannel : AnBRobotSystem.getcode.WebServicezkd_pcdSoap, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class WebServicezkd_pcdSoapClient : System.ServiceModel.ClientBase<AnBRobotSystem.getcode.WebServicezkd_pcdSoap>, AnBRobotSystem.getcode.WebServicezkd_pcdSoap {
        
        public WebServicezkd_pcdSoapClient() {
        }
        
        public WebServicezkd_pcdSoapClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public WebServicezkd_pcdSoapClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public WebServicezkd_pcdSoapClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public WebServicezkd_pcdSoapClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        AnBRobotSystem.getcode.GetJiaMistrResponse AnBRobotSystem.getcode.WebServicezkd_pcdSoap.GetJiaMistr(AnBRobotSystem.getcode.GetJiaMistrRequest request) {
            return base.Channel.GetJiaMistr(request);
        }
        
        public string GetJiaMistr(string text) {
            AnBRobotSystem.getcode.GetJiaMistrRequest inValue = new AnBRobotSystem.getcode.GetJiaMistrRequest();
            inValue.Body = new AnBRobotSystem.getcode.GetJiaMistrRequestBody();
            inValue.Body.text = text;
            AnBRobotSystem.getcode.GetJiaMistrResponse retVal = ((AnBRobotSystem.getcode.WebServicezkd_pcdSoap)(this)).GetJiaMistr(inValue);
            return retVal.Body.GetJiaMistrResult;
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<AnBRobotSystem.getcode.GetJiaMistrResponse> AnBRobotSystem.getcode.WebServicezkd_pcdSoap.GetJiaMistrAsync(AnBRobotSystem.getcode.GetJiaMistrRequest request) {
            return base.Channel.GetJiaMistrAsync(request);
        }
        
        public System.Threading.Tasks.Task<AnBRobotSystem.getcode.GetJiaMistrResponse> GetJiaMistrAsync(string text) {
            AnBRobotSystem.getcode.GetJiaMistrRequest inValue = new AnBRobotSystem.getcode.GetJiaMistrRequest();
            inValue.Body = new AnBRobotSystem.getcode.GetJiaMistrRequestBody();
            inValue.Body.text = text;
            return ((AnBRobotSystem.getcode.WebServicezkd_pcdSoap)(this)).GetJiaMistrAsync(inValue);
        }
    }
}

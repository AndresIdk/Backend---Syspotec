using Microsoft.OpenApi.Any;

namespace Syspotec.Api.Responses
{
    public class ApiResponse
    {
        public string opc1 { get; set; }
        public string opc2 { get; set; }
        public bool state { get; set; }

        public ApiResponse(string[] data, bool state)
        {
            this.opc1 = data[0];
            this.opc2 = data[1];
            this.state = state;
        }

        public object getResponse()
        {
            var objResponse = new
            {
                status = this.state,
                message = this.state ? this.opc1 : this.opc2
            };
            return objResponse;
        }


}
}

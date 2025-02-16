using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace AU_Framework.WebAPI.Middleware
{
    public sealed class ErrorResult: ErrorStatusCode
    {
        public string Message {  get; set; }
    }

    public class ErrorStatusCode
    {
        public int StatusCode { get; set; }
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
    public sealed class ValidationErrorDetails: ErrorStatusCode
        {
        public IEnumerable<string> Errors { get; set; }
    }
}

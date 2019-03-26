using System.Net;

namespace ContentApi.JSON
{
    public interface IJsonResponse
    {
        IJsonData Data { get; set; }
        HttpStatusCode StatusCode { get; set; }
    }
}

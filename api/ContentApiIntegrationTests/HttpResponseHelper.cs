using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ContentApiIntegrationTests
{
    public static class HttpResponseHelper
    {
        public static async Task<T> ReadBody<T>(HttpResponseMessage httpResponse)
        {
            var responseJson = await httpResponse.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(responseJson);
        }
    }
}

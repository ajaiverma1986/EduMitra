using EDUMITRA.Datamodel.Shared;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace EDUMITRA.Commonlib.Utility
{
    public static class HttpClientHelper
    {
        public static HttpClient _httpClient = new HttpClient();

        public static async Task<SimpleResponse> HttpGet(string URL)
        {
            SimpleResponse ret = new SimpleResponse();
            using (HttpRequestMessage msg = new HttpRequestMessage(HttpMethod.Get, URL))
            {
                using (HttpResponseMessage resp = await _httpClient.SendAsync(msg))
                {
                    if (resp.IsSuccessStatusCode)
                    {
                        ret.Result = resp.Content.ReadAsStringAsync().Result;
                        return ret;
                    }
                    else
                    {
                        ErrorResponse err = new ErrorResponse(resp.ReasonPhrase);
                        ret.SetError(err);
                        return ret;
                    }
                }
            }
        }
    }
}

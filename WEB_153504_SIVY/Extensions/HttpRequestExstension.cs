using Azure.Core;

namespace WEB_153504_SIVY.Extensions
{
    public static class HttpRequestExstension
    {
        public static bool IsAjaxRequest(this HttpRequest request)
        {
            return request.Headers["x-requested-with"].ToString().ToLower().Equals("xmlhttprequest");
        }
    }
}

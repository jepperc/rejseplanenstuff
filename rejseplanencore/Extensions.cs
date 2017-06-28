using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.WebUtilities;
using System.Xml.Serialization;
using System;

namespace rejseplanencore
{
    public class RestRequest
    {
        private Dictionary<string,string> _queryParams = new Dictionary<string,string>();
        public IDictionary<string,string> QueryParams => _queryParams;
        public string Url { get; }
        public RestRequest(string url)
        {
            Url = url;
        }

        public RestRequest AddQueryParameter(string key, string value)
        {
            _queryParams.Add(key, value);
            return this;
        }
    }

    public class RestResponse<T>    
    {
        public RestResponse(T data)
        {
            Data = data;
        }
        public T Data { get; }
    }
    internal static class Extensions
    {
        internal static int ToInt(this bool boolean)
        {
            return boolean ? 1 : 0;
        }

        internal static string ToIntString(this bool boolean)
        {
            return boolean.ToInt()
                          .ToString();
        }

        internal static async Task<RestResponse<T>> Execute<T>(this System.Net.Http.HttpClient client, RestRequest request) where T:class
        {
            var x = new XmlSerializer(typeof(T));
            string url = request.Url;
            if(request.QueryParams != null)
                url = QueryHelpers.AddQueryString(url, request.QueryParams);
                        
            var s = await client.GetStreamAsync(url);
            var str = await client.GetStringAsync(url);
            Console.WriteLine(str);
            return new RestResponse<T>((T)x.Deserialize(s));
        }
    }
}

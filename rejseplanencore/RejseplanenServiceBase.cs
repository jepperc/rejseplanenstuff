using System;
using System.Net;
using System.Net.Http;
namespace rejseplanencore
{
    public abstract class RejseplanenServiceBase
    {
        protected HttpClient Client;
        protected const string Url = @"http://xmlopen.rejseplanen.dk/bin/rest.exe/";

        protected internal RejseplanenServiceBase()
        {
            var c = new HttpClient();
            c.BaseAddress = new Uri(Url);            
            
            Client = c;
        }
    }
}
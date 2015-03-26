namespace Bukkit.JSONAPI
{
    using Bukkit.JSONAPI.Entities;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Net;
    using System.Net.Cache;
    using System.Security.Cryptography;
    using System.Text;
    using System.Threading.Tasks;
    using System.Web;

    public abstract class ServiceBase : IDisposable
    {
        public ServiceBase(string uri, string user, string pass)
            : this(new Uri(uri), user, pass)
        {
        }

        public ServiceBase(Uri uri, string user, string pass)
        {
            this.BaseUri = uri;
            this.User = user;
            this.Pass = pass;
        }

        protected Uri ApiUri { get { return new Uri(this.BaseUri, "api/2/call"); } }

        protected Uri BaseUri { get; private set; }


        protected string User { get; private set; }

        protected string Pass { get; private set; }

        protected virtual TResponse Request<TResponse>(RequestBase data)
            where TResponse : class
        {
            data.Key = this.GetKey(data.Name);
            data.User = this.User;

            var dataJson = HttpUtility.UrlEncode(JsonConvert.SerializeObject(data, Formatting.None));
            var request = WebRequest.Create(new Uri(this.ApiUri, string.Format("?json={0}", dataJson))) as HttpWebRequest;
            if (request == null) throw new ArgumentNullException("WebRequest.Create()");

            request.Accept = "application/json";
            request.AllowAutoRedirect = true;
            request.ContentType = "application/json";
            request.CachePolicy = new HttpRequestCachePolicy(HttpRequestCacheLevel.NoCacheNoStore);
            request.Method = "GET";
            request.Timeout = 3000;
            request.UseDefaultCredentials = true;

            try
            {
                using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
                {
                    if (response == null) throw new ArgumentNullException("Request.GetResponse()");

                    using (var responseStream = new StreamReader(response.GetResponseStream()))
                    {
                        var responseString = responseStream.ReadToEnd();
                        var responseBase = JsonConvert.DeserializeObject<ResponseBase>(responseString);

                        if (!responseBase.IsSuccess) throw new Exception(responseBase.Error.Value<string>("message"));
                        return JsonConvert.DeserializeObject<TResponse>(responseBase.Success.ToString());
                    }
                }
            }
            catch (WebException exception)
            {
                if (exception.Response == null) throw;

                JToken exceptionResponseObject = null;
                var exceptionResponseString = new StreamReader(exception.Response.GetResponseStream()).ReadToEnd();
                try
                {
                    var exceptionResponseBase = JsonConvert.DeserializeObject<ResponseBase>(exceptionResponseString);
                    if (exceptionResponseBase != null) exceptionResponseObject = (JToken)exceptionResponseBase.Error;
                }
                catch { }

                if (exceptionResponseString == null || exceptionResponseObject == null) throw;
                throw new Exception(exceptionResponseObject.Value<string>("message"), exception);
            }
        }

        protected virtual async Task<TResponse> RequestAsync<TResponse>(RequestBase data)
            where TResponse : class
        {
            data.Key = this.GetKey(data.Name);
            data.User = this.User;

            var dataJson = HttpUtility.UrlEncode(JsonConvert.SerializeObject(data, Formatting.None));
            var request = WebRequest.Create(new Uri(this.ApiUri, string.Format("?json={0}", dataJson))) as HttpWebRequest;
            if (request == null) throw new ArgumentNullException("WebRequest.Create()");

            request.Accept = "application/json";
            request.AllowAutoRedirect = true;
            request.ContentType = "application/json";
            request.CachePolicy = new HttpRequestCachePolicy(HttpRequestCacheLevel.NoCacheNoStore);
            request.Method = "GET";
            request.Timeout = 3000;
            request.UseDefaultCredentials = true;

            try
            {
                using (HttpWebResponse response = await request.GetResponseAsync() as HttpWebResponse)
                {
                    if (response == null) throw new ArgumentNullException("Request.GetResponse()");

                    using (var responseStream = new StreamReader(response.GetResponseStream()))
                    {
                        var responseString = responseStream.ReadToEnd();
                        var responseBase = JsonConvert.DeserializeObject<ResponseBase>(responseString);

                        if (!responseBase.IsSuccess) throw new Exception(responseBase.Error.Value<string>("message"));
                        return JsonConvert.DeserializeObject<TResponse>(responseBase.Success.ToString());
                    }
                }
            }
            catch (WebException exception)
            {
                if (exception.Response == null) throw;

                JToken exceptionResponseObject = null;
                var exceptionResponseString = new StreamReader(exception.Response.GetResponseStream()).ReadToEnd();
                try
                {
                    var exceptionResponseBase = JsonConvert.DeserializeObject<ResponseBase>(exceptionResponseString);
                    if (exceptionResponseBase != null) exceptionResponseObject = (JToken)exceptionResponseBase.Error;
                }
                catch { }

                if (exceptionResponseString == null || exceptionResponseObject == null) throw;
                throw new Exception(exceptionResponseObject.Value<string>("message"), exception);
            }
        }

        protected virtual JToken Request(string name, IEnumerable<object> arguments = null)
        {
            var data = new JObject();
            data.Add("key", new JValue(this.GetKey(name)));
            data.Add("name", new JValue(name));
            data.Add("username", new JValue(this.User));
            if (arguments != null) data.Add("arguments", new JArray(arguments));

            var dataJson = HttpUtility.UrlEncode(JsonConvert.SerializeObject(data, Formatting.None));
            var request = WebRequest.Create(new Uri(this.ApiUri, string.Format("?json={0}", dataJson))) as HttpWebRequest;
            if (request == null) throw new ArgumentNullException("WebRequest.Create()");

            request.Accept = "application/json";
            request.AllowAutoRedirect = true;
            request.ContentType = "application/json";
            request.CachePolicy = new HttpRequestCachePolicy(HttpRequestCacheLevel.NoCacheNoStore);
            request.Method = "GET";
            request.Timeout = 3000;
            request.UseDefaultCredentials = true;

            try
            {
                using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
                {
                    if (response == null) throw new ArgumentNullException("Request.GetResponse()");

                    using (var responseStream = new StreamReader(response.GetResponseStream()))
                    {
                        var responseString = responseStream.ReadToEnd();
                        var responseBase = JsonConvert.DeserializeObject<ResponseBase>(responseString);

                        if (!responseBase.IsSuccess) throw new Exception(responseBase.Error.Value<string>("message"));
                        return (JToken)responseBase.Success;
                    }
                }
            }
            catch (WebException exception)
            {
                if (exception.Response == null) throw;

                JToken exceptionResponseObject = null;
                var exceptionResponseString = new StreamReader(exception.Response.GetResponseStream()).ReadToEnd();
                try
                {
                    var exceptionResponseBase = JsonConvert.DeserializeObject<ResponseBase>(exceptionResponseString);
                    if (exceptionResponseBase != null) exceptionResponseObject = (JToken)exceptionResponseBase.Error;
                }
                catch { }

                if (exceptionResponseString == null || exceptionResponseObject == null) throw;
                throw new Exception(exceptionResponseObject.Value<string>("message"), exception);
            }
        }

        protected virtual async Task<JToken> RequestAsync(string name, IEnumerable<object> arguments = null)
        {
            var data = new JObject();
            data.Add("key", new JValue(this.GetKey(name)));
            data.Add("name", new JValue(name));
            data.Add("username", new JValue(this.User));
            if (arguments != null) data.Add("arguments", new JArray(arguments));

            var dataJson = HttpUtility.UrlEncode(JsonConvert.SerializeObject(data, Formatting.None));
            var request = WebRequest.Create(new Uri(this.ApiUri, string.Format("?json={0}", dataJson))) as HttpWebRequest;
            if (request == null) throw new ArgumentNullException("WebRequest.Create()");

            request.Accept = "application/json";
            request.AllowAutoRedirect = true;
            request.ContentType = "application/json";
            request.CachePolicy = new HttpRequestCachePolicy(HttpRequestCacheLevel.NoCacheNoStore);
            request.Method = "GET";
            request.Timeout = 3000;
            request.UseDefaultCredentials = true;

            try
            {
                using (HttpWebResponse response = await request.GetResponseAsync() as HttpWebResponse)
                {
                    if (response == null) throw new ArgumentNullException("Request.GetResponse()");

                    using (var responseStream = new StreamReader(response.GetResponseStream()))
                    {
                        var responseString = responseStream.ReadToEnd();
                        var responseBase = JsonConvert.DeserializeObject<ResponseBase>(responseString);

                        if (!responseBase.IsSuccess) throw new Exception(responseBase.Error.Value<string>("message"));
                        return (JToken)responseBase.Success;
                    }
                }
            }
            catch (WebException exception)
            {
                if (exception.Response == null) throw;

                JToken exceptionResponseObject = null;
                var exceptionResponseString = new StreamReader(exception.Response.GetResponseStream()).ReadToEnd();
                try
                {
                    var exceptionResponseBase = JsonConvert.DeserializeObject<ResponseBase>(exceptionResponseString);
                    if (exceptionResponseBase != null) exceptionResponseObject = (JToken)exceptionResponseBase.Error;
                }
                catch { }

                if (exceptionResponseString == null || exceptionResponseObject == null) throw;
                throw new Exception(exceptionResponseObject.Value<string>("message"), exception);
            }
        }

        protected virtual string GetKey(string name)
        {
            var input = string.Concat(this.User, name, this.Pass);

            var hash = new SHA256Managed();
            var hashed = hash.ComputeHash(Encoding.UTF8.GetBytes(input));
            return BitConverter.ToString(hashed).Replace("-", "").ToLower();
        }

        public virtual void Dispose()
        {
            this.BaseUri = null;
            this.User = null;
            this.Pass = null;
        }
    }
}

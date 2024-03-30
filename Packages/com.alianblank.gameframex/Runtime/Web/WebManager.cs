using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using GameFrameX.Runtime;

namespace GameFrameX.Web
{
    public partial class WebManager : GameFrameworkModule, IWebManager
    {
        private readonly StringBuilder m_StringBuilder = new StringBuilder(256);
        private readonly Queue<WebData> m_WaitingQueue = new Queue<WebData>(256);
        private readonly List<WebData> m_SendingList = new List<WebData>(16);
        private readonly MemoryStream m_MemoryStream;

        public WebManager()
        {
            MaxConnectionPerServer = 8;
            m_MemoryStream = new MemoryStream();
            RequestTimeout = TimeSpan.FromSeconds(5);
        }

        public int MaxConnectionPerServer { get; set; }

        public TimeSpan RequestTimeout { get; set; }

        internal override void Update(float elapseSeconds, float realElapseSeconds)
        {
            lock (m_StringBuilder)
            {
                if (m_SendingList.Count < MaxConnectionPerServer)
                {
                    if (m_WaitingQueue.Count > 0)
                    {
                        var webData = m_WaitingQueue.Dequeue();

                        if (webData.UniTaskCompletionStringSource != null)
                        {
                            MakeStringRequest(webData);
                        }
                        else
                        {
                            MakeBytesRequest(webData);
                        }

                        m_SendingList.Add(webData);
                    }
                }
            }
        }

        internal override void Shutdown()
        {
            while (m_WaitingQueue.Count > 0)
            {
                var webData = m_WaitingQueue.Dequeue();
                webData.UniTaskCompletionBytesSource?.TrySetCanceled();
                webData.UniTaskCompletionStringSource?.TrySetCanceled();
            }

            m_WaitingQueue.Clear();
            while (m_SendingList.Count > 0)
            {
                var webData = m_SendingList[0];
                m_SendingList.RemoveAt(0);
                webData.UniTaskCompletionBytesSource?.TrySetCanceled();
                webData.UniTaskCompletionStringSource?.TrySetCanceled();
            }

            m_SendingList.Clear();
            m_MemoryStream.Dispose();
        }

        /// <summary>
        /// 发送Get 请求
        /// </summary>
        /// <param name="url">请求地址</param>
        /// <returns></returns>
        public Task<string> GetToString(string url)
        {
            return GetToString(url, null, null);
        }

        /// <summary>
        /// 发送Get 请求
        /// </summary>
        /// <param name="url">请求地址</param>
        /// <returns></returns>
        public Task<byte[]> GetToBytes(string url)
        {
            return GetToBytes(url, null, null);
        }

        /// <summary>
        /// 发送Get 请求
        /// </summary>
        /// <param name="url">请求地址</param>
        /// <param name="queryString">请求参数</param>
        /// <returns></returns>
        public Task<string> GetToString(string url, Dictionary<string, string> queryString)
        {
            return GetToString(url, queryString, null);
        }

        /// <summary>
        /// 发送Get 请求
        /// </summary>
        /// <param name="url">请求地址</param>
        /// <param name="queryString">请求参数</param>
        /// <returns></returns>
        public Task<byte[]> GetToBytes(string url, Dictionary<string, string> queryString)
        {
            return GetToBytes(url, queryString, null);
        }

        /// <summary>
        /// 发送Get 请求
        /// </summary>
        /// <param name="url">请求地址</param>
        /// <param name="queryString">请求参数</param>
        /// <param name="header">请求头</param>
        /// <returns></returns>
        public Task<string> GetToString(string url, Dictionary<string, string> queryString, Dictionary<string, string> header)
        {
            var uniTaskCompletionSource = new TaskCompletionSource<string>();
            url = UrlHandler(url, queryString);

            WebData webData = new WebData(url, header, true, uniTaskCompletionSource);
            m_WaitingQueue.Enqueue(webData);
            return uniTaskCompletionSource.Task;
        }

        private async void MakeStringRequest(WebData webData)
        {
            try
            {
                HttpWebRequest request = WebRequest.CreateHttp(webData.URL);
                request.Method = webData.IsGet ? WebRequestMethods.Http.Get : WebRequestMethods.Http.Post;
                request.Timeout = (int)RequestTimeout.TotalMilliseconds; // 设置请求超时时间
                if (webData.Form != null && webData.Form.Count > 0)
                {
                    request.ContentType = "application/json";
                    string body = Utility.Json.ToJson(webData.Form);
                    byte[] postData = Encoding.UTF8.GetBytes(body);
                    request.ContentLength = postData.Length;
                    using (Stream requestStream = request.GetRequestStream())
                    {
                        await requestStream.WriteAsync(postData, 0, postData.Length);
                    }
                }

                if (webData.Header != null && webData.Header.Count > 0)
                {
                    foreach (var kv in webData.Header)
                    {
                        request.Headers[kv.Key] = kv.Value;
                    }
                }

                using (HttpWebResponse response = (HttpWebResponse)await request.GetResponseAsync())
                {
                    using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                    {
                        string content = await reader.ReadToEndAsync();
                        webData.UniTaskCompletionStringSource.SetResult(content);
                        m_SendingList.Remove(webData);
                    }
                }
            }
            catch (WebException e)
            {
                // 捕获超时异常
                if (e.Status == WebExceptionStatus.Timeout)
                {
                    webData.UniTaskCompletionStringSource.SetException(new TimeoutException());
                    m_SendingList.Remove(webData);
                    return;
                }

                webData.UniTaskCompletionStringSource.SetException(e);
                m_SendingList.Remove(webData);
            }
            catch (IOException e)
            {
                webData.UniTaskCompletionStringSource.SetException(e);
                m_SendingList.Remove(webData);
            }
            catch (Exception e)
            {
                Console.WriteLine("Unexpected exception caught: " + e.Message);
                webData.UniTaskCompletionStringSource.SetException(e);
                m_SendingList.Remove(webData);
            }
        }

        private async void MakeBytesRequest(WebData webData)
        {
            try
            {
                HttpWebRequest request = WebRequest.CreateHttp(webData.URL);
                request.Method = webData.IsGet ? WebRequestMethods.Http.Get : WebRequestMethods.Http.Post;
                request.Timeout = (int)RequestTimeout.TotalMilliseconds; // 设置请求超时时间
                if (webData.Header != null && webData.Header.Count > 0)
                {
                    foreach (var kv in webData.Header)
                    {
                        request.Headers[kv.Key] = kv.Value;
                    }
                }

                if (webData.Form != null && webData.Form.Count > 0)
                {
                    request.ContentType = "application/json";
                    string body = Utility.Json.ToJson(webData.Form);
                    byte[] postData = Encoding.UTF8.GetBytes(body);
                    request.ContentLength = postData.Length;
                    using (Stream requestStream = request.GetRequestStream())
                    {
                        await requestStream.WriteAsync(postData, 0, postData.Length);
                    }
                }

                using (HttpWebResponse response = (HttpWebResponse)await request.GetResponseAsync())
                {
                    using (Stream responseStream = response.GetResponseStream())
                    {
                        await responseStream.CopyToAsync(m_MemoryStream);
                        webData.UniTaskCompletionBytesSource.SetResult(m_MemoryStream.ToArray()); // 将流的内容复制到内存流中并转换为byte数组 
                    }
                }
            }
            catch (WebException e)
            {
                // 捕获超时异常
                if (e.Status == WebExceptionStatus.Timeout)
                {
                    webData.UniTaskCompletionBytesSource.SetException(new TimeoutException());
                    m_SendingList.Remove(webData);
                    return;
                }

                webData.UniTaskCompletionBytesSource.SetException(e);
                m_SendingList.Remove(webData);
            }
            catch (IOException e)
            {
                webData.UniTaskCompletionBytesSource.SetException(e);
                m_SendingList.Remove(webData);
            }
            catch (Exception e)
            {
                webData.UniTaskCompletionBytesSource.SetException(e);
                m_SendingList.Remove(webData);
            }
        }

        /// <summary>
        /// 发送Get 请求
        /// </summary>
        /// <param name="url">请求地址</param>
        /// <param name="queryString">请求参数</param>
        /// <param name="header">请求头</param>
        /// <returns></returns>
        public Task<byte[]> GetToBytes(string url, Dictionary<string, string> queryString, Dictionary<string, string> header)
        {
            var uniTaskCompletionSource = new TaskCompletionSource<byte[]>();
            url = UrlHandler(url, queryString);

            WebData webData = new WebData(url, header, true, uniTaskCompletionSource);
            m_WaitingQueue.Enqueue(webData);
            return uniTaskCompletionSource.Task;
        }


        /// <summary>
        /// 发送Post 请求
        /// </summary>
        /// <param name="url">请求地址</param>
        /// <param name="from">请求参数</param>
        /// <returns></returns>
        public Task<string> PostToString(string url, Dictionary<string, string> from)
        {
            return PostToString(url, from, null, null);
        }

        /// <summary>
        /// 发送Post 请求
        /// </summary>
        /// <param name="url">请求地址</param>
        /// <param name="from">请求参数</param>
        /// <returns></returns>
        public Task<byte[]> PostToBytes(string url, Dictionary<string, string> from)
        {
            return PostToBytes(url, from, null, null);
        }

        /// <summary>
        /// 发送Post 请求
        /// </summary>
        /// <param name="url">请求地址</param>
        /// <param name="from">表单请求参数</param>
        /// <param name="queryString">URl请求参数</param>
        /// <returns></returns>
        public Task<string> PostToString(string url, Dictionary<string, string> from, Dictionary<string, string> queryString)
        {
            return PostToString(url, from, queryString, null);
        }

        /// <summary>
        /// 发送Post 请求
        /// </summary>
        /// <param name="url">请求地址</param>
        /// <param name="from">表单请求参数</param>
        /// <param name="queryString">URl请求参数</param>
        /// <returns></returns>
        public Task<byte[]> PostToBytes(string url, Dictionary<string, string> from, Dictionary<string, string> queryString)
        {
            return PostToBytes(url, from, queryString, null);
        }


        /// <summary>
        /// 发送Post 请求
        /// </summary>
        /// <param name="url">请求地址</param>
        /// <param name="from">表单请求参数</param>
        /// <param name="queryString">URl请求参数</param>
        /// <param name="header">请求头</param>
        /// <returns></returns>
        public Task<string> PostToString(string url, Dictionary<string, string> from, Dictionary<string, string> queryString, Dictionary<string, string> header)
        {
            var uniTaskCompletionSource = new TaskCompletionSource<string>();
            url = UrlHandler(url, queryString);

            WebData webData = new WebData(url, header, from, uniTaskCompletionSource);
            m_WaitingQueue.Enqueue(webData);
            return uniTaskCompletionSource.Task;
        }

        /// <summary>
        /// 发送Post 请求
        /// </summary>
        /// <param name="url">请求地址</param>
        /// <param name="from">表单请求参数</param>
        /// <param name="queryString">URl请求参数</param>
        /// <param name="header">请求头</param>
        /// <returns></returns>
        public Task<byte[]> PostToBytes(string url, Dictionary<string, string> from, Dictionary<string, string> queryString, Dictionary<string, string> header)
        {
            var uniTaskCompletionSource = new TaskCompletionSource<byte[]>();
            url = UrlHandler(url, queryString);
            WebData webData = new WebData(url, header, from, uniTaskCompletionSource);
            m_WaitingQueue.Enqueue(webData);
            return uniTaskCompletionSource.Task;
        }

        /// <summary>
        /// URL 标准化
        /// </summary>
        /// <param name="url"></param>
        /// <param name="queryString"></param>
        /// <returns></returns>
        private string UrlHandler(string url, Dictionary<string, string> queryString)
        {
            m_StringBuilder.Clear();
            m_StringBuilder.Append((url));
            if (queryString != null && queryString.Count > 0)
            {
                if (!url.EndsWithFast("?"))
                {
                    m_StringBuilder.Append("?");
                }

                foreach (var kv in queryString)
                {
                    m_StringBuilder.AppendFormat("{0}={1}&", kv.Key, kv.Value);
                }

                url = m_StringBuilder.ToString(0, m_StringBuilder.Length - 1);
                m_StringBuilder.Clear();
            }

            return url;
        }
    }
}
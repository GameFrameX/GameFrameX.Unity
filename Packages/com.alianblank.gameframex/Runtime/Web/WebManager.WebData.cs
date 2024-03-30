using System.Collections.Generic;
using System.Threading.Tasks;

namespace GameFrameX.Web
{
    public partial class WebManager
    {
        private class WebData
        {
            public bool IsGet { get; }
            public string URL { get; set; }
            public Dictionary<string, string> Header { get; }
            public Dictionary<string, string> Form { get; }
            public readonly TaskCompletionSource<string> UniTaskCompletionStringSource;
            public readonly TaskCompletionSource<byte[]> UniTaskCompletionBytesSource;

            public WebData(string url, Dictionary<string, string> header, bool isGet, TaskCompletionSource<byte[]> source)
            {
                IsGet = isGet;
                URL = url;
                Header = header;
                UniTaskCompletionBytesSource = source;
            }

            public WebData(string url, Dictionary<string, string> header, bool isGet, TaskCompletionSource<string> source)
            {
                IsGet = isGet;
                URL = url;
                Header = header;
                UniTaskCompletionStringSource = source;
            }

            public WebData(string url, Dictionary<string, string> header, Dictionary<string, string> form, TaskCompletionSource<string> source)
            {
                IsGet = false;
                URL = url;
                Header = header;
                Form = form;
                UniTaskCompletionStringSource = source;
            }

            public WebData(string url, Dictionary<string, string> header, Dictionary<string, string> form, TaskCompletionSource<byte[]> source)
            {
                IsGet = false;
                URL = url;
                Header = header;
                Form = form;
                UniTaskCompletionBytesSource = source;
            }
        }
    }
}
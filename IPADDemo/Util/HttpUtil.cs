using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Cache;
using System.Net.Security;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using System.Text.RegularExpressions;

namespace IPADDemo.Util
{
    class HttpUtil
    {
    }
    public class Http_Helper
    {
        public Http_Helper()
        {
            System.Net.ServicePointManager.DefaultConnectionLimit = 1024;
        }
        public CookieContainer CookieContainers = new CookieContainer();


        public string IE7 = "Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 5.1; InfoPath.2; .NET CLR 2.0.50727; .NET CLR 3.0.04506.648; .NET CLR 3.5.21022; .NET4.0C; .NET4.0E)";

        public string IE8 = "Mozilla/5.0 (Windows NT 6.1; WOW64; Trident/7.0; rv:11.0)";

        private RequestCachePolicy rcp = new RequestCachePolicy(RequestCacheLevel.NoCacheNoStore);

        public bool isUserAgentSet = false;

        public string Cookies { get; set; }


        [DllImport("wininet.dll", CharSet = CharSet.Auto)]
        public static extern bool DeleteUrlCacheEntry(string lpszUrlName);

        [SecurityCritical, SuppressUnmanagedCodeSecurity]
        [DllImport("wininet.dll", CharSet = CharSet.Unicode, ExactSpelling = true, SetLastError = true)]
        internal static extern bool InternetGetCookieExW([In] string Url, [In] string cookieName, [Out] StringBuilder cookieData, [In] [Out] ref uint pchCookieData, uint flags, IntPtr reserved);

        public HttpStatusCode GetResponse(ref string pResponsetext, string pUrl, string pMethod, string pData, string pReferer, int pTime = 10000, string pEncoding = "UTF-8", bool pIsPTHander = true)
        {
            Set_Cookies(pUrl);
            try
            {
                DeleteUrlCacheEntry(pUrl);
                HttpWebRequest pRequest = (HttpWebRequest)WebRequest.Create(pUrl);
                pRequest.KeepAlive = true;
                pRequest.Method = pMethod.ToUpper();
                pRequest.AllowAutoRedirect = true;
                pRequest.CookieContainer = CookieContainers;
                pRequest.Referer = pReferer;
                pRequest.UserAgent = IE8;
                pRequest.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8";
                pRequest.Timeout = pTime;
                pRequest.ReadWriteTimeout = pTime;
                pRequest.CachePolicy = rcp;
                pRequest.Headers.Add("x-requested-with", "XMLHttpRequest");
                //ProtocolVersion
                //KeepAlive
                //Allowautoredirect
                pRequest.ProtocolVersion = HttpVersion.Version10;
                pRequest.KeepAlive = false;


                if (pIsPTHander)
                {
                    PTRequest(ref pRequest);
                }
                Encoding encoding = Encoding.GetEncoding(pEncoding);
                if (pMethod.ToUpper() != "GET" && pData != null)
                {
                    byte[] bytes = encoding.GetBytes(pData);
                    pRequest.ContentLength = (long)bytes.Length;
                    pRequest.ContentType = "application/x-www-form-urlencoded; charset=UTF-8";
                    Stream requestStream = pRequest.GetRequestStream();
                    requestStream.Write(bytes, 0, bytes.Length);
                    requestStream.Close();
                }
                HttpWebResponse httpWebResponse = (HttpWebResponse)pRequest.GetResponse();
                StreamReader streamReader = new StreamReader(httpWebResponse.GetResponseStream(), encoding);
                pResponsetext = streamReader.ReadToEnd();


                pRequest.Abort();
                httpWebResponse.Close();
                 
                return httpWebResponse.StatusCode;
            }
            catch (WebException ex)
            {

                try
                {
                    HttpWebResponse response = (HttpWebResponse)ex.Response;

                    if (response.StatusCode == HttpStatusCode.BadRequest)
                    {
                        using (Stream data = response.GetResponseStream())
                        {
                            using (StreamReader reader = new StreamReader(data))
                            {
                                string text = reader.ReadToEnd();
                                pResponsetext = text;
                                Console.WriteLine(text);
                            }
                        }
                    }
                }
                catch
                {
                    return HttpStatusCode.ExpectationFailed;
                }

                return HttpStatusCode.ExpectationFailed;
            }
        }

        public HttpStatusCode GetResponse_WX(ref string pResponsetext, string pUrl, string pMethod, string pData, string pReferer, int pTime = 10000, string pEncoding = "UTF-8", bool pIsPTHander = true)
        {
            Set_Cookies(pUrl);
            try
            {
                DeleteUrlCacheEntry(pUrl);
                HttpWebRequest pRequest = (HttpWebRequest)WebRequest.Create(pUrl);
                pRequest.KeepAlive = true;
                pRequest.Method = pMethod.ToUpper();
                pRequest.AllowAutoRedirect = true;
                pRequest.CookieContainer = CookieContainers;
                pRequest.Referer = pReferer;
                pRequest.UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/39.0.2171.95 Safari/537.36 MicroMessenger/6.5.2.501 NetType/WIFI WindowsWechat QBCore/3.43.556.400 QQBrowser/9.0.2524.400";
                pRequest.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8";
                pRequest.Timeout = pTime;
                pRequest.ReadWriteTimeout = pTime;
                pRequest.CachePolicy = rcp;
                pRequest.Headers.Add("x-requested-with", "XMLHttpRequest");

               
                //ProtocolVersion
                //KeepAlive
                //Allowautoredirect
                pRequest.ProtocolVersion = HttpVersion.Version10;
                pRequest.KeepAlive = false;


                if (pIsPTHander)
                {
                    PTRequest(ref pRequest);
                }
                Encoding encoding = Encoding.GetEncoding(pEncoding);
                if (pMethod.ToUpper() != "GET" && pData != null)
                {
                    byte[] bytes = encoding.GetBytes(pData);
                    pRequest.ContentLength = (long)bytes.Length;
                    pRequest.ContentType = "application/x-www-form-urlencoded; charset=UTF-8";
                    Stream requestStream = pRequest.GetRequestStream();
                    requestStream.Write(bytes, 0, bytes.Length);
                    requestStream.Close();
                }
                HttpWebResponse httpWebResponse = (HttpWebResponse)pRequest.GetResponse();
                StreamReader streamReader = new StreamReader(httpWebResponse.GetResponseStream(), encoding);
                pResponsetext = streamReader.ReadToEnd();


                pRequest.Abort();
                httpWebResponse.Close();

                return httpWebResponse.StatusCode;
            }
            catch (WebException ex)
            {

                try
                {
                    HttpWebResponse response = (HttpWebResponse)ex.Response;

                    if (response.StatusCode == HttpStatusCode.BadRequest)
                    {
                        using (Stream data = response.GetResponseStream())
                        {
                            using (StreamReader reader = new StreamReader(data))
                            {
                                string text = reader.ReadToEnd();
                                pResponsetext = text;
                                Console.WriteLine(text);
                            }
                        }
                    }
                }
                catch
                {
                    return HttpStatusCode.ExpectationFailed;
                }

                return HttpStatusCode.ExpectationFailed;
            }
        }

        public HttpStatusCode GetResponse1(ref string pResponsetext, string pUrl, string pMethod, string pData, string pReferer, int pTime = 10000, string pEncoding = "UTF-8", bool pIsPTHander = true)
        {
            Set_Cookies(pUrl);
            try
            {
                DeleteUrlCacheEntry(pUrl);
                HttpWebRequest pRequest = (HttpWebRequest)WebRequest.Create(pUrl);
                pRequest.KeepAlive = true;
                pRequest.Method = pMethod.ToUpper();
                pRequest.AllowAutoRedirect = true;
                pRequest.CookieContainer = CookieContainers;
                pRequest.Referer = pReferer;
                pRequest.UserAgent = IE8;
                pRequest.Accept = "application/json, text/javascript, */*; q=0.01";
                pRequest.Timeout = pTime;
                pRequest.ReadWriteTimeout = pTime;
                pRequest.CachePolicy = rcp;
                pRequest.Headers.Add("x-requested-with", "XMLHttpRequest");
                pRequest.ProtocolVersion = HttpVersion.Version10;
                pRequest.KeepAlive = false;
                if (pIsPTHander)
                {
                    PTRequest(ref pRequest);
                }
                Encoding encoding = Encoding.GetEncoding(pEncoding);

                if (pMethod.ToUpper() == "POST" && pData != null)
                {
                    byte[] bytes = encoding.GetBytes(pData);
                    pRequest.ContentLength = (long)bytes.Length;
                    pRequest.ContentType = "application/json; charset=UTF-8";
                    Stream requestStream = pRequest.GetRequestStream();
                    requestStream.Write(bytes, 0, bytes.Length);
                    requestStream.Close();
                }
                HttpWebResponse httpWebResponse = (HttpWebResponse)pRequest.GetResponse();
                StreamReader streamReader = new StreamReader(httpWebResponse.GetResponseStream(), encoding);
                pResponsetext = streamReader.ReadToEnd();
                pRequest.Abort();
                httpWebResponse.Close();
                
                return httpWebResponse.StatusCode;
            }
            catch (WebException ex)
            {
                HttpWebResponse response = (HttpWebResponse)ex.Response;

                if (response.StatusCode == HttpStatusCode.BadRequest)
                {
                    using (Stream data = response.GetResponseStream())
                    {
                        using (StreamReader reader = new StreamReader(data))
                        {
                            string text = reader.ReadToEnd();
                            pResponsetext = text;
                            Console.WriteLine(text);
                        }
                    }
                }

                return HttpStatusCode.ExpectationFailed;
            }
        }

        public HttpStatusCode GetResponse2(ref string pResponsetext, string pUrl, string pMethod, string pData, string pReferer, int pTime = 10000, string pEncoding = "UTF-8", bool pIsPTHander = true)
        {
            Set_Cookies(pUrl);
            try
            {
                DeleteUrlCacheEntry(pUrl);
                HttpWebRequest pRequest = (HttpWebRequest)WebRequest.Create(pUrl);
                pRequest.KeepAlive = true;
                pRequest.Method = pMethod.ToUpper();
                pRequest.AllowAutoRedirect = true;
                pRequest.CookieContainer = CookieContainers;
                pRequest.Referer = pReferer;
                pRequest.UserAgent = IE8;
                pRequest.Accept = "application/json, text/javascript, */*; q=0.01";
                pRequest.Timeout = pTime;
                pRequest.ReadWriteTimeout = pTime;
                pRequest.CachePolicy = rcp;
                pRequest.Headers.Add("x-requested-with", "XMLHttpRequest");
                pRequest.ProtocolVersion = HttpVersion.Version10;
                pRequest.KeepAlive = false;
                if (pIsPTHander)
                {
                    PTRequest(ref pRequest);
                }
                Encoding encoding = Encoding.GetEncoding(pEncoding);
                if (pMethod.ToUpper() == "POST" && pData != null)
                {
                    byte[] bytes = encoding.GetBytes(pData);
                    pRequest.ContentLength = (long)bytes.Length;
                    pRequest.ContentType = "application/x-www-form-urlencoded; charset=UTF-8";
                    Stream requestStream = pRequest.GetRequestStream();
                    requestStream.Write(bytes, 0, bytes.Length);
                    requestStream.Close();
                }
                HttpWebResponse httpWebResponse = (HttpWebResponse)pRequest.GetResponse();
                StreamReader streamReader = new StreamReader(httpWebResponse.GetResponseStream(), encoding);
                pResponsetext = streamReader.ReadToEnd();
                pRequest.Abort();
                httpWebResponse.Close();
                 
                return httpWebResponse.StatusCode;
            }
            catch (WebException ex)
            {
                HttpWebResponse response = (HttpWebResponse)ex.Response;

                if (response.StatusCode == HttpStatusCode.BadRequest)
                {
                    using (Stream data = response.GetResponseStream())
                    {
                        using (StreamReader reader = new StreamReader(data))
                        {
                            string text = reader.ReadToEnd();
                            pResponsetext = text;
                            Console.WriteLine(text);
                        }
                    }
                }

                return HttpStatusCode.ExpectationFailed;
            }
        }

        public HttpStatusCode GetResponse3(ref string pResponsetext, string pUrl, string pMethod, string pData, string pReferer, int pTime = 10000, string pEncoding = "UTF-8", bool pIsPTHander = true)
        {
            Set_Cookies(pUrl);
            try
            {
                DeleteUrlCacheEntry(pUrl);
                HttpWebRequest pRequest = (HttpWebRequest)WebRequest.Create(pUrl);
                pRequest.KeepAlive = true;
                pRequest.Method = pMethod.ToUpper();
                pRequest.AllowAutoRedirect = true;
                pRequest.CookieContainer = CookieContainers;
                pRequest.Referer = pReferer;
                pRequest.UserAgent = "Mozilla/5.0 (Windows NT 6.3; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/45.0.2454.101 Safari/537.36";
                pRequest.Accept = "text/plain, */*; q=0.01";
                pRequest.Timeout = pTime;
                pRequest.ReadWriteTimeout = pTime;
                pRequest.CachePolicy = rcp;
                pRequest.Headers.Add("x-requested-with", "XMLHttpRequest");
                pRequest.ProtocolVersion = HttpVersion.Version10;
                pRequest.KeepAlive = false;
                if (pIsPTHander)
                {
                    PTRequest(ref pRequest);
                }
                Encoding encoding = Encoding.GetEncoding(pEncoding);
                if (pMethod.ToUpper() != "GET" && pData != null)
                {
                    byte[] bytes = encoding.GetBytes(pData);
                    pRequest.ContentLength = (long)bytes.Length;
                    pRequest.ContentType = "application/x-www-form-urlencoded; charset=UTF-8";
                    Stream requestStream = pRequest.GetRequestStream();
                    requestStream.Write(bytes, 0, bytes.Length);
                    requestStream.Close();
                }
                HttpWebResponse httpWebResponse = (HttpWebResponse)pRequest.GetResponse();
                StreamReader streamReader = new StreamReader(httpWebResponse.GetResponseStream(), encoding);
                pResponsetext = streamReader.ReadToEnd();

                pRequest.Abort();
                httpWebResponse.Close();
               
                return httpWebResponse.StatusCode;
            }
            catch (WebException ex)
            {
                HttpWebResponse response = (HttpWebResponse)ex.Response;

                if (response.StatusCode == HttpStatusCode.BadRequest)
                {
                    using (Stream data = response.GetResponseStream())
                    {
                        using (StreamReader reader = new StreamReader(data))
                        {
                            string text = reader.ReadToEnd();
                            pResponsetext = text;
                            Console.WriteLine(text);
                        }
                    }
                }

                return HttpStatusCode.ExpectationFailed;
            }
        }

        public Stream GetResponseImage(string pUrl, string pReferer = "", string pMethod = "GET", string pData = "", int pTime = 6000, string pEncoding = "UTF-8", bool pIsPTHander = true)
        {

            Set_Cookies(pUrl);
            try
            {
                DeleteUrlCacheEntry(pUrl);
                HttpWebRequest pRequest = (HttpWebRequest)WebRequest.Create(pUrl);
                pRequest.KeepAlive = true;
                pRequest.Method = pMethod;
                pRequest.AllowAutoRedirect = true;
                pRequest.CookieContainer = CookieContainers;
                pRequest.Referer = pReferer;
                pRequest.UserAgent = IE8;
                pRequest.Accept = "*/*";
                pRequest.Timeout = pTime;
                pRequest.CachePolicy = rcp;
                pRequest.Headers.Add("x-requested-with", "XMLHttpRequest");
                pRequest.ProtocolVersion = HttpVersion.Version10;
                pRequest.KeepAlive = false;
                if (pIsPTHander)
                {
                    PTRequest(ref pRequest);
                }

                Encoding encoding = Encoding.GetEncoding(pEncoding);
                if (pMethod.ToUpper() == "POST" && pData != null)
                {
                    byte[] bytes = encoding.GetBytes(pData);
                    pRequest.ContentLength = (long)bytes.Length;
                    pRequest.ContentType = "application/x-www-form-urlencoded; charset=UTF-8";
                    Stream requestStream = pRequest.GetRequestStream();
                    requestStream.Write(bytes, 0, bytes.Length);
                    requestStream.Close();
                }
                ServicePointManager.ServerCertificateValidationCallback += (RemoteCertificateValidationCallback)((se, cert, chain, sslerror) => true);
                 
                return pRequest.GetResponse().GetResponseStream();
            }
            catch
            {
                return (Stream)null;
            }
        }


         
        /// <summary>
        /// 提取string Cookies
        /// </summary>
        /// <param name="pUrl"></param>
        /// <returns></returns>
        public string GetCookieInternal(string pUrl)
        {
            string result = "";
            uint num = 0u;
            string url = pUrl.ToString();
            uint flags = 8192u;
            if (InternetGetCookieExW(url, null, null, ref num, flags, IntPtr.Zero))
            {
                num += 1u;
                StringBuilder stringBuilder = new StringBuilder((int)num);
                if (InternetGetCookieExW(url, null, stringBuilder, ref num, flags, IntPtr.Zero))
                {
                    result = stringBuilder.ToString();
                }
            }
            return result;
        }

        /// <summary>
        /// 提取string Cookies
        /// </summary>
        /// <param name="pUrl"></param>
        /// <param name="pCookie"></param>
        /// <returns></returns>
        public string GetHttpHelperCookieString(Uri pUrl, CookieCollection pCookie = null)
        {
            List<string> pList = new List<string>();
            if (pCookie == null)
                pCookie = CookieContainers.GetCookies(pUrl);
            foreach (Cookie cookie in pCookie)
            {
                string name = cookie.Name;
                string str = cookie.Value;
                pList.Add(name + "=" + str);
            }
            return string.Join("; ", pList);
        }

        /// <summary>
        /// 提取string Cookies
        /// </summary>
        /// <param name="pUrl"></param>
        /// <param name="pCookie"></param>
        /// <returns></returns>
        public string GetHttpHelperCookieString(string pUrl, CookieCollection pCookie = null)
        {
            List<string> pList = new List<string>();
            if (pCookie == null)
                pCookie = CookieContainers.GetCookies(new Uri(pUrl));
            foreach (Cookie cookie in pCookie)
            {
                string name = cookie.Name;
                string str = cookie.Value;
                pList.Add(name + "=" + str);
            }
            return string.Join("; ", pList);
        }

        public Dictionary<string, string> GetHttpHelperCookie(Uri pUrl)
        {
            Dictionary<string, string> dictionary = new Dictionary<string, string>();
            foreach (Cookie cookie in CookieContainers.GetCookies(pUrl))
            {
                string name = cookie.Name;
                string str = cookie.Value;
                dictionary[name] = str;
            }
            return dictionary;
        }


        /// <summary>
        /// 删除Heard
        /// </summary>
        /// <param name="pRequest"></param>
        public void PTRequest(ref HttpWebRequest pRequest)
        {
            pRequest.Headers.Remove("x-requested-with");
        }

        /// <summary>
        /// 枚举类型
        /// </summary>
        public enum InternetFlags
        {
            INTERNET_FLAG_RESTRICTED_ZONE = 16,
            INTERNET_COOKIE_HTTPONLY = 8192,
            INTERNET_COOKIE_THIRD_PARTY = 131072,
        }

        /// <summary>
        /// 设置COOKIES
        /// </summary>
        /// <param name="url"></param>
        public void Set_Cookies(string url)
        {
            if (Cookies != null)
            {
                string[] arrCookie = Cookies.Split(';');

                CookieContainer cookie_container = new CookieContainer();    //加载Cookie
                //cookie_container.SetCookies(new Uri(url), cookie);
                foreach (string sCookie in arrCookie)
                {
                    if (sCookie.IndexOf("expires") > 0)
                        continue;
                    cookie_container.SetCookies(new Uri(url), sCookie);
                }
                CookieContainers = cookie_container;
            }

        }


        #region 正则提取Cookie
        public static string LiteCookies(string Cookies)
        {
            try
            {
                string rStr = "";
                Cookies = Cookies.Replace("HttpOnly", "").Replace(";", "; ");
                Regex r = new Regex("(?<=,|^)(?<cookie>[^ ]+=[\\s|\"]?(?![\"]?deleted[\"]?)[^;]+)[\"]?;");
                Match m = r.Match(Cookies);
                while (m.Success)
                {
                    rStr = GetCleanCookie(rStr, m.Groups["cookie"].Value);
                    m = m.NextMatch();
                }
                return rStr;
            }
            catch
            {
                return "";
            }
        }
        #endregion

        #region 获取一次请求中的无重复Cookie
        private static string GetCleanCookie(string source, string inStr)
        {
            if (source != "" && inStr != "")
            {
                bool changed = false;
                string[] tem = source.Split(';');
                for (int i = 0; i < tem.Length; i++)
                {
                    if (tem[i].Split('=')[0] == inStr.Split('=')[0])
                    {
                        source = source.Replace(tem[i], inStr);
                        changed = true;
                    }
                }
                if (!changed) source += ";" + inStr;
                return source;
            }
            else if (inStr != "") return inStr;
            else if (source != "") return source;
            else return "";
        }
        #endregion

        #region 合并多次请求的Cookie,去掉重复部分
        public static string MergerCookies(string OldCookie, string NewCookie)
        {
            if (!string.IsNullOrEmpty(OldCookie) && !string.IsNullOrEmpty(NewCookie))
            {
                if (OldCookie == NewCookie) return OldCookie;
                else
                {
                    List<string> Old = new List<String>(OldCookie.Split(';'));
                    List<string> New = new List<String>(NewCookie.Split(';'));
                    foreach (string n in New)
                    {
                        foreach (string o in Old)
                        {
                            if (o == n || o.Split('=')[0] == n.Split('=')[0])
                            {
                                Old.Remove(o);
                                break;
                            }
                        }
                    }
                    List<string> list = new List<string>(Old);
                    list.AddRange(New);
                    return string.Join(";", list.ToArray());
                }
            }
            else if (!string.IsNullOrEmpty(OldCookie)) return OldCookie;
            else if (!string.IsNullOrEmpty(NewCookie)) return NewCookie;
            else return "";
        }
        #endregion


        public byte[] StreamToBytes(Stream stream)
        {

            byte[] bytes = new byte[stream.Length];
            stream.Read(bytes, 0, bytes.Length);
            // 设置当前流的位置为流的开始
            stream.Seek(0, SeekOrigin.Begin);
            return bytes;
        }
    }
}

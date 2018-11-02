/*
 类说明:函数自编或由网络收集整理而成.
 编写人:cload
 整理日期:2014-10-25
*/
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Security;

namespace IPADDemo.Util
{
    public class Utilities
    {
        #region 编解码函数
        #region GB2312转Unicode
        /// <summary>
        /// 单个汉字转换为%AB%CD格式,如"我=%CE%D2"
        /// </summary>
        /// <param name="Source"></param>
        /// <returns></returns>
        public static string GB2Unicode(string source)
        {
            string retStr = "";
            string tem = "";
            byte[] Bytes = Encoding.GetEncoding("GB2312").GetBytes(source);
            for (int i = 0; i < Bytes.Length; i++)
            {
                if (Bytes[i] >= 48 && Bytes[i] <= 57 || Bytes[i] >= 65 && Bytes[i] <= 90 || Bytes[i] >= 97 && Bytes[i] <= 122)
                    tem = ((char)(Bytes[i])).ToString();
                else
                    tem = "%" + Bytes[i].ToString("X");
                retStr += tem;
            }
            return retStr;
        }
        #endregion

        #region UTF8编码
        /// <summary>
        /// 编码部分默认转为大写字母,字符编码采用默认值
        /// </summary>
        /// <param name="Source">http://www.123.com</param>
        /// <returns>http%3A%2F%2Fwww.123.com</returns>
        public static string UTF8(string Source, bool toUpper = true)
        {
            if (toUpper)
            {
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < Source.Length; i++)
                {
                    string t = Source[i].ToString();
                    string k = HttpUtility.UrlEncode(t, Encoding.Default);
                    if (t == k) sb.Append(t);
                    else sb.Append(k.ToUpper());
                }
                return sb.ToString();
            }
            else
                return HttpUtility.UrlEncode(Source, Encoding.Default);
        }
        public static string URL(string Source)
        {
            return HttpUtility.UrlEncode(Source);
        }
        #endregion

        #region %uxxxx编码
        public static string EncodeStr(string Source)
        {
            return HttpUtility.UrlEncodeUnicode(Source);
        }
        #endregion

        #region &#xx|%uxx|%xx%yy格式解码
        public static string DecodeStr(string Source)
        {
            string tem = Source;
            if (Source.Contains("&#"))
            {
                tem = "";
                if (Source.EndsWith(";")) Source = Source.Substring(0, Source.Length - 1);
                string[] tmp = Regex.Replace(Source, "&#", "").Split(';');
                for (int i = 0; i < tmp.Length; i++)
                {
                    string t = int.Parse(tmp[i]).ToString("x4");
                    tem += "%u" + t.Substring(t.Length - 4);
                }
            }
            return HttpUtility.UrlDecode(Regex.Unescape(tem));
        }
        #endregion

        #region MD5加密
        public static string MD5(string Source)
        {
            return FormsAuthentication.HashPasswordForStoringInConfigFile(Source, "MD5");
        }
        public static string GetMD5(string sDataIn)
        {
            System.Security.Cryptography.MD5CryptoServiceProvider md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
            byte[] bytValue, bytHash;
            bytValue = System.Text.Encoding.UTF8.GetBytes(sDataIn);
            bytHash = md5.ComputeHash(bytValue);
            md5.Clear();
            string sTemp = "";
            for (int i = 0; i < bytHash.Length; i++)
            {
                sTemp += bytHash[i].ToString("X").PadLeft(2, '0');
            }
            return sTemp.ToLower();
        }
        #endregion

        #endregion

        #region byte[]数组或16进制文本文件转图片
        /// <summary>
        /// 输入byte[]数组或16进制文本文件的全路径文本名
        /// </summary>
        /// <param name="ByteOrPath">字节数组或全路径文件名</param>
        /// <returns>Image图像</returns>
        public static Image ImageFromByte(object ByteOrPath)
        {
            try
            {
                byte[] PicBytes = null;
                if (ByteOrPath.GetType().Name == "String")
                {
                    string fileStr = File.ReadAllText(ByteOrPath.ToString());
                    fileStr = Regex.Replace(fileStr, "\\s+|\r\n", "");
                    byte[] picByte = new byte[fileStr.Length / 2];
                    for (int i = 0; i < picByte.Length; i++)
                    {
                        picByte[i] = Convert.ToByte("0x" + fileStr.Substring(i * 2, 2), 16);
                    }
                    PicBytes = picByte;
                }
                else PicBytes = (byte[])ByteOrPath;
                MemoryStream ms = new MemoryStream(PicBytes);
                ms.Position = 0;
                Image img = Image.FromStream(ms);
                ms.Close();
                return img;
            }
            catch
            {
                return null;
            }
        }
        #endregion

        #region Base64字符串转图片
        public static Image ImageFromBase64(string PathOrStr)
        {
            try
            {
                String Base64Str = "";
                if (PathOrStr.Contains("\\"))
                    Base64Str = File.ReadAllText(PathOrStr);
                else
                    Base64Str = PathOrStr;
                byte[] arr = Convert.FromBase64String(Base64Str);
                MemoryStream ms = new MemoryStream(arr);
                Bitmap bmp = new Bitmap(ms);
                ms.Close();
                return bmp;
            }
            catch
            {
                return null;
            }
        }
        #endregion

        #region Cookie简化去除重复数据
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

        #endregion

        #region 字符串操作函数
        #region 过滤html标签
        public static string StripHTML(string stringToStrip)
        {
            stringToStrip = Regex.Replace(stringToStrip, "</p(?:\\s*)>(?:\\s*)<p(?:\\s*)>", "\n\n", RegexOptions.IgnoreCase | RegexOptions.Compiled);
            stringToStrip = Regex.Replace(stringToStrip, "", "\n", RegexOptions.IgnoreCase | RegexOptions.Compiled);
            stringToStrip = Regex.Replace(stringToStrip, "\"", "''", RegexOptions.IgnoreCase | RegexOptions.Compiled);
            stringToStrip = StripHtmlXmlTags(stringToStrip);
            return stringToStrip;
        }
        private static string StripHtmlXmlTags(string content)
        {
            return Regex.Replace(content, "<[^>]+>", "", RegexOptions.IgnoreCase | RegexOptions.Compiled);
        }
        #endregion

        #region 取中间字符串
        public static string GetMidStr(string Source, string StartStr, string EndStr)
        {
            try
            {
                int StartPos = Source.IndexOf(StartStr, 0) + StartStr.Length;
                int EndPos = Source.IndexOf(EndStr, StartPos);
               // int s = 0;
               // int le=EndStr.Length;
                //for (int i = 0; i < Source.Length; i++)
                //{
                //    var x = Source.Length - i - le;
                //    if (Source.Substring(x, le) == EndStr)
                //    {
                //        s = x;
                //        break;
                            
                //    }
                //}

                return Source.Substring(StartPos, EndPos - StartPos);
            }
            catch { return ""; }
        }

        public static string GetMidStr(string Source, string EndStr)
        {
            try
            {
                /*        "}"125
                          "]" 93
                 */

                int s = 0;
                for (int i = Source.Length-1; i >= 0; i--)
                {
                     
                    if (Source.Substring(i, 1) == EndStr)
                    {
                        s = i;
                        break;

                    }
                }
                var data = Source.Substring(0, s+1);
                return data;
            }
            catch { return ""; }
        }
        #endregion





        #endregion

        #region 时间操作函数
        #region 时间戳
        public static string GetTime()
        {
            DateTime timeStamp = new DateTime(1970, 1, 1);
            long a = (DateTime.UtcNow.Ticks - timeStamp.Ticks) / 10000;
            return a.ToString();
        }

        public static double GetTimestamp
        {
            get
            {
                TimeSpan ts = DateTime.Now.ToUniversalTime() - new DateTime(1970, 1, 1);
                return ts.TotalMilliseconds;
            }
        }
        #endregion



        #region 时间相加
        public static string OperationTime(string BaseTime, string AddTime, string AddType)
        {
            DateTime dt = Convert.ToDateTime(BaseTime);
            DateTime rt = DateTime.Now;
            switch (AddType)
            {
                case "年": rt = dt.AddYears(int.Parse(AddTime)); break;
                case "月": rt = dt.AddMonths(int.Parse(AddTime)); break;
                case "日": rt = dt.AddDays(double.Parse(AddTime)); break;
                case "时": rt = dt.AddHours(double.Parse(AddTime)); break;
                case "分": rt = dt.AddMinutes(double.Parse(AddTime)); break;
                case "秒": rt = dt.AddSeconds(double.Parse(AddTime)); break;
                case "毫": rt = dt.AddMilliseconds(double.Parse(AddTime)); break;
            }
            return rt.ToString();
        }
        #endregion

        #region 英文月份变成数字月份
        public static string strToNum(string Month)
        {
            string ret = "";
            string[] enMonth = { "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec" };
            string[] nuMonth = { "01", "02", "03", "04", "05", "06", "07", "08", "09", "10", "11", "12" };
            for (int i = 0; i < enMonth.Length; i++)
            {
                if (Month == enMonth[i])
                {
                    ret = nuMonth[i];
                    break;
                }
            }
            return ret;
        }
        #endregion

        #endregion

    }
}
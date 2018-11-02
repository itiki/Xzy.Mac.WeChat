using Newtonsoft.Json;
using SuperSocket.ClientEngine;
using SuperSocket.ProtoBase;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using IPADDemo;
using IPADDemo.Model;
using IPADDemo.Util;
using System.Configuration;

namespace IPADDemo.WeChat
{
    public class XzyWeChatThread
    {
        #region 全局变量
        public int pointerWxUser;
        public int pushStr;
        public int result;
        public int msgPtr;
        public int callBackMsg;
        public int redPack;
        public int readMember;

        public string bankerWxid = "";
        public string cheshouWxid = "";
        public string groupId = "";

        public object objGroup = new object();

        private WxUser wxUser = new WxUser();
        private List<WxGroup> wxGroup { get; set; }

        public Dictionary<string, string> dicRedPack { get; set; }
        public Dictionary<string, string> dicReadContent { get; set; }
        private EasyClient<StringPackageInfo> socketClient = null;

        private int mHeartBeatInterval = 1000 * 10;
        private int mReConnectionInterval = 1000 * 10;

        Random R = new Random();
        string RandomStr(int n)
        {
            List<int> ilist = new List<int>();
            for (int i = 0; i < n; i++)
            {
                ilist.Add(R.Next(0, 9));
            }
            return string.Join("", ilist);

        }
        Int64 RandomL(int n)
        {
            List<int> ilist = new List<int>();
            for (int i = 0; i < n; i++)
            {
                ilist.Add(R.Next(0, 9));
            }
            return Convert.ToInt64(string.Join("", ilist));

        }
        string Mac
        {
            get
            {
                //return "0016D3B5C493";
                int min = 0;
                int max = 16;
                Random ro = new Random();
                var sn = string.Format("{0}{1}{2}{3}{4}{5}{6}{7}{8}{9}{10}{11}",
                   ro.Next(min, max).ToString("x"),//0
                   ro.Next(min, max).ToString("x"),//
                   ro.Next(min, max).ToString("x"),
                   ro.Next(min, max).ToString("x"),
                   ro.Next(min, max).ToString("x"),
                   ro.Next(min, max).ToString("x"),//5
                   ro.Next(min, max).ToString("x"),
                   ro.Next(min, max).ToString("x"),
                   ro.Next(min, max).ToString("x"),
                   ro.Next(min, max).ToString("x"),
                   ro.Next(min, max).ToString("x"),//10
                   ro.Next(min, max).ToString("x")
                    ).ToUpper();
                return sn;
            }
        }
        string UUID
        {
            get
            {
                return RandomStr(8) + "-" + RandomStr(4) + "-" + RandomL(4).ToString("X") + "-" + RandomL(4).ToString("X") + "-" + RandomL(12).ToString("X");
            }
        }

        #endregion

        #region 微信委托
        public XzyWxApis.DllcallBack msgCallBack { get; set; }
        #endregion

        #region 定时器
        /// <summary>
        /// 心跳检查定时器
        /// </summary>
        private System.Threading.Timer tmrHeartBeat = null;

        /// <summary>
        /// 断线重连定时器
        /// </summary>
        private System.Threading.Timer tmrReConnection = null;
        #endregion

        #region 构造函数
        /// <summary>
        /// 初始化，并且使用二维码登录。并且初始化一些计时器，但实际上这些计时器没有什么用。这些计时器，应该是为了实现类似signalr的功能
        /// </summary>
        public XzyWeChatThread()
        {
            dicRedPack = new Dictionary<string, string>();
            dicReadContent = new Dictionary<string, string>();
            Task.Factory.StartNew(() =>
            {
                this.Init();
            });

            msgCallBack += new XzyWxApis.DllcallBack(Wx_MsgCallBack);

            tmrHeartBeat = new System.Threading.Timer(HeartBeatCallBack, null, mHeartBeatInterval, mHeartBeatInterval);

            tmrReConnection = new System.Threading.Timer(ReConnectionCallBack, null, mReConnectionInterval, mReConnectionInterval);

            SocketStart(null);

        }

        /// <summary>
        /// 62数据登陆，并且初始化一些计时器，但实际上这些计时器没有什么用。这些计时器，应该是为了实现类似signalr的功能
        /// </summary>
        /// <param name="str16"></param>
        /// <param name="WxUsername"></param>
        /// <param name="wxpassword"></param>
        public XzyWeChatThread(string str16, string WxUsername, string wxpassword)
        {
            dicRedPack = new Dictionary<string, string>();
            dicReadContent = new Dictionary<string, string>();
            Task.Factory.StartNew(() =>
            {
                this.Init62(str16, WxUsername, wxpassword);
            });

            msgCallBack += new XzyWxApis.DllcallBack(Wx_MsgCallBack);

            tmrHeartBeat = new System.Threading.Timer(HeartBeatCallBack, null, mHeartBeatInterval, mHeartBeatInterval);

            tmrReConnection = new System.Threading.Timer(ReConnectionCallBack, null, mReConnectionInterval, mReConnectionInterval);

            SocketStart(null);

        }

        #endregion

        #region 全局方法

        #region socket
        /// <summary>
        /// socket启动
        /// </summary>
        /// <param name="a"></param>
        private async void SocketStart(object a)
        {
            try
            {
                //初始化并启动客户端引擎（TCP、文本协议）
                socketClient = new EasyClient<StringPackageInfo>()
                {
                    ReceiveBufferSize = 65535
                };
                socketClient.Initialize(new MyTerminatorReceiveFilter());
                socketClient.Connected += client_Connected;
                socketClient.NewPackageReceived += client_NewPackageReceived;
                socketClient.Closed += client_Closed;
                socketClient.Error += client_Error;//192.168.0.102
                var connected = await socketClient.ConnectAsync(new System.Net.DnsEndPoint(CS.IP, 9000));
                if (connected && socketClient.IsConnected)
                {
                    ShowMessage("连接成功！");
                    if (this.wxUser.wxid != "")
                    {
                        TcpSendMsg(TcpMsg.OL, wxUser);
                        if (wxGroup != null)
                        {
                            this.Wx_GetContacts();
                        }
                    }
                }
                else
                {
                    ShowMessage("连接失败！");
                }
            }
            catch (Exception ex)
            {
                ShowMessage(string.Format("连接服务器失败:{0}", ex.Message));
            }
        }

        /// <summary>
        /// 接收消息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void client_NewPackageReceived(object sender, PackageEventArgs<StringPackageInfo> e)
        {
            var body = e.Package.Body;
            MsgContent json = null;
            switch (e.Package.Key)
            {
                case TcpMsg.content:
                    json = Newtonsoft.Json.JsonConvert.DeserializeObject<MsgContent>(body);
                    this.Wx_SendMsg(json.towxid, json.content);
                    break;

                case TcpMsg.img:
                    json = Newtonsoft.Json.JsonConvert.DeserializeObject<MsgContent>(body);
                    this.Wx_SendImg(json.towxid, json.ImgPath);
                    break;

                case TcpMsg.redpack:
                    //json = Newtonsoft.Json.JsonConvert.DeserializeObject<MsgContent>(body);
                    //var key = json.content;
                    //await Task.Factory.StartNew(() =>
                    //{
                    //    this.RedpackOK2(key);
                    //});
                    //this.RedpackOK2(key, 0);
                    break;

                case TcpMsg.BankerConfig:
                    var BankerConfig = Newtonsoft.Json.JsonConvert.DeserializeObject<BankerConfig>(body);

                    switch (BankerConfig.code)
                    {
                        case 1: groupId = BankerConfig.wxid; break;

                        case 2: bankerWxid = BankerConfig.wxid; break;

                        case 3: cheshouWxid = BankerConfig.wxid; break;
                    }
                    break;
            }
            ShowMessage(e.Package.Body);
        }

        /// <summary>
        /// socket 异常回调
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void client_Error(object sender, global::SuperSocket.ClientEngine.ErrorEventArgs e)
        {
            if (e.Exception.GetType() == typeof(System.Net.Sockets.SocketException))
            {
                var socketExceptin = e.Exception as System.Net.Sockets.SocketException;
                if (socketExceptin.SocketErrorCode == SocketError.ConnectionRefused)
                {
                    ShowMessage("错误:请先启动AppServer 服务端！");
                }
                else
                    ShowMessage("错误:" + e.Exception.Message);
            }
            else
                ShowMessage("错误:" + e.Exception.Message);
        }

        /// <summary>
        /// socket断开回调
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void client_Closed(object sender, EventArgs e)
        {
            ShowMessage("您已经掉线！");
            SocketStart(null);
        }

        /// <summary>
        /// socket连接成功回调
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void client_Connected(object sender, EventArgs e)
        {
            ShowMessage("连接成功！");
        }

        /// <summary>
        /// socket发送消息
        /// </summary>
        /// <param name="TcpMsg"></param>
        /// <param name="content"></param>
        void TcpSendMsg(string TcpMsg, object content)
        {
            if (socketClient != null && socketClient.IsConnected)
            {
                var json = TcpMsg + " " + Newtonsoft.Json.JsonConvert.SerializeObject(content) + Environment.NewLine;
                var data = Encoding.UTF8.GetBytes(json);
                socketClient.Send(new ArraySegment<byte>(data, 0, data.Length));
            }
        }

        /// <summary>
        /// 心跳包
        /// </summary>
        /// <param name="state"></param>
        private void HeartBeatCallBack(object state)
        {
            try
            {
                tmrHeartBeat.Change(Timeout.Infinite, Timeout.Infinite);
                if (socketClient != null && socketClient.IsConnected)
                {
                    var sbMessage = new StringBuilder();
                    sbMessage.AppendFormat(string.Format("heartbeat #{0}#\r\n", "心跳数据包:ok"));
                    var data = Encoding.UTF8.GetBytes(sbMessage.ToString());
                    socketClient.Send(new ArraySegment<byte>(data, 0, data.Length));
                }
            }
            finally
            {
                tmrHeartBeat.Change(mHeartBeatInterval, mHeartBeatInterval);
            }
        }

        /// <summary>
        /// 掉线重连
        /// </summary>
        /// <param name="state"></param>
        private void ReConnectionCallBack(object state)
        {
            try
            {
                tmrReConnection.Change(Timeout.Infinite, Timeout.Infinite);
                if (socketClient != null &&
                    socketClient.IsConnected == false)
                {
                    SocketStart(null);
                    //btnOpen_Click(null, null);
                }
            }
            finally
            {
                tmrReConnection.Change(mHeartBeatInterval, mHeartBeatInterval);
            }
        }

        #endregion

        /// <summary>
        /// 登录
        /// </summary>
        public unsafe void Init()
        {
            fixed (int* WxUser1 = &pointerWxUser, pushStr1 = &pushStr)
            {
                //string version = System.Reflection.Assembly.LoadFrom("wxipadapi.dll").GetName().Version.ToString();
                //WxDelegate.show("版本信息：" + version);
                string uid = UUID;
                var mac = Mac;

                //var ret = XzyAuth.Init();
                //if (ret == 1)
                //{
                var ret = XzyAuth.Init(ConfigurationSettings.AppSettings["AuthKey"].ToString());
                //}

                WxDelegate.show("授权结果：" + ret);
                var key = string.Format(@"<softtype><k3>11.0.1</k3><k9>iPad</k9><k10>2</k10><k19>58BF17B5-2D8E-4BFB-A97E-38F1226F13F8</k19><k20>{0}</k20><k21>neihe_5GHz</k21><k22>(null)</k22><k24>{1}</k24><k33>\345\276\256\344\277\241</k33><k47>1</k47><k50>1</k50><k51>com.tencent.xin</k51><k54>iPad4,4</k54></softtype>", UUID, Mac);

                XzyWxApis.WXInitialize((int)WxUser1, "张三的IPAD", key, UUID);

                XzyWxApis.WXSetRecvMsgCallBack(pointerWxUser, msgCallBack);
                XzyWxApis.WXGetQRCode(pointerWxUser, (int)pushStr1);

                var msg = Marshal.PtrToStringAnsi(new IntPtr(Convert.ToInt32(pushStr)));

                WxQrCode qr_code = JsonConvert.DeserializeObject<WxQrCode>(msg);//反序列化

                //var img = MyUtils.Base64StringToImage(qr_code.QrCodeStr);
                WxDelegate.qrCode(qr_code.QrCodeStr);

                Wx_ReleaseEX(ref pushStr);
                QrCodeJson QRCodejson = null;
                while (true)
                {
                    Thread.Sleep(500);
                    XzyWxApis.WXCheckQRCode(pointerWxUser, (int)pushStr1);
                    var datas = MarshalNativeToManaged((IntPtr)pushStr);
                    if (datas == null)
                    {
                        continue;
                    }
                    string sstr = datas.ToString();
                    QRCodejson = Newtonsoft.Json.JsonConvert.DeserializeObject<QrCodeJson>(sstr);//反序列化
                    Wx_ReleaseEX(ref pushStr);
                    bool breakok = false;
                    switch (QRCodejson.Status)
                    {
                        case 0: WxDelegate.show("请扫描二维码"); break;
                        case 1: WxDelegate.show("请点在手机上点确认"); break;
                        case 2: WxDelegate.show("正在登录中.."); breakok = true; break;
                        case 3: WxDelegate.show("已过期"); break;
                        case 4: WxDelegate.show("取消操作了"); breakok = true; break;
                    }
                    if (breakok) { break; }
                }
                if (QRCodejson.Status == 2)
                {
                    var username = QRCodejson.UserName;

                    this.wxUser.wxid = QRCodejson.UserName;
                    this.wxUser.name = QRCodejson.NickName;
                    var pass = QRCodejson.Password;
                    XzyWxApis.WXQRCodeLogin(pointerWxUser, username, pass, (int)pushStr1);
                    var datas = MarshalNativeToManaged((IntPtr)pushStr);
                    string sstr = datas.ToString();
                    Wx_ReleaseEX(ref pushStr);
                    UserData userdata = Newtonsoft.Json.JsonConvert.DeserializeObject<UserData>(sstr);//反序列化
                    if (userdata.Status == -301)
                    {
                        XzyWxApis.WXQRCodeLogin(pointerWxUser, username, pass, (int)pushStr1);
                        datas = MarshalNativeToManaged((IntPtr)pushStr);
                        sstr = datas.ToString();
                        Wx_ReleaseEX(ref pushStr);
                        WxDelegate.show("微信重定向");
                        userdata = Newtonsoft.Json.JsonConvert.DeserializeObject<UserData>(sstr);//反序列化

                        if (userdata.Status == 0)
                        {
                            WxDelegate.show("登录成功");
                            XzyWxApis.WXHeartBeat(pointerWxUser, (int)pushStr1);
                            datas = MarshalNativeToManaged((IntPtr)pushStr);
                            sstr = datas.ToString();
                            Wx_ReleaseEX(ref pushStr);

                            this.TcpSendMsg(TcpMsg.OL, this.wxUser);

                            Task.Factory.StartNew(delegate { this.Wx_GetContacts(); });

                            return;
                        }
                        else
                        {
                            WxDelegate.show("登录失败");

                        }
                    }
                    if (userdata.Status == 0)
                    {
                        WxDelegate.show("登录成功");
                        XzyWxApis.WXHeartBeat(pointerWxUser, (int)pushStr1);
                        datas = MarshalNativeToManaged((IntPtr)pushStr);

                        TestAPI test = new TestAPI();
                        test.Test(pointerWxUser);

                        sstr = datas.ToString();
                        Wx_ReleaseEX(ref pushStr);

                        this.TcpSendMsg(TcpMsg.OL, this.wxUser);

                        Task.Factory.StartNew(delegate { this.Wx_GetContacts(); });

                        return;
                    }
                    else
                    {
                        WxDelegate.show("登录失败");
                    }
                }
            }
        }

        /// <summary>
        /// 初始化62数据
        /// </summary>
        /// <param name="str16"></param>
        /// <param name="WxUsername"></param>
        /// <param name="wxpassword"></param>
        public unsafe void Init62(string str16, string WxUsername, string wxpassword)
        {
            fixed (int* WxUser1 = &pointerWxUser, pushStr1 = &pushStr)
            {

                List<byte> blist = new List<byte>();
                for (int i = 0; i < str16.Length / 2; i++)
                {
                    var s = str16.Substring(i * 2, 2);

                    var a = Convert.ToInt32(s, 16);
                    blist.Add((byte)a);
                }
                byte[] byt = blist.ToArray();

                //var ret = XzyAuth.Init();
                //if (ret == 1)
                //{
                var ret = XzyAuth.Init(ConfigurationSettings.AppSettings["AuthKey"].ToString());
                //}
                string uid = UUID;
                var mac = Mac;

                var key = string.Format(@"<softtype><k3>11.0.1</k3><k9>iPad</k9><k10>2</k10><k19>58BF17B5-2D8E-4BFB-A97E-38F1226F13F8</k19><k20>{0}</k20><k21>neihe_5GHz</k21><k22>(null)</k22><k24>{1}</k24><k33>\345\276\256\344\277\241</k33><k47>1</k47><k50>1</k50><k51>com.tencent.xin</k51><k54>iPad4,4</k54></softtype>", UUID, Mac);

                XzyWxApis.WXInitialize((int)WxUser1, "sdsaIPAD", key, UUID);

                XzyWxApis.WXSetRecvMsgCallBack(pointerWxUser, msgCallBack);

                //62数据是扫码登录成功后，再获取，并保存下来，而不是其它方式登录后再保存。并且还要使用方法WXGetLoginToken保存下token
                #region 使用62数据自动登录，在扫码登录后，会得到62数据及token，传入到这里即可实现自动登录
                //加载62数据
                //var data = "YnBsaXN0MDDUAQIDBAUGCQpYJHZlcnNpb25YJG9iamVjdHNZJGFyY2hpdmVyVCR0b3ASAAGGoKIHCFUkbnVsbF8QIGM1N2Y3NTkzMmI3MDhjM2RjYTY1YTY2MjdmZjM4NmE2XxAPTlNLZXllZEFyY2hpdmVy0QsMVHJvb3SAAQgRGiMtMjc6QGN1eH0AAAAAAAABAQAAAAAAAAANAAAAAAAAAAAAAAAAAAAAfw==";
                //byte[] data62Bytes = Convert.FromBase64String(data);
                //XzyWxApis.WXLoadWxDat(pointerWxUser, data62Bytes, data62Bytes.Length, (int)pushStr1);
                XzyWxApis.WXLoadWxDat(pointerWxUser, byt, byt.Length, (int)pushStr1);
                var datas1 = MarshalNativeToManaged((IntPtr)pushStr);
                var sstr1 = datas1.ToString();
                if (string.IsNullOrEmpty(sstr1))
                {
                    WxDelegate.show("登陆失败，重新登录");
                }
                Wx_ReleaseEX(ref pushStr);


                //WXLoginRequest 可以自动登录
                XzyWxApis.WXLoginRequest(pointerWxUser, "0XEbAvUZZY++yTRZVxY+BFdCUK/fniCOt9XRhw5YYmpacX781DiS66MHn6dSO8u3gvs9qh8/yVHujrCl3IKTqbcCJxthPP1M3HavpT3K62iBAs86cbqnrh328QzXZNJULW5qIgm2o7G2qBnbY9XIH5/bzsT7SwtlDji58IGIyfBFGyRgoCAByCSfNu5Ozdssuh4de9mQCs12z9XCeXdXZwaNsEYhkb//zpGClyk1X45EODzOPUq3rzlu79J+i3ZF7gmU39PqrgzSQtwkPn/oU1oS7GDYDqwiJwxo/rQVNmrlUbaoFEqSWK74JAMS1lEdgtin5LVmaJj0h9CZgWcNoyfTVC54tXjp8/Dpq1pgLy5Sue4ICuUw3qgsAEJJm/jXphRskYbxeultiL6qDPJFeXT/tpVsw8mXa7fd0tZ1n83wuxi5f1MNYOuoinov/C6jRrFjLk1T7Ko7JdosCbpsKhfeI0p8R9JFeI8iP23YP9Pz3Epk6scGcNV4L3nr1AiaHR5mYiBnPVkTh5EXYaUUTeVV1XlwBsIbcma2nkU+lflZwk3Wnt/8udiHIc1+zArJjioajHPxvisY+rafWvMJp6/o70CgjkM9VXzmRYXhxBWNQHGIL9kxA4S3R7/RqiQIkOTaP/l8EruHVE7B1VIGksdIC1PxccR+iIZbMNtYSLQyvnvdLr0emdD51moNfu3E", (int)pushStr1);

                //WXAutoLogin  用于断线重连。一般非断线下如果 要自动登录，是使用WXLoginRequest方法来做二次登录。若断线再连就使用WXAutoLogin
                //XzyWxApis.WXAutoLogin(pointerWxUser, "wLdXBzhJttK0TFD3TFpU62ztnYC/bIphOgiv4E8byH+oqKRvl6D5cEDl5kwYHX3Opo77H9M1SuJPbOT3OVW4Ta83vKuzLXos8IQtlcA16GWtM0oXtiCzlJc2zgrh9wbquKY7XQ1TUTPvvnKXbmLWMryL672poL3qqg3wfrdldOR5txTMl9JqsPWC6IaflN2F0VdRJMbVYAinbfCHZ37zCo9jTABo61G19/4MHucBPSJxCaIGqL5v87aH99nT/Lk4lE+bn+S/+9h1RD9UZq+mkcYZ4GypC5Rz8QRbswUJ29+uUDN1cLan6rgOtyM5D8YxzuqjjpNRfS7V1kUo1WNqyJYzTVOEde+i5rIwo/OoRJw7uwS2tthdmzOHGqnglmXoOjpU26j6PMVctC/R036uUopk9jDJ7BA9ALzoaOXn1kOsTPV7d0M1+fRi8wa0kFGx7MrTCHgLprbxyiImxhlO3lHRaedJ+2BqvUjUm2N5+VThxQ4bF8c031Ubk2A0p+UGjbYDdWl6gOxwnRdc6AHXGevL+3qWKIBirALmZ/AfgHcQ0ukXdPtbJX2NK75XUEV5RRVn0GdGPK2HTGmjQaz3/XZKBTxDysCLGJcxn43wPIRzJaD/V5mIEZxcPFYwecNZtoSISqq45flBfe/KST14/FE5mFitS74YCpjy6si5hkM=", (int)pushStr1);
                //XzyWxApis.WXLoginRequest(pointerWxUser, tokenData.Token, (int)pushStr1);
                var datas2 = MarshalNativeToManaged((IntPtr)pushStr);
                var sstr2 = datas2.ToString();
                Wx_ReleaseEX(ref pushStr);
                #endregion

                //以下是使用账号密码登录，已经测试成功。账号：13127873237，密码：Taobao123
                XzyWxApis.WXUserLogin(pointerWxUser, WxUsername, wxpassword, (int)pushStr1);
                var datas = MarshalNativeToManaged((IntPtr)pushStr);
                var sstr = datas.ToString();
                Wx_ReleaseEX(ref pushStr);

                UserData userdata = Newtonsoft.Json.JsonConvert.DeserializeObject<UserData>(sstr);//反序列化

                if (userdata.Status == -301)
                {
                    XzyWxApis.WXUserLogin(pointerWxUser, WxUsername, wxpassword, (int)pushStr1);
                    datas = MarshalNativeToManaged((IntPtr)pushStr);
                    sstr = datas.ToString();
                    Wx_ReleaseEX(ref pushStr);
                    WxDelegate.show("微信重定向");
                    userdata = Newtonsoft.Json.JsonConvert.DeserializeObject<UserData>(sstr);//反序列化
                    this.wxUser.wxid = userdata.UserName;
                    this.wxUser.name = userdata.NickName;
                    if (userdata.Status == 0)
                    {
                        WxDelegate.show("登录成功");
                        XzyWxApis.WXHeartBeat(pointerWxUser, (int)pushStr1);
                        datas = MarshalNativeToManaged((IntPtr)pushStr);
                        sstr = datas.ToString();
                        Wx_ReleaseEX(ref pushStr);
                        this.TcpSendMsg(TcpMsg.OL, this.wxUser);
                        Task.Factory.StartNew(delegate { this.Wx_GetContacts(); });

                        //马志远：登录成功后，取出token备用
                        XzyWxApis.WXGetLoginToken(pointerWxUser, (int)pushStr1);
                        var datas3 = MarshalNativeToManaged((IntPtr)pushStr);
                        var sstr3 = datas3.ToString();
                        var tokenData = Newtonsoft.Json.JsonConvert.DeserializeAnonymousType(sstr3, new { Token = "" });
                        Wx_ReleaseEX(ref pushStr);
                        return;
                    }
                    else
                    {
                        WxDelegate.show("登录失败");
                    }
                }
                if (userdata.Status == 0)
                {
                    WxDelegate.show("登录成功");
                    XzyWxApis.WXHeartBeat(pointerWxUser, (int)pushStr1);
                    datas = MarshalNativeToManaged((IntPtr)pushStr);
                    sstr = datas.ToString();
                    Wx_ReleaseEX(ref pushStr);
                    this.wxUser.wxid = userdata.UserName;
                    this.wxUser.name = userdata.NickName;
                    this.TcpSendMsg(TcpMsg.OL, this.wxUser);
                    Task.Factory.StartNew(delegate { this.Wx_GetContacts(); });

                    //马志远：登录成功后，取出token备用
                    XzyWxApis.WXGetLoginToken(pointerWxUser, (int)pushStr1);
                    var datas3 = MarshalNativeToManaged((IntPtr)pushStr);
                    var sstr3 = datas3.ToString();
                    var tokenData = Newtonsoft.Json.JsonConvert.DeserializeAnonymousType(sstr3, new { Token = "" });
                    Wx_ReleaseEX(ref pushStr);
                    return;
                }
                else
                {
                    WxDelegate.show("登录失败");
                }
            }
        }

        public object MarshalNativeToManaged(IntPtr pNativeData)
        {
            try
            {
                if (pNativeData == IntPtr.Zero)
                {
                    return null;
                }
                List<byte> list = new List<byte>();
                int num = 0;
                for (; ; )
                {
                    byte b = Marshal.ReadByte(pNativeData, num);
                    if (b == 0)
                    {
                        break;
                    }
                    list.Add(b);
                    num++;
                }
                return Encoding.UTF8.GetString(list.ToArray(), 0, list.Count);
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// 图片转byte
        /// </summary>
        /// <param name="image"></param>
        /// <returns></returns>
        public byte[] ImageToBytes(Image image)
        {
            ImageFormat format = image.RawFormat;
            using (MemoryStream ms = new MemoryStream())
            {
                if (format.Equals(ImageFormat.Jpeg))
                {
                    image.Save(ms, ImageFormat.Jpeg);
                }
                else if (format.Equals(ImageFormat.Png))
                {
                    image.Save(ms, ImageFormat.Png);
                }
                else if (format.Equals(ImageFormat.Bmp))
                {
                    image.Save(ms, ImageFormat.Bmp);
                }
                else if (format.Equals(ImageFormat.Gif))
                {
                    image.Save(ms, ImageFormat.Gif);
                }
                else if (format.Equals(ImageFormat.Icon))
                {
                    image.Save(ms, ImageFormat.Icon);
                }
                byte[] buffer = new byte[ms.Length];
                //Image.Save()会改变MemoryStream的Position，需要重新Seek到Begin
                ms.Seek(0, SeekOrigin.Begin);
                ms.Read(buffer, 0, buffer.Length);
                return buffer;
            }
        }

        /// <summary>
        /// 打印消息
        /// </summary>
        /// <param name="msg"></param>
        private void ShowMessage(string msg)
        {
            Console.WriteLine(msg);
        }

        public static int TimeStamp
        {
            get
            {

                TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
                return Convert.ToInt32(ts.TotalSeconds - 180);
            }
        }

        #endregion

        #region 微信方法
        /// <summary>
        /// 发消息 -文字
        /// </summary>
        /// <param name="wxid"></param>
        /// <param name="content"></param>
        public unsafe void Wx_SendMsg(string wxid, string content)
        {
            content = content.Replace(" ", "\r\n");
            fixed (int* WxUser1 = &pointerWxUser, msgptr1 = &msgPtr)
            {
                XzyWxApis.WXSendMsg(pointerWxUser, wxid, content, null, (int)msgptr1);
                var datas = MarshalNativeToManaged((IntPtr)msgPtr);
                var str = datas.ToString();
                Wx_ReleaseEX(ref msgPtr);
            }
        }

        private int wx_imptr;
        /// <summary>
        /// 发消息 - 图片
        /// </summary>
        /// <param name="wxid"></param>
        /// <param name="imgpath"></param>
        public unsafe void Wx_SendImg(string wxid, string imgpath)
        {
            WxDelegate.show(string.Format("发送图片 {0}", imgpath));

            fixed (int* WxUser1 = &pointerWxUser, imptr1 = &wx_imptr)
            {
                try
                {
                    Image _image = Image.FromStream(WebRequest.Create(imgpath).GetResponse().GetResponseStream());
                    //把文件读取到字节数组
                    byte[] data = this.ImageToBytes(_image);
                    if (data.Length > 0)
                    {
                        XzyWxApis.WXSendImage(pointerWxUser, wxid, data, data.Length, (int)imptr1);
                        var datas = MarshalNativeToManaged((IntPtr)wx_imptr);
                        var str = datas.ToString();
                        Wx_ReleaseEX(ref wx_imptr);
                    }
                    _image = null;
                }
                catch { }
            }
        }

        private int wx_resultContacts;
        /// <summary>
        /// 获取通讯录
        /// </summary>
        public unsafe void Wx_GetContacts()
        {
            if (wxGroup != null && wxGroup.Count > 0)
            {
                TcpSendMsg(TcpMsg.Group, wxGroup);
                return;
            }
            wxGroup = new List<WxGroup>();
            Dictionary<string, string> dicg = new Dictionary<string, string>();
            fixed (int* WxUser1 = &pointerWxUser, resulttxl1 = &wx_resultContacts)
            {
                while (true)
                {
                    Thread.Sleep(200);
                    XzyWxApis.WXSyncContact(pointerWxUser, (int)(resulttxl1));
                    if (wx_resultContacts == 0)
                    {
                        continue;
                    }
                    var datas = MarshalNativeToManaged((IntPtr)wx_resultContacts);
                    Wx_ReleaseEX(ref wx_resultContacts);
                    if (datas == null) { continue; }
                    var str = datas.ToString();
                    List<Contact> Contact = null;
                    Contact = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Contact>>(str);
                    result = 0;
                    var con = 0;
                    //循环所有通讯录对象，此通讯录包括好友、群、公众号等
                    foreach (var c in Contact)
                    {
                        con = c.Continue;
                        if (con == 0) { break; }
                        if (c.MsgType == 2)
                        {
                            var user = c.UserName;
                            var nick = c.NickName;
                            var bit = c.BitValue;
                            if (bit == 3)
                            {
                                //群组

                            }
                            lock (objGroup)
                            {
                                WxDelegate.show(user);
                                //若用户名中包含了@符号的，表示这是一个群
                                if (user.IndexOf("@") != -1)
                                {
                                    WxDelegate.show(user + "---" + nick + "|" + bit.ToString());
                                    if (!dicg.ContainsKey(user))
                                    {
                                        dicg.Add(user, user);
                                        //将所有包含@符号的群收集起来
                                        wxGroup.Add(new WxGroup() { groupid = c.UserName, groupname = c.NickName, WxUser = this.wxUser.wxid, membercount = c.MemberCount });
                                    }
                                }
                            }
                        }
                    }
                    if (con == 0) { break; }
                }
                XzyWxApis.WXSyncReset(pointerWxUser);

                //过滤群，将得到有成员列表的群。若没有成员的群，将被过滤掉
                List<WxGroup> nWxGroup = new List<WxGroup>();

                for (int i = 0; i < wxGroup.Count; i++)
                {
                    WxGroup n = wxGroup[i];
                    n.member = this.Wx_GetGroupMember(wxGroup[i].groupid);

                    //无群成员，则过滤掉
                    if (n.member != null)
                    {
                        nWxGroup.Add(n);

                    }
                }
                wxGroup = nWxGroup;
                TcpSendMsg(TcpMsg.Group, wxGroup);
            }
        }

        /// <summary>
        /// 取群成员
        /// </summary>
        /// <param name="groupId"></param>
        /// <returns></returns>
        public unsafe List<WxMember> Wx_GetGroupMember(string groupId)
        {
            fixed (int* WxUser1 = &pointerWxUser, readmember1 = &readMember)
            {
                XzyWxApis.WXGetChatRoomMember(pointerWxUser, groupId, (int)readmember1);
                var datas = MarshalNativeToManaged((IntPtr)readMember);
                var str = datas.ToString();
                Wx_ReleaseEX(ref readMember);
                GroupMember groupmember = null;
                groupmember = Newtonsoft.Json.JsonConvert.DeserializeObject<GroupMember>(str);
                List<Member> member = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Member>>(groupmember.Member);
                List<WxMember> WxMember = new List<WxMember>();
                if (member != null && member.Count > 0)
                {
                    foreach (var m in member)
                    {
                        WxMember w = new WxMember();
                        w.userid = this.wxUser.wxid;
                        w.groupid = groupId;
                        w.nickname = m.NickName;
                        w.wxid = m.UserName;
                        WxMember.Add(w);
                    }
                    return WxMember;
                }
                return null;
            }
        }

        public object wx_objMsg = new object();
        /// <summary>
        /// 设置消息。用于判断消息是否被处理过。若未处理过，则返回true，已经处理过的，返回false。
        /// </summary>
        /// <param name="msgid"></param>
        /// <returns></returns>
        public bool Wx_SetMsgKey(string msgid)
        {
            lock (wx_objMsg)
            {
                try
                {
                    if (dicReadContent.Count > 5000)
                    {
                        dicReadContent = new Dictionary<string, string>();
                    }

                    if (!dicReadContent.ContainsKey(msgid))
                    {
                        dicRedPack.Add(msgid, msgid);
                        return true;
                    }
                }
                catch
                {
                    return false;
                }
            }
            return false;
        }

        int wx_intoGroup = 0;
        /// <summary>
        /// 进群
        /// </summary>
        /// <param name="url"></param>
        public unsafe void Wx_IntoGroup(string url)
        {
            if (url != "")
            {

                fixed (int* WxUser1 = &pointerWxUser, jinqun1 = &wx_intoGroup)
                {
                    XzyWxApis.WXGetRequestToken(pointerWxUser, "", url, (int)jinqun1);
                    if ((int)jinqun1 == 0) { return; }

                    var json = MarshalNativeToManaged((IntPtr)wx_intoGroup).ToString();
                    Wx_ReleaseEX(ref wx_intoGroup);
                    if (json == "") { return; }
                    EnterGroupJson jinqunjson = Newtonsoft.Json.JsonConvert.DeserializeObject<EnterGroupJson>(json);
                    var FullUrl = jinqunjson.FullUrl;
                    var tk = Utilities.GetMidStr(jinqunjson.FullUrl + "||||", "ticket=", "||||");
                    Http_Helper Http_Helper = new Http_Helper();
                    var res = "";
                    var status = Http_Helper.GetResponse_WX(ref res, FullUrl, "POST", "", FullUrl, 30000, "UTF-8", true);
                    WxDelegate.show("被邀请进入群，开始读通讯录！");
                    this.Wx_GetContacts();
                }
            }
        }

        public object wx_groupObj = new object();
        /// <summary>
        /// 更新群成员
        /// </summary>
        /// <param name="groupId"></param>
        /// <returns></returns>
        public bool Wx_SetGroup(string groupId)
        {
            lock (wx_groupObj)
            {
                try
                {
                    List<WxGroup> nWxGroup = new List<WxGroup>();

                    for (int i = 0; i < wxGroup.Count; i++)
                    {
                        WxGroup n = wxGroup[i];
                        if (groupId == n.groupid)
                        {
                            n.member = this.Wx_GetGroupMember(wxGroup[i].groupid);
                        }
                        if (n.member != null)
                        {
                            nWxGroup.Add(n);
                        }
                    }
                    wxGroup = nWxGroup;
                    TcpSendMsg(TcpMsg.Group, wxGroup);
                }
                catch
                {
                    return false;
                }
            }
            return false;
        }

        /// <summary>
        /// 微信消息 - 回调
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        public unsafe void Wx_MsgCallBack(int a, int b)
        {
            if (b == -1)
            {
                TcpSendMsg(TcpMsg.Out, wxUser);
                return;
            }
            fixed (int* wxUser1 = &pointerWxUser, callBackMsg1 = &callBackMsg)
            {
                try
                {
                    XzyWxApis.WXSyncMessage(pointerWxUser, (int)callBackMsg1);
                    if (callBackMsg == 0)
                    {
                        return;
                    }
                    var str = MarshalNativeToManaged((IntPtr)callBackMsg).ToString();
                    List<BackWxMsg> BackWxMsg = new List<BackWxMsg>();
                    Wx_ReleaseEX(ref callBackMsg);
                    List<WxTtsMsg> WXttsmsg = Newtonsoft.Json.JsonConvert.DeserializeObject<List<WxTtsMsg>>(str);
                    foreach (var msg in WXttsmsg)
                    {
                        var msgtype = msg.MsgType;
                        var content = msg.Content;
                        var sub_type = msg.SubType;
                        var MsgId = msg.MsgId;
                        if (msg.Timestamp < TimeStamp)
                        {
                            continue;
                        }

                        //32768 主动删除好友
                        //5 浏览通知。比如手机上打开群时，会得到5的通知
                        //2048 估计是二维码登录成功的通知

                        //Timestamp = 1520153620
                        //<msg><appmsg appid="" sdkver=""><title><![CDATA[邀请你加入群聊]]></title><des><![CDATA["红毛哥哥，(招财进宝)"邀请你加入群聊🌸  招财进宝🌸  三点比！！，进入可查看详情。]]></des><action>view</action><type>5</type><showtype>0</showtype><content></content><url><![CDATA[http://support.weixin.qq.com/cgi-bin/mmsupport-bin/addchatroombyinvite?ticket=AxmPr0y71kZ0GHNfgHzvMA%3D%3D]]></url><thumburl><![CDATA[http://weixin.qq.com/cgi-bin/getheadimg?username=c718f942d57830318779f76095c014e2f561b16364ef1eef132a316aa94fcc5a]]></thumburl><lowurl></lowurl><appattach><totallen>0</totallen><attachid></attachid><fileext></fileext></appattach><extinfo></extinfo></appmsg><appinfo><version></version><appname></appname></appinfo></msg>

                        if (sub_type == 42)
                        {
                            var username = Utilities.GetMidStr(msg.Content, "username=\"", "\" nickname=").Trim();
                            if (username.Length > 20)
                            {
                                //v1
                            }
                        }

                        //判断此消息是否已经处理过。若未处理，才会进入里面处理
                        if (Wx_SetMsgKey(MsgId))
                        {
                            if (sub_type == 49)
                            {
                                if (content.IndexOf("加入群聊") != -1)
                                {
                                    var url = Utilities.GetMidStr(content, "<url><![CDATA[", "]]>");
                                    Task.Factory.StartNew(delegate { Wx_IntoGroup(url); });
                                }
                                else if (content.IndexOf("微信转账") != -1)//当sub_type=49，并且content内容包含“微信转账”时，表示这是一笔微信转账通知
                                {
                                    Task.Factory.StartNew(delegate
                                    {
                                        fixed (int* WxUser1 = &pointerWxUser, pushStr1 = &pushStr)
                                        {
                                            XzyWxApis.WXTransferOperation(pointerWxUser, Newtonsoft.Json.JsonConvert.SerializeObject(msg), (int)pushStr1);

                                            var datas23 = MarshalNativeToManaged((IntPtr)pushStr);
                                            var str23 = datas23.ToString();
                                            WxDelegate.show(str23);
                                            Wx_ReleaseEX(ref pushStr);
                                        }
                                    });
                                }
                            }
                            if (sub_type == 10000)
                            {
                                if (msg.Content.IndexOf("邀请") != -1)
                                {
                                    //更新群成员
                                    if (wxGroup != null)
                                    {
                                        Task.Factory.StartNew(delegate { Wx_SetGroup(msg.FromUser); });
                                    }
                                }
                            }
                            if (sub_type == 10002)
                            {
                                if (msg.Content.IndexOf("撤回") != -1)
                                {
                                    //<newmsgid>7189063840892613759</newmsgid>

                                    //撤回消息
                                    var chid = Utilities.GetMidStr(msg.Content, "<newmsgid>", "</newmsgid>");
                                    if (chid != "")
                                    {
                                        BackWxMsg chmsg = new BackWxMsg();
                                        chmsg.wxid = this.wxUser.wxid;
                                        chmsg.groupid = msg.FromUser;
                                        chmsg.msgid = MsgId;
                                        chmsg.chmsgid = chid;
                                        BackWxMsg.Add(chmsg);
                                    }
                                }
                            }
                            if (sub_type == 1) //文字消息
                            {
                                if (content != null)
                                {
                                    var des = content.Trim();
                                    if (des != "")
                                    {
                                        Console.WriteLine(des + "--" + sub_type.ToString());

                                        //来源
                                        var from_user = msg.FromUser;

                                        var arr = msg.Content.Trim().Split(new string[] { ":\n" }, StringSplitOptions.None);
                                        if (arr.Length >= 2)
                                        {

                                            BackWxMsg BackWxMsgs = new BackWxMsg();

                                            BackWxMsgs.wxid = arr[0];
                                            arr[0] = "";
                                            BackWxMsgs.content = string.Join("", arr);
                                            BackWxMsgs.groupid = from_user;
                                            BackWxMsgs.inputtime = msg.Timestamp;
                                            BackWxMsgs.msgid = MsgId;
                                            BackWxMsgs.chmsgid = null;
                                            BackWxMsg.Add(BackWxMsgs);
                                        }
                                    }
                                }
                            }
                            else if (sub_type == 49 && content.IndexOf("CDATA[1002]") != -1) //红包
                            {
                                var redpackjson = Newtonsoft.Json.JsonConvert.SerializeObject(msg);
                                var packtouser = msg.ToUser;//发包人
                                var packforuser = msg.FromUser;//qun



                                Dictionary<string, packitme> data = null;
                                Task.Factory.StartNew(delegate { data = RedpackOK(redpackjson, msg.Timestamp); });
                            }
                            else if (sub_type == 3 || sub_type == 47) //图片
                            {
                                var img = "";
                            }
                            else if (sub_type == 37)
                            {
                                //37代表：对方主动加我们好友时，微信发过一个回调消息，消息类型是37。
                                //通过这个变量： var content = msg.Content;
                                //可以得到stranger的v1参数和v2参数
                                //然后调用此接口：public static extern void WXAcceptUser(int objects, string stranger, string ticket, int result);
                                //就会自动同意此好友请求。其中该接口的参数stranger就是v1，ticket就是v2参数
                            }
                            else if (sub_type == 34)//34指语音消息，
                            {
                                Task.Factory.StartNew(delegate
                                {
                                    fixed (int* WxUser1 = &pointerWxUser, pushStr1 = &pushStr)
                                    {
                                        XzyWxApis.WXGetMsgVoice(pointerWxUser, Newtonsoft.Json.JsonConvert.SerializeObject(msg), (int)pushStr1);

                                        var datas23 = MarshalNativeToManaged((IntPtr)pushStr);
                                        var str23 = datas23.ToString();
                                        WxDelegate.show(str23);
                                        Wx_ReleaseEX(ref pushStr);
                                    }
                                });
                            }
                            else if (sub_type == 51)
                            {
                                //51  自己主动查看群信息
                            }
                        }
                    }
                    this.TcpSendMsg(TcpMsg.content, BackWxMsg);
                }
                catch
                {
                    Console.WriteLine("异常事件");
                }
            }
        }

        //读红包key
        public unsafe Dictionary<string, packitme> RedpackOK(string json, int Timestamp)
        {
            fixed (int* WxUser1 = &pointerWxUser, redpack1 = &redPack)
            {

                XzyWxApis.WXReceiveRedPacket(pointerWxUser, json, (int)redpack1);
                if ((int)redpack1 == 0) { return null; }
                var fromwxid = "";
                var key = MarshalNativeToManaged((IntPtr)redPack).ToString();
                if (key == null)
                {
                    return null;
                }

                Wx_ReleaseEX(ref redPack);

                WXReceiveRedPacketJson wxReceiveRedPacketjson = Newtonsoft.Json.JsonConvert.DeserializeObject<WXReceiveRedPacketJson>(key);

                key = wxReceiveRedPacketjson.Key;
                RedPacketJson redPacketjson = Newtonsoft.Json.JsonConvert.DeserializeObject<RedPacketJson>(wxReceiveRedPacketjson.External);
                fromwxid = redPacketjson.SendUserName;

                WxDelegate.show(key);

                if (!this.SET_redpack_Key(key, json))
                {
                    return null;
                }
                else
                {
                    /*接收到新红包*/
                    this.CallBackRedPack(false, -2, "收到新红包", key, fromwxid, Timestamp);

                    //将收到新红包的时间等日志信息，记录到数据库中。
                    //db.apck_insert(new redpack() { inputtime = DateTime.Now, pack_key = key, pack_json = json, groupid = fromwxid });
                    WxDelegate.show("收到新红包");
                }

                #region 领取红包
                XzyWxApis.WXOpenRedPacket(pointerWxUser, json, key, (int)redpack1);
                if ((int)redpack1 == 0) { return null; }
                var datas22 = MarshalNativeToManaged((IntPtr)redPack);
                var str22 = datas22.ToString();
                WxDelegate.show(str22);
                Wx_ReleaseEX(ref redPack);
                #endregion

                #region 循环接收红包事件，先隐藏掉，马志远
                double time = Utilities.GetTimestamp;
                //while (true)
                //{
                //    Thread.Sleep(500);

                //读取红包，要在领了红包后再调用此方法查看
                XzyWxApis.WXQueryRedPacket(pointerWxUser, json, 0, (int)redpack1);
                if ((int)redpack1 == 0) { return null; }
                var datas = MarshalNativeToManaged((IntPtr)redPack);
                var str = datas.ToString();
                Wx_ReleaseEX(ref redPack);

                ReadPackJson redpackjson = Newtonsoft.Json.JsonConvert.DeserializeObject<ReadPackJson>(str);
                Dictionary<string, packitme> ipackitme = new Dictionary<string, packitme>();
                if (redpackjson.External != "")
                {
                    ReadPackItem redpackitem = Newtonsoft.Json.JsonConvert.DeserializeObject<ReadPackItem>(redpackjson.External);
                    if (redpackitem.HeadTitle != null)
                    {
                        var nowcount = redpackitem.RecNum;
                        var count = redpackitem.TotalNum;

                        WxDelegate.show(string.Format("当前{0}-{1}", nowcount, count));

                        if (nowcount == count || redpackitem.HeadTitle.IndexOf("被抢光") != -1)
                        {
                            this.CallBackRedPack(false, -1, "红包被抢光,读包中", key, fromwxid, Timestamp);
                            WxDelegate.show("红包被抢光,读包中");
                        #region  抢光之后开始翻页
                        Bk2:;
                            var countpage = Convert.ToInt32(Convert.ToDouble(count) / 11.00);
                            if (count % 11 > 0)
                            {
                                countpage = countpage + 1;
                            }
                            ipackitme = new Dictionary<string, packitme>();

                            var index = 0;
                            List<packitme> ilist = new List<packitme>();

                            for (int i = 0; i < countpage + countpage; i++)
                            {

                                XzyWxApis.WXQueryRedPacket(pointerWxUser, json, i, (int)redpack1);
                                if ((int)redpack1 == 0) { return null; }
                                var datas1 = MarshalNativeToManaged((IntPtr)redPack);
                                var str1 = datas1.ToString();
                                Wx_ReleaseEX(ref redPack);

                                var redpackjson1 = Newtonsoft.Json.JsonConvert.DeserializeObject<ReadPackJson>(str1);
                                var redpackitem1 = Newtonsoft.Json.JsonConvert.DeserializeObject<ReadPackItem>(redpackjson1.External);
                                this.CallBackRedPack(false, i + 1, string.Format("读红包第{0}页", i + 1), key, fromwxid, Timestamp);
                                foreach (var rec in redpackitem1.Record)
                                {
                                    packitme packitme = Newtonsoft.Json.JsonConvert.DeserializeObject<packitme>(rec.ToString());

                                    if (!ipackitme.ContainsKey(packitme.UserName))
                                    {
                                        packitme.xh = index;

                                        ipackitme.Add(packitme.UserName, packitme);
                                        index++;
                                        ilist.Add(packitme);
                                    }
                                }

                            }


                            if (index == count)
                            {
                                this.CallBackRedPack(false, 0, "读包完毕", key, fromwxid, Timestamp, ipackitme);
                                return ipackitme;

                            }
                            else
                            {
                                goto Bk2;
                            }
                            #endregion
                        }
                        else
                        {
                            if (Utilities.GetTimestamp - time > 60 * 1000)
                            {
                                this.CallBackRedPack(false, -3, "红包超时", key, fromwxid, Timestamp, ipackitme);
                                return null;
                            }
                        }
                    }
                }

                //}
                #endregion

                return null;
            }
        }

        public Dictionary<string, string> dic_redpack { get; set; }
        public object obj = new object();

        public bool SET_redpack_Key(string key, string json)
        {
            lock (obj)
            {
                try
                {
                    if (dic_redpack == null)
                    {
                        dic_redpack = new Dictionary<string, string>();
                    }

                    if (!dic_redpack.ContainsKey(key))
                    {
                        dic_redpack.Add(key, json);
                        return true;
                    }
                }
                catch
                {

                    return false;
                }
            }
            return false;
        }


        /// <summary>
        /// 收到新红包时的回调处理
        /// </summary>
        /// <param name="ok"></param>
        /// <param name="page"></param>
        /// <param name="msg"></param>
        /// <param name="key"></param>
        /// <param name="fromuser"></param>
        /// <param name="Timestamp"></param>
        /// <param name="dic"></param>
        public void CallBackRedPack(bool ok, int page, string msg, string key, string fromuser, int Timestamp, Dictionary<string, packitme> dic = null)
        {
            PackMsg packmsg = new PackMsg();
            packmsg.msg = msg;
            packmsg.key = key;
            packmsg.fromuser = fromuser;
            packmsg.Timestamp = Timestamp;
            if (ok)
            {
                packmsg.ok = true;
                packmsg.packitme = dic;
            }
            else
            {
                packmsg.ok = false;
                packmsg.page = page;

            }

            this.TcpSendMsg(TcpMsg.redpack, packmsg);
        }

        /// <summary>
        /// 释放内存
        /// </summary>
        /// <param name="hande"></param>
        public void Wx_ReleaseEX(ref int hande)
        {
            XzyWxApis.WXRelease(hande);
            hande = 0;
        }

        #endregion
    }
}

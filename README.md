# 微信mac协议。
 请进入  http://www.keduoduo.online/ 进行注册授权，
 注册后将授权配置到app.config即可使用
 支持功能如下

· -- 

        /// <summary>
        /// 账户与密码登录方式
        /// </summary>
        /// <param name="objects">接口对象指针</param>
        /// <param name="user">用户名</param>
        /// <param name="password">二维码验证密码</param>
        /// <param name="result">二级指针，json字符串，返回执行结果</param>
        /// <returns></returns>
        [DllImport("wxipadapi6.7.dll", EntryPoint = "WXUserLogin")]
        public static extern void WXUserLogin(int objects, string user, string password, int result);

        /// <summary>
        /// 手机登陆
        /// </summary>
        /// <param name="objects">接口对象指针</param>
        /// <param name="user">用户名</param>
        /// <param name="password">二维码验证密码</param>
        /// <param name="result">二级指针，json字符串，返回执行结果</param>
        /// <returns></returns>
        public static extern void WXPhoneLogin(int objects, string user, string password, int result);

        /// <summary>
        /// 获取登陆验证码
        /// </summary>
        /// <param name="objects">接口对象指针</param>
        /// <param name="phone_number">手机号码,格式+8613666666666</param>
        /// <param name="result">二级指针，json字符串，返回执行结果</param>
        /// <returns></returns>
        public static extern void WXGetLoginVerifyCode(int objects, string phone_number, int result);

        /// <summary>
        /// 发送登陆验证码
        /// </summary>
        /// <param name="objects">接口对象指针</param>
        /// <param name="phone_number">手机号码,格式+8613666666666</param>
        /// <param name="verify_code">验证码</param>
        /// <param name="result">二级指针，json字符串，返回执行结果</param>
        /// <returns></returns>
        public static extern void WXSendLoginVerifyCode(int objects, string phone_number, string verify_code, int result);

        /// <summary>
        /// 获取指定用户详细信息
        /// </summary>
        /// <param name="objects">接口对象指针</param>
        /// <param name="user">目标微信帐户</param>
        /// <param name="result">二级指针，json字符串，返回执行结果</param>
        public static extern void WXGetContact(int objects, string user, int result);

        /// <summary>
        /// 获取好友朋友圈信息
        /// </summary>
        /// <param name="objects">接口对象指针</param>
        /// <param name="user">对方用户名</param>
        /// <param name="id">获取到的最后一次的id，第一次调用设置为空</param>
        /// <param name="result"></param>
        public static extern void WXSnsUserPage(int objects, string user, string id, int result);

        /// <summary>
        /// 获取朋友圈消息详情(例如评论)
        /// </summary>
        /// <param name="objects"></param>
        /// <param name="id">朋友圈消息id</param>
        /// <param name="result"></param>
        public static extern void WXSnsObjectDetail(int objects, string id, int result);

        /// <summary>
        /// 朋友圈操作(删除评论，取消赞)
        /// </summary>
        /// <param name="objects"></param>
        /// <param name="id">朋友圈消息id</param>
        /// <param name="type">操作类型,4为删除评论，5为取消赞</param>
        /// <param name="comment">当type为4时，对应删除评论的id，通过WXSnsObjectDetail接口获取。当type为5时，comment不可用，置为0.</param>
        /// <param name="comment_type">
        /// 此参数马志远增加。
        /// 原备注：评论类型,当删除评论时可用，2或者3.(规律未知)
        /// 
        /// 马志远已研究得到的新备注：
        /// 当type=4，且comment_type=3时，将删除指定的comment的评论id的评论
        /// 当type=5时，comment和comment_type随便填，不影响。这是取消赞的。
        /// </param>
        /// <param name="result"></param>
        public static extern void WXSnsObjectOp(int objects, string id, int type, int comment, int comment_type, int result);

        /// <summary>
        /// 搜索指定用户详细信息
        /// </summary>
        /// <param name="objects"></param>
        /// <param name="user">目标微信帐户</param>
        /// <param name="result"></param>
        public static extern void WXSearchContact(int objects, string user, int result);

        /// <summary>
        /// 主动添加好友
        /// </summary>
        /// <param name="objects"></param>
        /// <param name="stranger">对方stranger字符串，例如v1_caa184cca678097XXXXXXXXXXXXXXXXXXXX70340e8ae5f3cad28347ad4@stranger</param>
        /// <param name="strangerV2">对方stranger字符串</param>
        /// <param name="type">
        /// 添加好友来源
        /// type来源值：
        /// 1   -通过QQ好友添加--可以
        /// 2   -通过搜索邮箱--可加但无提示
        /// 3   -通过微信号搜索--可以
        /// 5   -通过朋友验证消息-可加但无提示
        /// 7   -通过朋友验证消息(可回复)-可加但无提示
        /// 12  -来自QQ好友--可以
        /// 13  -通过手机通讯录添加--可以
        /// 14  -通过群来源--no
        /// 15  -通过搜索手机号--可以
        /// 16  -通过朋友验证消息-可加但无提示
        /// 17  -通过名片分享--no
        /// 18  -通过附近的人--可以(貌似只需要v1就够了)
        /// 22  -通过摇一摇打招呼方式--可以
        /// 25  -通过漂流瓶---no
        /// 30  -通过二维码方式--可以
        /// </param>
        /// <param name="verify">添加好友时的验证信息</param>
        /// <param name="result"></param>
        public static extern void WXAddUser(int objects, string stranger, string strangerV2, int type, string verify, int result);

        /// <summary>
        /// 删除指定好友
        /// </summary>
        /// <param name="objects"></param>
        /// <param name="user">对方用户名</param>
        /// <param name="result"></param>
        public static extern void WXDeleteUser(int objects, string user, int result);

        /// <summary>
        /// 接受好友请求
        /// </summary>
        /// <param name="objects"></param>
        /// <param name="stranger">对方stranger字符串，这是v1参数，例如v1_caa184cca678097XXXXXXXXXXXXXXXXXXXX70340e8ae5f3cad28347ad4@stranger</param>
        /// <param name="ticket">好友请求ticket，这是stranger的v2参数。不论是v1还是v2，在某人加我为好友时，微信就会发起回调，在回调中，我们会得到v1和v2的参数值</param>
        /// <param name="result"></param>
        public static extern void WXAcceptUser(int objects, string stranger, string ticket, int result);

        /// <summary>
        /// 心跳
        /// </summary>
        /// <param name="objects">接口指针对象WxUser</param>
        /// <param name="result">二级指针，返回执行结果</param>
        public static extern void WXHeartBeat(int objects, int result);

        /// <summary>
        /// 获取登录token
        /// </summary>
        /// <param name="objects"></param>
        /// <param name="result"></param>
        public static extern void WXGetLoginToken(int objects, int result);

        /// <summary>
        /// 使用62数据和token实现二次自动登录
        /// </summary>
        /// <param name="objects"></param>
        /// <param name="token">调用WXGetLoginToken获取token字段数据</param>
        /// <param name="result"></param>
        public static extern void WXLoginRequest(int objects, string token, int result);

        /// <summary>
        /// 断线后重连，二次自动登录。一般非断线下如果要自动登录，是使用WXLoginRequest方法来做二次登录。若断线再连就使用WXAutoLogin
        /// </summary>
        /// <param name="objects"></param>
        /// <param name="token">调用WXGetLoginToken获取token字段数据</param>
        /// <param name="result"></param>
        public static extern void WXAutoLogin(int objects, string token, int result);

        /// <summary>
        /// 加载62数据
        /// </summary>
        /// <param name="objects">接口指针对象WxUser</param>
        /// <param name="wxdata">62数据内容</param>
        /// <param name="wx_size">62数据内容</param>
        /// <param name="result">二级指针，返回执行结果</param>
        public static extern void WXLoadWxDat(int objects, byte[] wxdata, int wx_size, int result);

        /// <summary>
        /// 获取url访问token
        /// </summary>
        /// <param name="objects"></param>
        /// <param name="user">公众号用户名gh_*******开头的</param>
        /// <param name="url">http://xxxxxxxxxxxxxx访问连接</param>
        /// <param name="result"></param>
        public static extern void WXGetRequestToken(int objects, string user, string url, int result);

        /// <summary>
        /// 返回公众号信息
        /// </summary>
        /// <param name="objects"></param>
        /// <param name="user">公众号或V1</param>
        /// <param name="result"></param>
        public static extern void WXGetSubscriptionInfo(int objects, string user, int result);

        /// <summary>
        /// 设置头像
        /// </summary>
        /// <param name="objects">接口指针对象WxUser</param>
        /// <param name="image_data">图片数据</param>
        /// <param name="image_size">图片大小</param>
        /// <param name="result">二级指针，返回执行结果</param>
        public static extern void WXSetHeadImage(int objects, byte[] image_data, int image_size, int result);

        /// <summary>
        /// 发送文字消息
        /// </summary>
        /// <param name="objects">接口指针对象WxUser</param>
        /// <param name="user">对方用户名</param>
        /// <param name="content">消息内容</param>
        /// <param name="at">@好友列表，json数组 {"wxid1","wxid2"} 不at传null</param>
        /// <param name="result">二级指针，返回执行结果串，返回执行结果</param>
        public static extern void WXSendMsg(int objects, string user, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(UTF8Marshaler))] string content, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(UTF8Marshaler))] string at, int result);

        /// <summary>
        /// 设置代理
        /// </summary>
        /// <param name="objects"></param>
        /// <param name="proxy">http代理服务器，格式192.168.1.1:8888 </param>
        /// <param name="type">代理类型，1为http代理，2为socks4，3为socks5(需要用户名和密码)</param>
        /// <param name="user">代理的用户名,socks5需要。</param>
        /// <param name="password">代理的密码,socks5需要。</param>
        /// <param name="result"></param>
        public static extern void WXSetProxyInfo(int objects, string proxy, int type, string user, string password, int result);

        /// <summary>
        /// 查看附近的人
        /// </summary>
        /// <param name="objects"></param>
        /// <param name="longitude">浮点数，经度</param>
        /// <param name="latitude">浮点数，维度</param>
        /// <param name="result"></param>
        public static extern void WXGetPeopleNearby(int objects, float logitude, float latitude, int result);

        /// <summary>
        /// 接收红包
        /// </summary>
        /// <param name="objects"></param>
        /// <param name="red_packet">整个红包消息结构</param>
        /// <param name="result"></param>
        public static extern void WXReceiveRedPacket(int objects, string red_packet, int result);

        /// <summary>
        /// 查看红包
        /// </summary>
        /// <param name="objects"></param>
        /// <param name="red_packet">整个红包消息结构</param>
        /// <param name="index">0代表第一页 11代表第二页  +11代表翻页</param>
        /// <param name="result"></param>
        public static extern void WXQueryRedPacket(int objects, string red_packet, int index, int result);

        /// <summary>
        /// 发送图片消息
        /// </summary>
        /// <param name="objects"></param>
        /// <param name="toUser">接收目标</param>
        /// <param name="image_data">图片</param>
        /// <param name="image_size">图片大小</param>
        /// <param name="result"></param>
        public static extern void WXSendImage(int objects, string toUser, byte[] image_data, int image_size, int result);

        /// <summary>
        /// 打招呼
        /// </summary>
        /// <param name="objects"></param>
        /// <param name="stranger">对方的stranger串</param>
        /// <param name="content">消息内容，可为空。为空时显示打了个招呼</param>
        /// <param name="result"></param>
        public static extern void WXSayHello(int objects, string stranger, string content, int result);

        /// <summary>
        /// 取消堆
        /// </summary>
        /// <param name="IP"></param>
        /// <param name="p"></param>
        /// <returns></returns>
        //public static extern int Auth(string key);

        /// <summary>
        /// 取消堆，此方法我估计是提供这些微信接口的人整出来的，实际与微信无关。目的仅是为了控制盈利
        /// </summary>
        /// <param name="IP">连接服务器的ip地址</param>
        /// <param name="port">该服务器的端口号</param>
        /// <returns></returns>
        public static extern int WXSetNetworkVerifyInfo(string ip, int port);

        /// <summary>
        /// 取消堆
        /// </summary>
        /// <param name="IP"></param>
        /// <param name="p"></param>
        /// <returns></returns>
        public static extern int test();

        /// <summary>
        /// 获取消息语音(语音消息大于20秒时通过该接口获取)
        /// </summary>
        /// <param name="objects">接口指针对象WxUser</param>
        /// <param name="msg">收到的整个语音消息</param>
        /// <param name="result">二级指针，返回执行结果</param>
        /// <returns></returns>
        public static extern int WXGetMsgVoice(int objects, string msg, int result);

        /// <summary>
        /// 接受转账
        /// </summary>
        /// <param name="objects"></param>
        /// <param name="transfer">转账消息</param>
        /// <param name="result"></param>
        /// <returns></returns>
        public static extern int WXTransferOperation(int objects, string transfer, int result);

        /// <summary>
        /// 发送朋友圈
        /// </summary>
        /// <param name="objects"></param>
        /// <param name="content">消息结构</param>
        /// <param name="result"></param>
        /// <returns></returns>
        public static extern int WXSendMoments(int objects, string content, int result);

        /// <summary>
        /// 群发消息
        /// </summary>
        /// <param name="objects"></param>
        /// <param name="user">用户名json数组 ["AB1","AC2","AD3"]</param>
        /// <param name="content">消息内容，可为空。为空时显示打了个招呼</param>
        /// <param name="result"></param>
        /// <returns></returns>
        public static extern int WXMassMessage(int objects, string user, string content, int result);

        /// <summary>
        /// 生成62数据
        /// 
        /// 马志远：
        /// 注意：62数据是扫码登录成功后，再获取，并保存下来，而不是其它方式登录后再保存。并且还要使用方法WXGetLoginToken保存下token
        /// </summary>
        /// <param name="objects"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        public static extern int WXGenerateWxDat(int objects, int result);

        /// <summary>
        /// 领取红包
        /// </summary>
        /// <param name="objects"></param>
        /// <param name="red_packet">整个红包消息结构</param>
        /// <param name="key">通过接受红包返回的key</param>
        /// <param name="result"></param>
        /// <returns></returns>
        public static extern int WXOpenRedPacket(int objects, string red_packet, string key, int result);

        /// <summary>
        /// 发送APP消息(分享应用或者朋友圈链接等)
        /// </summary>
        /// <param name="objects">接口指针对象WxUser</param>
        /// <param name="user">对方用户名</param>
        /// <param name="content">消息内容(整个消息结构<appmsg xxxxxxxxx>)</param>
        /// <param name="result">二级指针，返回执行结果</param>
        /// <returns></returns>
        public static extern int WXSendAppMsg(int objects, string user, string content, int result);

        /// <summary>
        /// 同步收藏消息(用户获取收藏对象的id)
        /// </summary>
        /// <param name="objects"></param>
        /// <param name="key">同步的key，第一次调用设置为空</param>
        /// <param name="result"></param>
        /// <returns></returns>
        public static extern int WXFavSync(int objects, string key, int result);

        /// <summary>
        /// 添加收藏
        /// </summary>
        /// <param name="objects"></param>
        /// <param name="fav_object">收藏对象结构(<favitem type=5xxxxxx)</param>
        /// <param name="result"></param>
        /// <returns></returns>
        public static extern int WXFavAddItem(int objects, string fav_object, int result);

        /// <summary>
        /// 获取收藏对象的详细信息
        /// </summary>
        /// <param name="objects"></param>
        /// <param name="id">收藏对象id</param>
        /// <param name="result"></param>
        /// <returns></returns>
        public static extern int WXFavGetItem(int objects, int id, int result);

        /// <summary>
        /// 删除收藏对象
        /// </summary>
        /// <param name="objects"></param>
        /// <param name="id">收藏对象id</param>
        /// <param name="result"></param>
        /// <returns></returns>
        public static extern int WXFavDeleteItem(int objects, int id, int result);

        /// <summary>
        /// 获取所有标签列表
        /// </summary>
        /// <param name="objects"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        public static extern int WXGetContactLabelList(int objects, int result);

        /// <summary>
        /// 添加标签
        /// </summary>
        /// <param name="objects"></param>
        /// <param name="label">标签内容</param>
        /// <returns></returns>
        public static extern int WXAddContactLabel(int objects, string label, int result);

        /// <summary>
        /// 从列表删除标签
        /// </summary>
        /// <param name="objects"></param>
        /// <param name="id">标签id</param>
        /// <param name="result"></param>
        /// <returns></returns>
        public static extern int WXDeleteContactLabel(int objects, string id, int result);

        /// <summary>
        /// 设置用户标签
        /// </summary>
        /// <param name="objects"></param>
        /// <param name="user">用户名</param>
        /// <param name="id">标签id</param>
        /// <param name="result"></param>
        /// <returns></returns>
        public static extern int WXSetContactLabel(int objects, string user, string id, int result);

        /// <summary>
        /// 分享名片
        /// </summary>
        /// <param name="objects">接口指针对象WxUser</param>
        /// <param name="user">对方用户名</param>
        /// <param name="wxid">微信ID</param>
        /// <param name="title">标题</param>
        /// <returns></returns>
        public static extern int WXShareCard(int objects, string user, string wxid, string title, int result);

        /// <summary>
        /// 执行公众号菜单
        /// </summary>
        /// <param name="objects"></param>
        /// <param name="user">公众号用户名gh* 开头的</param>
        /// <param name="id">通过WXGetSubscriptionInfo获取</param>
        /// <param name="key">通过WXGetSubscriptionInfo获取</param>
        /// <param name="result"></param>
        /// <returns></returns>
        public static extern int WXSubscriptionCommand(int objects, string user, UInt32 id, string key, int result);

        /// <summary>
        /// 访问/阅读url
        /// </summary>
        /// <param name="objects"></param>
        /// <param name="url">访问的链接</param>
        /// <param name="key">token中的key</param>
        /// <param name="uin">token中的uin</param>
        /// <param name="result"></param>
        /// <returns></returns>
        public static extern int WXRequestUrl(int objects, string url, string key, string uin, int result);

        /// <summary>
        /// 重置同步信息
        /// </summary>
        /// <param name="objects">接口指针对象WxUser</param>
        /// <returns></returns>
        public static extern int WXSyncReset(int objects);

        /// <summary>
        /// 添加群成员
        /// </summary>
        /// <param name="objects">接口指针对象WxUser</param>
        /// <param name="chatroom">群用户名</param>
        /// <param name="user">成员用户名</param>
        /// <param name="result">二级指针，返回执行结果</param>
        /// <returns></returns>
        public static extern int WXAddChatRoomMember(int objects, string chatroom, string user, int result);

        /// <summary>
        /// 创建群
        /// </summary>
        /// <param name="objects">接口指针对象WxUser</param>
        /// <param name="user">用户名json数组，例如["wxid_g58r112lnw0q22","my26259675"]</param>
        /// <param name="result">二级指针，返回执行结果</param>
        /// <returns></returns>
        public static extern int WXCreateChatRoom(int objects, string user, int result);

        /// <summary>
        /// 邀请群成员
        /// </summary>
        /// <param name="objects">接口指针对象WxUser</param>
        /// <param name="chatroom">群用户名</param>
        /// <param name="user">成员用户名</param>
        /// <param name="result">二级指针，返回执行结果</param>
        /// <returns></returns>
        public static extern int WXInviteChatRoomMember(int objects, string chatroom, string user, int result);

        /// <summary>
        /// 获取二维码信息
        /// </summary>
        /// <param name="objects"></param>
        /// <param name="path">本地二维码图片全路径</param>
        /// <param name="result"></param>
        /// <returns></returns>
        public static extern int WXQRCodeDecode(int objects, string path, int result);

        /// <summary>
        /// 获取其他设备登陆请求
        /// </summary>
        /// <param name="objects"></param>
        /// <param name="login_url">通过二维码扫描获取的url</param>
        /// <param name="result"></param>
        /// <returns></returns>
        public static extern int WXExtDeviceLoginGet(int objects, string login_url, int result);

        /// <summary>
        /// 确认其他设备登陆请求
        /// </summary>
        /// <param name="objects"></param>
        /// <param name="login_url">通过二维码扫描获取的url</param>
        /// <param name="result"></param>
        /// <returns></returns>
        public static extern int WXExtDeviceLoginOK(int objects, string login_url, int result);

        /// <summary>
        /// 发送语音消息
        /// </summary>
        /// <param name="objects">接口对象指针</param>
        /// <param name="toUser">接收目标</param>
        /// <param name="voice_data">语音数据</param>
        /// <param name="voice_size">语音大小</param>
        /// <param name="voice_time">语音时间(毫秒，最大60 * 1000)</param>
        /// <param name="result"></param>
        /// <returns></returns>
        public static extern int WXSendVoice(int objects, string toUser, byte[] voice_data, int voice_size, int voice_time, int result);
        
        /// <summary>
        /// 设置个人信息
        /// </summary>
        /// <param name="objects"></param>
        /// <param name="nick_name">昵称</param>
        /// <param name="unsigned">签名</param>
        /// <param name="sex">性别，1男，2女</param>
        /// <param name="country">国家，CN</param>
        /// <param name="provincia">地区，省，Zhejiang</param>
        /// <param name="city">城市，Hangzhou</param>
        /// <param name="result">二级指针，返回执行结果</param>
        /// <returns></returns>
        public static extern int WXSetUserInfo(int objects, string nick_name, string unsigned, int sex, string country, string provincia, string city, int result);

        /// <summary>
        /// 初始化接口(对象)
        /// </summary>
        /// <param name="objects">接口对象二级指针,分配一个对象</param>
        /// <param name="device_name">格式 xxxx 的ipad</param>
        /// <param name="device_type">格式 
        ///     <softtype>
        ///         <k3>9.0.2</k3><k9>iPad</k9><k10>2</k10>
        ///         <k19>58BF17B5-2D8E-4BFB-A97E-38F1226F13F8</k19><k20>710DECBB-7176-4E50-83D3-C24BA2070356</k20>
        ///         <k21>neihe_5GHz</k21><k22>(null)</k22>
        ///         <k24>94:10:3e:8e:8:43</k24><k33>\345\276\256\344\277\241</k33>
        ///         <k47>1</k47><k50>1</k50>
        ///         <k51>com.tencent.xin</k51>
        ///         <k54>iPad4,4</k54>
        ///     </softtype>
        /// </param>
        /// <param name="device_uuid">格式 以上面k20一致 710DECBB-7176-4E50-83D3-C24BA2070356</param>
        /// <returns></returns>
        public static extern int WXInitialize(int objects, string device_name, string device_type, string device_uuid);

        /// <summary>
        /// 获取扫码登录二维码
        /// </summary>
        /// <param name="objects"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        public static extern int WXGetQRCode(int objects, int result);

        /// <summary>
        /// 设置http服务地址相关信息
        /// </summary>
        /// <param name="objects"></param>
        /// <param name="server">服务地址</param>
        /// <param name="sign">开发者相关信息{"code":"123456"}</param>
        /// <param name="timeout">超时时间</param>
        /// <param name="result"></param>
        /// <returns></returns>
        public static extern int WXSetHttpServer(int objects, string server, string sign, int timeout, int result);

        /// <summary>
        /// 获取群成员资料
        /// </summary>
        /// <param name="objects"></param>
        /// <param name="chatroom">群ID</param>
        /// <param name="result"></param>
        /// <returns></returns>
        public static extern int WXGetChatRoomMember(int objects, string chatroom, int result);

        /// <summary>
        /// 检查二维码状态 //需循环调用
        /// </summary>
        /// <param name="objects"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        public static extern int WXCheckQRCode(int objects, int result);

        /// <summary>
        /// 二维码登陆
        /// </summary>
        /// <param name="objects"></param>
        /// <param name="user">用户名</param>
        /// <param name="password">二维码验证密码</param>
        /// <param name="result"></param>
        /// <returns></returns>
        public static extern int WXQRCodeLogin(int objects, string user, string password, int result);

        /// <summary>
        /// 帐户注销
        /// </summary>
        /// <param name="objects"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        public static extern int WXLogout(int objects, int result);

        /// <summary>
        /// 同步包含通讯录及微信消息，不要与WXSyncMessage一起使用
        /// </summary>
        /// <param name="objects"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        public static extern int WXSyncMsg(int objects, int result);

        /// <summary>
        /// 只同步聊天信息，，不要与WXSyncMsg一起使用
        /// </summary>
        /// <param name="objects"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        public static extern int WXSyncMessage(int objects, int result);

        /// <summary>
        /// 只同步通讯录信息
        /// </summary>
        /// <param name="objects"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        public static extern int WXSyncContact(int objects, int result);

        /// <summary>
        /// 释放内存，使用一个函数后将接收返回值的指针做初始化释放内存
        /// </summary>
        /// <param name="result"></param>
        /// <returns></returns>
        public static extern int WXRelease(int result);

        /// <summary>
        /// 设置接收消息通知回调函数
        /// </summary>
        /// <param name="objects"> 接口指针对象WxUser</param>
        /// <param name="sync_msg_cb">回调函数指针</param>
        public static extern void WXSetRecvMsgCallBack(int objects, DllcallBack sync_msg_cb);

        /// <summary>
        /// 朋友圈图片上传
        /// </summary>
        /// <param name="objects"> 接口指针对象WxUser</param>
        /// <param name="sync_msg_cb">回调函数指针</param>
        public static extern void WXSnsUpload(int objects, byte[] image_data,int image_size,int result);

        /// <summary>
        /// 接口对象释放
        /// </summary>
        /// <param name="objects"></param>
        public static extern void WXObjectRelease(int objects);

        /// <summary>
        /// 设置同步消息回调函数
        /// </summary>
        /// <param name="objects"></param>
        /// <param name="sync_msg_cb"></param>
        public static extern void WXSetSyncMsgCallBack(int objects,int sync_msg_cb);

        /// <summary>
        /// 获取朋友圈动态
        /// </summary>
        /// <param name="objects"></param>
        /// <param name="id"></param>
        /// <param name="result"></param>
        public static extern void WXSnsTimeline(int objects,string id,int result);

        /// <summary>
        /// 验证密码
        /// </summary>
        /// <param name="objects"></param>
        /// <param name="password"></param>
        /// <param name="result"></param>
        public static extern void WXVerifyPassword(int objects, string password, int result);

        /// <summary>
        /// 设置密码
        /// </summary>
        /// <param name="objects"></param>
        /// <param name="password"></param>
        /// <param name="result"></param>
        public static extern void WXSetPassword(int objects, string password, int result);

        /// <summary>
        /// 设置微信ID
        /// </summary>
        /// <param name="objects"></param>
        /// <param name="id"></param>
        /// <param name="result"></param>
        public static extern void WXSetWeChatID(int objects, string id, int result);

        /// <summary>
        /// 踢出群成员
        /// </summary>
        /// <param name="objects"></param>
        /// <param name="chatroomid"></param>
        /// <param name="wxid"></param>
        /// <param name="result"></param>
        public static extern void WXDeleteChatRoomMember(int objects, string chatroomid, int wxid,int result);

        /// <summary>
        /// 退群
        /// </summary>
        /// <param name="objects"></param>
        /// <param name="chatroomid"></param>
        /// <param name="result"></param>
        public static extern void WXQuitChatRoom(int objects, string chatroomid, int result);

        /// <summary>
        /// 设置备注
        /// </summary>
        /// <param name="objects"></param>
        /// <param name="wxid"></param>
        /// <param name="signature"></param>
        /// <param name="result"></param>
        public static extern void WXSetUserRemark(int objects, string wxid,string signature, int result);

        /// <summary>
        /// 获取消息图片
        /// </summary>
        /// <param name="objects"></param>
        /// <param name="wxid"></param>
        /// <param name="msg"></param>
        /// <param name="result"></param>
        public static extern void WXGetMsgImage(int objects, string msg, int result);

        /// <summary>
        /// 获取消息视频
        /// </summary>
        /// <param name="objects"></param>
        /// <param name="wxid"></param>
        /// <param name="msg"></param>
        /// <param name="result"></param>
        public static extern void WXGetMsgVideo(int objects, string msg, int result);

        /// <summary>
        /// 取群二维码
        /// </summary>
        /// <param name="objects"></param>
        /// <param name="chatroomid"></param>
        /// <param name="style">是否使用风格化二维码</param>
        /// <param name="result"></param>
        public static extern void WXGetUserQRCode(int objects, string chatroomid,int style, int result);

        /// <summary>
        /// 修改群名称
        /// </summary>
        /// <param name="objects"></param>
        /// <param name="chatroomid"></param>
        /// <param name="comment"></param>
        /// <param name="result"></param>
        public static extern void WXSetChatroomName(int objects, string chatroomid, string comment, int result);

        /// <summary>
        /// 修改群公告
        /// </summary>
        /// <param name="objects"></param>
        /// <param name="chatroomid"></param>
        /// <param name="comment"></param>
        /// <param name="result"></param>
        public static extern void WXSetChatroomAnnouncement(int objects, string chatroomid, string comment, int result);

        /// <summary>
        /// 朋友圈消息评论
        /// </summary>
        /// <param name="objects"></param>
        /// <param name="wxid"></param>
        /// <param name="snsid"></param>
        /// <param name="comment"></param>
        /// <param name="reply_id"></param>
        /// <param name="result"></param>
        public static extern void WXSnsComment(int objects, string wxid,string snsid, string comment,int reply_id, int result);

        /// <summary>
        /// 登录验证处理
        /// </summary>
        /// <param name="objects"></param>
        /// <param name="ticket"></param>
        /// <param name="cookies"></param>
        /// <param name="answer"></param>
        /// <param name="result"></param>
        public static extern void WXLoginQuestion(int objects, string ticket, string cookies, string answer, int result);

        /// <summary>
        /// 登陆验证问题处理
        /// </summary>
        /// <param name="objects"></param>
        /// <param name="code"></param>
        /// <param name="result"></param>
        public static extern void WXLoginQuestionResult(int objects, string code,  int result);

        /// <summary>
        /// 同步朋友圈动态(好友评论或点赞自己朋友圈的消息)
        /// </summary>
        /// <param name="objects"></param>
        /// <param name="key"></param>
        /// <param name="result"></param>
        public static extern void WXSnsSync(int objects, string key, int result);

        /// <summary>
        /// 微信消息callback
        /// </summary>
        /// <param name="objects"></param>
        /// <param name="sync_msg_cb"></param>
        public delegate void DllcallBack(int objects, int sync_msg_cb);
-- ·

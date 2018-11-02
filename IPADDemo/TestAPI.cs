using IPADDemo.WeChat;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IPADDemo;

namespace IPADDemo
{
    public class TestAPI
    {
        public int pushStr;
        public unsafe void Test(int WXID=0)
        {
            fixed(int* pushStr1 = &pushStr){
                XzyWxApis.WXHeartBeat(WXID, (int)pushStr1);
                XzyWeChatThread xzyWeChat = new XzyWeChatThread();
                var datas = xzyWeChat.MarshalNativeToManaged((IntPtr)pushStr);
                xzyWeChat.Wx_ReleaseEX(ref pushStr);

                ////返回null
                //XzyWxApis.WXUserLogin(WXID, "bilizhou0903", "zhoubili0903", (int)pushStr1);
                //XzyWeChatThread xzyWeChat = new XzyWeChatThread();
                //var datas = xzyWeChat.MarshalNativeToManaged((IntPtr)pushStr);
                //xzyWeChat.Wx_ReleaseEX(ref pushStr);

                XzyWxApis.WXSendAppMsg(WXID, "wxid_z3sckhpgn43v21", "<appmsg appid='wx3fea7a3c94a23944' sdkver='0'><title>领取饿了么红包，看饿了别叫妈，和王祖蓝一起走火入魔</title><des>每周日0点爱奇艺，百变神咖-王祖蓝，魔性上演</des><action></action><showtype>0</showtype><mediatagname></mediatagname><messageext></messageext><messageaction></messageaction><content></content><url>https://m.ele.me/activities/hongbao?hardware_id=3fca613450f7e9a7bdc7d590e4f2ff8f&amp;track_id=1442566941%7c_f85c6d24-5de3-11e5-b23a-c81f66fb98a9%7cbd6b3eda48770a593bf2a444d486215b&amp;d18:20 2015/12/6 星期日vice_id=b325dc11-26e0-3c79-a918-955dc85b7b34&amp;sn=28cf845bf9a08aa4addc2044d9944866</url><lowurl></lowurl><dataurl></dataurl><lowdataurl></lowdataurl><appattach><totallen>0</totallen><attachid></attachid><emoticonmd5></emoticonmd5><fileext></fileext><cdnthumburl>30590201000452305002010002046d42c1c002030f424302041ff690750204566408a1042e6175706170706d73675f653831313963356364376633353464655f313434393333353932383332325f33313730350201000201000400</cdnthumburl><cdnthumblength>17478</cdnthumblength><cdnthumbwidth>100</cdnthumbwidth><cdnthumbheight>200</cdnthumbheight><cdnthumbaeskey>39333861323861363666626436616431</cdnthumbaeskey><aeskey>39333861323861363666626436616431</aeskey><encryver>0</encryver></appattach><extinfo></extinfo><commenturl></commenturl><thumburl></thumburl><md5></md5></appmsg>", (int)pushStr1);
                datas = xzyWeChat.MarshalNativeToManaged((IntPtr)pushStr);
                xzyWeChat.Wx_ReleaseEX(ref pushStr);

                ////返回null
                XzyWxApis.WXPhoneLogin(WXID, "+8618768177519", "zhoubili0903", (int)pushStr1);
                datas = xzyWeChat.MarshalNativeToManaged((IntPtr)pushStr);
                xzyWeChat.Wx_ReleaseEX(ref pushStr);

                ////返回null
                XzyWxApis.WXGetLoginVerifyCode(WXID, "+8618768177519", (int)pushStr1);
                datas = xzyWeChat.MarshalNativeToManaged((IntPtr)pushStr);
                xzyWeChat.Wx_ReleaseEX(ref pushStr);

                ////返回null
                //XzyWxApis.WXSendLoginVerifyCode(WXID, "+8618768177519", "123456", (int)pushStr1);
                //datas = xzyWeChat.MarshalNativeToManaged((IntPtr)pushStr);
                //xzyWeChat.Wx_ReleaseEX(ref pushStr);

                ////成功
                //XzyWxApis.WXGetContact(WXID, "wxid_z3sckhpgn43v21", (int)pushStr1);
                //datas = xzyWeChat.MarshalNativeToManaged((IntPtr)pushStr);
                //xzyWeChat.Wx_ReleaseEX(ref pushStr);

                ////成功
                //XzyWxApis.WXSnsUserPage(WXID, "wxid_z3sckhpgn43v21","", (int)pushStr1);
                //datas = xzyWeChat.MarshalNativeToManaged((IntPtr)pushStr);
                //xzyWeChat.Wx_ReleaseEX(ref pushStr);

                ////成功
                //XzyWxApis.WXSnsObjectDetail(WXID, "12838091718978637970", (int)pushStr1);
                //datas = xzyWeChat.MarshalNativeToManaged((IntPtr)pushStr);
                //xzyWeChat.Wx_ReleaseEX(ref pushStr);

                ////成功返回{"message":"","status":0}
                //XzyWxApis.WXSnsObjectOp(WXID, "12838091718978637970",5,192,2,(int)pushStr1);
                //datas = xzyWeChat.MarshalNativeToManaged((IntPtr)pushStr);
                //xzyWeChat.Wx_ReleaseEX(ref pushStr);

                ////成功
                //XzyWxApis.WXSearchContact(WXID, "18768177519",(int)pushStr1);
                //datas = xzyWeChat.MarshalNativeToManaged((IntPtr)pushStr);
                //xzyWeChat.Wx_ReleaseEX(ref pushStr);

                ////成功
                //XzyWxApis.WXAddUser(WXID, "v1_92a7db75f738a5fda0be97a2a116a76646248f17fb898f17994a12352c9b6f818cd50b7f3cd29962a55ddd28bd5e3f5e@stranger","",1,"hello", (int)pushStr1);
                //datas = xzyWeChat.MarshalNativeToManaged((IntPtr)pushStr);
                //xzyWeChat.Wx_ReleaseEX(ref pushStr);

                ////成功
                //XzyWxApis.WXDeleteUser(WXID, "wxid_qpa6140vqikj21@stranger", (int)pushStr1);
                //datas = xzyWeChat.MarshalNativeToManaged((IntPtr)pushStr);
                //xzyWeChat.Wx_ReleaseEX(ref pushStr);

                ////有返回
                //XzyWxApis.WXAcceptUser(WXID, "v1_caa184cca67809743add39844a9a7a51cd4df982570340e8ae5f3cad28347ad4@stranger", "v2_d1c8b3b609c0e3863c2ea11ac5adbd27f5a25f960ab6da885f35589e1b4cc4f1928817d6f130e5ca87e6edd4e41d5d09cb73e76bd96dc610767053879cba15b1@stranger", (int)pushStr1);
                //datas = xzyWeChat.MarshalNativeToManaged((IntPtr)pushStr);
                //xzyWeChat.Wx_ReleaseEX(ref pushStr);

                ////成功
                //XzyWxApis.WXGetLoginToken(WXID, (int)pushStr1);
                //datas = xzyWeChat.MarshalNativeToManaged((IntPtr)pushStr);
                //LoginToken token = JsonConvert.DeserializeObject<LoginToken>(datas.ToString());
                //xzyWeChat.Wx_ReleaseEX(ref pushStr);

                ////返回null
                //XzyWxApis.WXLoginRequest(WXID, token.token, (int)pushStr1);
                //datas = xzyWeChat.MarshalNativeToManaged((IntPtr)pushStr);
                //xzyWeChat.Wx_ReleaseEX(ref pushStr);

                ////登录错误，请重新登录
                //XzyWxApis.WXAutoLogin(WXID, token.token, (int)pushStr1);
                //datas = xzyWeChat.MarshalNativeToManaged((IntPtr)pushStr);
                //xzyWeChat.Wx_ReleaseEX(ref pushStr);

                ////{"message":"cannot parse wx.dat","status":-1}
                //byte[] data62 = System.Text.Encoding.Default.GetBytes("YnBsaXN0MDDUAQIDBAUGCQpYJHZlcnNpb25YJG9iamVjdHNZJGFyY2hpdmVyVCR0b3ASAAGGoKIHCFUkbnVsbF8QIDEzYjM3NDAyMDJjMDk0ZjVhOTYzNTlkYmQ5Njg5NTBkXxAPTlNLZXllZEFyY2hpdmVy0QsMVHJvb3SAAQgRGiMtMjc6QGN1eH0AAAAAAAABAQAAAAAAAAANAAAAAAAAAAAAAAAAAAAAfw==");
                //XzyWxApis.WXLoadWxDat(WXID, data62, data62.Length, (int)pushStr1);
                //datas = xzyWeChat.MarshalNativeToManaged((IntPtr)pushStr);
                //xzyWeChat.Wx_ReleaseEX(ref pushStr);

                ////成功
                //XzyWxApis.WXGetRequestToken(WXID, "gh_8f0ca9863b81", "https://mp.weixin.qq.com/s?__biz=MzA5MDAwOTExMw==&mid=200126214&idx=1&sn=a1e7410ec56de5b6c4810dd7f7db8a47&chksm=1e0b3470297cbd666198666278421aed0a131d775561c08f52db0c82ce0e6a9546aac072a20e&mpshare=1&scene=1&srcid=0408bN3ACxqAH6jyq4vCBP9e&ascene=7&devicetype=iPad+iPhone+OS9.0.2&version=16060125&nettype=WIFI&lang=zh_CN&fontScale=100&pass_ticket=hmO2uJbtlPo52na5wD1Dp%2FSgR0%2FXjHoCEiOL%2BCBrvng%3D&wx_header=1", (int)pushStr1);
                //datas = xzyWeChat.MarshalNativeToManaged((IntPtr)pushStr);
                //xzyWeChat.Wx_ReleaseEX(ref pushStr);

                ////成功
                //XzyWxApis.WXGetSubscriptionInfo(WXID, "gh_8f0ca9863b81", (int)pushStr1);
                //datas = xzyWeChat.MarshalNativeToManaged((IntPtr)pushStr);
                //xzyWeChat.Wx_ReleaseEX(ref pushStr);

                ////成功
                //byte[] image = GetFileByte("D:\\2.png");
                //XzyWxApis.WXSetHeadImage(WXID, image, image.Length, (int)pushStr1);
                //datas = xzyWeChat.MarshalNativeToManaged((IntPtr)pushStr);
                //xzyWeChat.Wx_ReleaseEX(ref pushStr);

                ////成功
                //XzyWxApis.WXSendMsg(WXID, "wxid_1dl6sa012v1f22", "hello", null, (int)pushStr1);
                //datas = xzyWeChat.MarshalNativeToManaged((IntPtr)pushStr);
                //xzyWeChat.Wx_ReleaseEX(ref pushStr);

                ////成功
                //XzyWxApis.WXSetProxyInfo(WXID, "192.168.0.0", 1, "","", (int)pushStr1);
                //datas = xzyWeChat.MarshalNativeToManaged((IntPtr)pushStr);
                //xzyWeChat.Wx_ReleaseEX(ref pushStr);

                ////程序会退出
                //XzyWxApis.WXGetPeopleNearby(WXID, 118.538361, 24.911349, (int)pushStr1);
                //datas = xzyWeChat.MarshalNativeToManaged((IntPtr)pushStr);
                //xzyWeChat.Wx_ReleaseEX(ref pushStr);

                //返回null
                //XzyWxApis.WXReceiveRedPacket(WXID, "{\"content\":\"<msg><appmsg appid=\"\" sdkver=\"\"><des><![CDATA[我给你发了一个红包，赶紧去拆!]]></des><url><![CDATA[https://wxapp.tenpay.com/mmpayhb/wxhb_personalreceive?showwxpaytitle=1&msgtype=1&channelid=1&sendid=1000039501201809067010753012257&ver=6&sign=9eaf410e700315be5f16e6588dc0d5507b45eb169b91ff05acbe03ba0c7f2766fa4bc40390576b7725abc1154bcbe48faa7e77c6038fa3bf852267ba2cddd3b8f45c8943d883f36bb2dd5d850c7cb819]]></url><type><![CDATA[2001]]></type><title><![CDATA[微信红包]]></title><thumburl><![CDATA[https://wx.gtimg.com/hongbao/1800/hb.png]]></thumburl><wcpayinfo><templateid><![CDATA[7a2a165d31da7fce6dd77e05c300028a]]></templateid><url><![CDATA[https://wxapp.tenpay.com/mmpayhb/wxhb_personalreceive?showwxpaytitle=1&msgtype=1&channelid=1&sendid=1000039501201809067010753012257&ver=6&sign=9eaf410e700315be5f16e6588dc0d5507b45eb169b91ff05acbe03ba0c7f2766fa4bc40390576b7725abc1154bcbe48faa7e77c6038fa3bf852267ba2cddd3b8f45c8943d883f36bb2dd5d850c7cb819]]></url><iconurl><![CDATA[https://wx.gtimg.com/hongbao/1800/hb.png]]></iconurl><receivertitle><![CDATA[恭喜发财，大吉大利]]></receivertitle><sendertitle><![CDATA[恭喜发财，大吉大利]]></sendertitle><scenetext><![CDATA[微信红包]]></scenetext><senderdes><![CDATA[查看红包]]></senderdes><receiverdes><![CDATA[领取红包]]></receiverdes><nativeurl><![CDATA[wxpay://c2cbizmessagehandler/hongbao/receivehongbao?msgtype=1&channelid=1&sendid=1000039501201809067010753012257&sendusername=PARFUMvae&ver=6&sign=9eaf410e700315be5f16e6588dc0d5507b45eb169b91ff05acbe03ba0c7f2766fa4bc40390576b7725abc1154bcbe48faa7e77c6038fa3bf852267ba2cddd3b8f45c8943d883f36bb2dd5d850c7cb819]]></nativeurl><sceneid><![CDATA[1002]]></sceneid><innertype><![CDATA[0]]></innertype><paymsgid><![CDATA[1000039501201809067010753012257]]></paymsgid><scenetext>微信红包</scenetext><locallogoicon><![CDATA[c2c_hongbao_icon_cn]]></locallogoicon><invalidtime><![CDATA[1536299981]]></invalidtime></wcpayinfo></appmsg><fromusername><![CDATA[PARFUMvae]]></fromusername></msg>\",\"continue\":1,\"description\":\"栾蓓蕾 : [红包]恭喜发财，大吉大利\",\"from_user\":\"PARFUMvae\",\"msg_id\":\"1025036533062768926\",\"msg_source\":\"<msgsource><pushkey /></msgsource>\",\"msg_type\":5,\"status\":1,\"sub_type\":49,\"timestamp\":1536213581,\"to_user\":\"wxid_z3sckhpgn43v21\",\"uin\":2708420761}", (int)pushStr1);
                //datas = xzyWeChat.MarshalNativeToManaged((IntPtr)pushStr);
                //xzyWeChat.Wx_ReleaseEX(ref pushStr);

                //返回null
                //XzyWxApis.WXQueryRedPacket(WXID, "{\"content\":\"<msg><appmsg appid=\"\" sdkver=\"\"><des><![CDATA[我给你发了一个红包，赶紧去拆!]]></des><url><![CDATA[https://wxapp.tenpay.com/mmpayhb/wxhb_personalreceive?showwxpaytitle=1&msgtype=1&channelid=1&sendid=1000039501201809067010753012257&ver=6&sign=9eaf410e700315be5f16e6588dc0d5507b45eb169b91ff05acbe03ba0c7f2766fa4bc40390576b7725abc1154bcbe48faa7e77c6038fa3bf852267ba2cddd3b8f45c8943d883f36bb2dd5d850c7cb819]]></url><type><![CDATA[2001]]></type><title><![CDATA[微信红包]]></title><thumburl><![CDATA[https://wx.gtimg.com/hongbao/1800/hb.png]]></thumburl><wcpayinfo><templateid><![CDATA[7a2a165d31da7fce6dd77e05c300028a]]></templateid><url><![CDATA[https://wxapp.tenpay.com/mmpayhb/wxhb_personalreceive?showwxpaytitle=1&msgtype=1&channelid=1&sendid=1000039501201809067010753012257&ver=6&sign=9eaf410e700315be5f16e6588dc0d5507b45eb169b91ff05acbe03ba0c7f2766fa4bc40390576b7725abc1154bcbe48faa7e77c6038fa3bf852267ba2cddd3b8f45c8943d883f36bb2dd5d850c7cb819]]></url><iconurl><![CDATA[https://wx.gtimg.com/hongbao/1800/hb.png]]></iconurl><receivertitle><![CDATA[恭喜发财，大吉大利]]></receivertitle><sendertitle><![CDATA[恭喜发财，大吉大利]]></sendertitle><scenetext><![CDATA[微信红包]]></scenetext><senderdes><![CDATA[查看红包]]></senderdes><receiverdes><![CDATA[领取红包]]></receiverdes><nativeurl><![CDATA[wxpay://c2cbizmessagehandler/hongbao/receivehongbao?msgtype=1&channelid=1&sendid=1000039501201809067010753012257&sendusername=PARFUMvae&ver=6&sign=9eaf410e700315be5f16e6588dc0d5507b45eb169b91ff05acbe03ba0c7f2766fa4bc40390576b7725abc1154bcbe48faa7e77c6038fa3bf852267ba2cddd3b8f45c8943d883f36bb2dd5d850c7cb819]]></nativeurl><sceneid><![CDATA[1002]]></sceneid><innertype><![CDATA[0]]></innertype><paymsgid><![CDATA[1000039501201809067010753012257]]></paymsgid><scenetext>微信红包</scenetext><locallogoicon><![CDATA[c2c_hongbao_icon_cn]]></locallogoicon><invalidtime><![CDATA[1536299981]]></invalidtime></wcpayinfo></appmsg><fromusername><![CDATA[PARFUMvae]]></fromusername></msg>\",\"continue\":1,\"description\":\"栾蓓蕾 : [红包]恭喜发财，大吉大利\",\"from_user\":\"PARFUMvae\",\"msg_id\":\"1025036533062768926\",\"msg_source\":\"<msgsource><pushkey /></msgsource>\",\"msg_type\":5,\"status\":1,\"sub_type\":49,\"timestamp\":1536213581,\"to_user\":\"wxid_z3sckhpgn43v21\",\"uin\":2708420761}", 1, (int)pushStr1);
                //datas = xzyWeChat.MarshalNativeToManaged((IntPtr)pushStr);
                //xzyWeChat.Wx_ReleaseEX(ref pushStr);

                ////成功
                //byte[] image2 = GetFileByte("D:\\2.png");
                //XzyWxApis.WXSendImage(WXID, "wxid_1dl6sa012v1f22", image2, image2.Length, (int)pushStr1);
                //datas = xzyWeChat.MarshalNativeToManaged((IntPtr)pushStr);
                //xzyWeChat.Wx_ReleaseEX(ref pushStr);

                ////{"message":"","status":0}
                //XzyWxApis.WXSayHello(WXID, "v1_caa184cca67809743add39844a9a7a51cd4df982570340e8ae5f3cad28347ad4@stranger", "", (int)pushStr1);
                //datas = xzyWeChat.MarshalNativeToManaged((IntPtr)pushStr);
                //xzyWeChat.Wx_ReleaseEX(ref pushStr);

                ////退出程序
                //XzyWxApis.WXGetMsgVoice(WXID, "{\"content\":\"<msg><voicemsg endflag=\"1\" cancelflag=\"0\" forwardflag=\"0\" voiceformat=\"4\" voicelength=\"3380\" length=\"6067\" bufid=\"432885235978469974\" clientmsgid=\"492806098176d67c3f695ce032d29258wxid_z3sckhpgn43v214517_1536213488\" fromusername=\"PARFUMvae\" /></msg>\",\"continue\":1,\"data\":\"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\",\"description\":\"栾蓓蕾 : [语音]\",\"from_user\":\"PARFUMvae\",\"msg_id\":\"5674497833798536071\",\"msg_source\":\"<msgsource />\",\"msg_type\":5,\"status\":1,\"sub_type\":34,\"timestamp\":1536213491,\"to_user\":\"wxid_z3sckhpgn43v21\",\"uin\":2708420761}", (int)pushStr1);
                //XzyWeChatThread xzyWeChat = new XzyWeChatThread();
                //var datas = xzyWeChat.MarshalNativeToManaged((IntPtr)pushStr);
                //xzyWeChat.Wx_ReleaseEX(ref pushStr);

                //XzyWxApis.WXTransferOperation(WXID, "wxid_1dl6sa012v1f22", (int)pushStr1);
                //datas = xzyWeChat.MarshalNativeToManaged((IntPtr)pushStr);
                //xzyWeChat.Wx_ReleaseEX(ref pushStr);

                ////成功
                //XzyWxApis.WXSendMoments(WXID, "测试", (int)pushStr1);
                //datas = xzyWeChat.MarshalNativeToManaged((IntPtr)pushStr);
                //xzyWeChat.Wx_ReleaseEX(ref pushStr);

                ////程序退出
                //XzyWxApis.WXMassMessage(WXID, "[\"wxid_1dl6sa012v1f22\"]","", (int)pushStr1);
                //datas = xzyWeChat.MarshalNativeToManaged((IntPtr)pushStr);
                //xzyWeChat.Wx_ReleaseEX(ref pushStr);

                ////成功{"data":"YnBsaXN0MDDUAQIDBAUGCQpYJHZlcnNpb25YJG9iamVjdHNZJGFyY2hpdmVyVCR0b3ASAAGGoKIHCFUkbnVsbF8QIDEzYjM3NDAyMDJjMDk0ZjVhOTYzNTlkYmQ5Njg5NTBkXxAPTlNLZXllZEFyY2hpdmVy0QsMVHJvb3SAAQgRGiMtMjc6QGN1eH0AAAAAAAABAQAAAAAAAAANAAAAAAAAAAAAAAAAAAAAfw==","message":"","status":0}
                //XzyWxApis.WXGenerateWxDat(WXID, (int)pushStr1);
                //datas = xzyWeChat.MarshalNativeToManaged((IntPtr)pushStr);
                //xzyWeChat.Wx_ReleaseEX(ref pushStr);

                //XzyWxApis.WXOpenRedPacket(WXID, "[{\"content\":\"<msg><appmsg appid=\"\" sdkver=\"\"><des><![CDATA[我给你发了一个红包，赶紧去拆!]]></des><url><![CDATA[https://wxapp.tenpay.com/mmpayhb/wxhb_personalreceive?showwxpaytitle=1&msgtype=1&channelid=1&sendid=1000039501201809067010753012257&ver=6&sign=9eaf410e700315be5f16e6588dc0d5507b45eb169b91ff05acbe03ba0c7f2766fa4bc40390576b7725abc1154bcbe48faa7e77c6038fa3bf852267ba2cddd3b8f45c8943d883f36bb2dd5d850c7cb819]]></url><type><![CDATA[2001]]></type><title><![CDATA[微信红包]]></title><thumburl><![CDATA[https://wx.gtimg.com/hongbao/1800/hb.png]]></thumburl><wcpayinfo><templateid><![CDATA[7a2a165d31da7fce6dd77e05c300028a]]></templateid><url><![CDATA[https://wxapp.tenpay.com/mmpayhb/wxhb_personalreceive?showwxpaytitle=1&msgtype=1&channelid=1&sendid=1000039501201809067010753012257&ver=6&sign=9eaf410e700315be5f16e6588dc0d5507b45eb169b91ff05acbe03ba0c7f2766fa4bc40390576b7725abc1154bcbe48faa7e77c6038fa3bf852267ba2cddd3b8f45c8943d883f36bb2dd5d850c7cb819]]></url><iconurl><![CDATA[https://wx.gtimg.com/hongbao/1800/hb.png]]></iconurl><receivertitle><![CDATA[恭喜发财，大吉大利]]></receivertitle><sendertitle><![CDATA[恭喜发财，大吉大利]]></sendertitle><scenetext><![CDATA[微信红包]]></scenetext><senderdes><![CDATA[查看红包]]></senderdes><receiverdes><![CDATA[领取红包]]></receiverdes><nativeurl><![CDATA[wxpay://c2cbizmessagehandler/hongbao/receivehongbao?msgtype=1&channelid=1&sendid=1000039501201809067010753012257&sendusername=PARFUMvae&ver=6&sign=9eaf410e700315be5f16e6588dc0d5507b45eb169b91ff05acbe03ba0c7f2766fa4bc40390576b7725abc1154bcbe48faa7e77c6038fa3bf852267ba2cddd3b8f45c8943d883f36bb2dd5d850c7cb819]]></nativeurl><sceneid><![CDATA[1002]]></sceneid><innertype><![CDATA[0]]></innertype><paymsgid><![CDATA[1000039501201809067010753012257]]></paymsgid><scenetext>微信红包</scenetext><locallogoicon><![CDATA[c2c_hongbao_icon_cn]]></locallogoicon><invalidtime><![CDATA[1536299981]]></invalidtime></wcpayinfo></appmsg><fromusername><![CDATA[PARFUMvae]]></fromusername></msg>\",\"continue\":1,\"description\":\"栾蓓蕾 : [红包]恭喜发财，大吉大利\",\"from_user\":\"PARFUMvae\",\"msg_id\":\"1025036533062768926\",\"msg_source\":\"<msgsource><pushkey /></msgsource>\",\"msg_type\":5,\"status\":1,\"sub_type\":49,\"timestamp\":1536213581,\"to_user\":\"wxid_z3sckhpgn43v21\",\"uin\":2708420761}]", "", (int)pushStr1);
                //datas = xzyWeChat.MarshalNativeToManaged((IntPtr)pushStr);
                //xzyWeChat.Wx_ReleaseEX(ref pushStr);

                //XzyWxApis.WXFavSync(WXID, "", (int)pushStr1);
                //datas = xzyWeChat.MarshalNativeToManaged((IntPtr)pushStr);
                //xzyWeChat.Wx_ReleaseEX(ref pushStr);

                //XzyWxApis.WXFavAddItem(WXID, "<favitem type=\"1\"><desc>我这辈子最佩服的只有两个人一个是群主，还有一个，就是免死[抱拳][抱拳][抱拳]</desc><ctrlflag>127</ctrlflag><source sourcetype=\"1\" sourceid=\"5247349643135372738\"><fromusr>wxid_j7rwo75glpzw22</fromusr><tousr>7459655793@chatroom</tousr><createtime>1474781339</createtime><msgid>5247349643135372738</msgid></source></favitem>", (int)pushStr1);
                //datas = xzyWeChat.MarshalNativeToManaged((IntPtr)pushStr);
                //xzyWeChat.Wx_ReleaseEX(ref pushStr);

                //XzyWxApis.WXFavGetItem(WXID, 1, (int)pushStr1);
                //datas = xzyWeChat.MarshalNativeToManaged((IntPtr)pushStr);
                //xzyWeChat.Wx_ReleaseEX(ref pushStr);

                //XzyWxApis.WXFavDeleteItem(WXID, 36, (int)pushStr1);
                //datas = xzyWeChat.MarshalNativeToManaged((IntPtr)pushStr);
                //xzyWeChat.Wx_ReleaseEX(ref pushStr);

                //XzyWxApis.WXGetContactLabelList(WXID, (int)pushStr1);
                //datas = xzyWeChat.MarshalNativeToManaged((IntPtr)pushStr);
                //LableResult result=JsonConvert.DeserializeObject<LableResult>(datas.ToString());
                //string str=UnBase64String(result.label[0].name);
                //xzyWeChat.Wx_ReleaseEX(ref pushStr);

                //XzyWxApis.WXAddContactLabel(WXID,"测试", (int)pushStr1);
                //datas = xzyWeChat.MarshalNativeToManaged((IntPtr)pushStr);
                //xzyWeChat.Wx_ReleaseEX(ref pushStr);

                //XzyWxApis.WXDeleteContactLabel(WXID, "1", (int)pushStr1);
                //datas = xzyWeChat.MarshalNativeToManaged((IntPtr)pushStr);
                //xzyWeChat.Wx_ReleaseEX(ref pushStr);

                //XzyWxApis.WXSetContactLabel(WXID, "wxid_z3sckhpgn43v21", "1", (int)pushStr1);
                //datas = xzyWeChat.MarshalNativeToManaged((IntPtr)pushStr);
                //xzyWeChat.Wx_ReleaseEX(ref pushStr);

                //XzyWxApis.WXShareCard(WXID, "wxid_z3sckhpgn43v21", "wxid_z3sckhpgn43v21", "分享", (int)pushStr1);
                //datas = xzyWeChat.MarshalNativeToManaged((IntPtr)pushStr);
                //xzyWeChat.Wx_ReleaseEX(ref pushStr);

                //XzyWxApis.WXGetSubscriptionInfo(WXID, "公众号", (int)pushStr1);
                //datas = xzyWeChat.MarshalNativeToManaged((IntPtr)pushStr);
                //xzyWeChat.Wx_ReleaseEX(ref pushStr);

                //XzyWxApis.WXSubscriptionCommand(WXID, "gh", 1, "1", (int)pushStr1);
                //datas = xzyWeChat.MarshalNativeToManaged((IntPtr)pushStr);
                //xzyWeChat.Wx_ReleaseEX(ref pushStr);

                //XzyWxApis.WXRequestUrl(WXID, "baidu.com", "1", "1", (int)pushStr1);
                //datas = xzyWeChat.MarshalNativeToManaged((IntPtr)pushStr);
                //xzyWeChat.Wx_ReleaseEX(ref pushStr);

                //int i=XzyWxApis.WXSyncReset(WXID);
                //datas = xzyWeChat.MarshalNativeToManaged((IntPtr)pushStr);
                //xzyWeChat.Wx_ReleaseEX(ref pushStr);

                //报错
                //XzyWxApis.WXCreateChatRoom(WXID, "[\"wxid_g58r112lnw0q22\",\"my26259675\"]", (int)pushStr1);
                //datas = xzyWeChat.MarshalNativeToManaged((IntPtr)pushStr);
                //xzyWeChat.Wx_ReleaseEX(ref pushStr);

                //XzyWxApis.WXAddChatRoomMember(WXID, "测试", "wxid_z3sckhpgn43v21", (int)pushStr1);
                //datas = xzyWeChat.MarshalNativeToManaged((IntPtr)pushStr);
                //xzyWeChat.Wx_ReleaseEX(ref pushStr);

                //XzyWxApis.WXInviteChatRoomMember(WXID, "测试", "wxid_z3sckhpgn43v21", (int)pushStr1);
                //datas = xzyWeChat.MarshalNativeToManaged((IntPtr)pushStr);
                //xzyWeChat.Wx_ReleaseEX(ref pushStr);

                //XzyWxApis.WXQRCodeDecode(WXID, "D:\\1536054976.png", (int)pushStr1);
                //datas = xzyWeChat.MarshalNativeToManaged((IntPtr)pushStr);
                //xzyWeChat.Wx_ReleaseEX(ref pushStr);

                ////不清楚这个方法干嘛用的，login_url
                //XzyWxApis.WXExtDeviceLoginGet(WXID, "", (int)pushStr1);
                //datas = xzyWeChat.MarshalNativeToManaged((IntPtr)pushStr);
                //xzyWeChat.Wx_ReleaseEX(ref pushStr);

                //XzyWxApis.WXSendVoice(WXID, "wxid_z3sckhpgn43v21", GetFileByte("E:\\Project\\myself\\SS_CUSTOM\\Custom\\Client\\CustomClient\\bin\\Debug\\cache\\1acc0639-f805-49ab-a257-7042755500e0.mp3"), 10,10, (int)pushStr1);
                //datas = xzyWeChat.MarshalNativeToManaged((IntPtr)pushStr);
                //xzyWeChat.Wx_ReleaseEX(ref pushStr);

                //XzyWxApis.WXExtDeviceLoginOK(WXID, "", (int)pushStr1);
                //datas = xzyWeChat.MarshalNativeToManaged((IntPtr)pushStr);
                //xzyWeChat.Wx_ReleaseEX(ref pushStr);

                //XzyWxApis.WXSetUserInfo(WXID, "江旭", "江旭",1,"中国","浙江","杭州", (int)pushStr1);
                //datas = xzyWeChat.MarshalNativeToManaged((IntPtr)pushStr);
                //xzyWeChat.Wx_ReleaseEX(ref pushStr);

                //XzyWxApis.WXLogout(WXID, (int)pushStr1);
                //datas = xzyWeChat.MarshalNativeToManaged((IntPtr)pushStr);
                //xzyWeChat.Wx_ReleaseEX(ref pushStr);

                //XzyWxApis.WXSetUserInfo(WXID, "王力宏", "china is one of", 1, "CN", "Zhejiang", "Hangzhou", (int)pushStr1);
                //datas = xzyWeChat.MarshalNativeToManaged((IntPtr)pushStr);
                //xzyWeChat.Wx_ReleaseEX(ref pushStr);

                //XzyWxApis.WXGetChatRoomMember(WXID, "4830356129@chatroom", (int)pushStr1);
                //datas = xzyWeChat.MarshalNativeToManaged((IntPtr)pushStr);
                //xzyWeChat.Wx_ReleaseEX(ref pushStr);

                //XzyWxApis.WXCheckQRCode(WXID, (int)pushStr1);
                //datas = xzyWeChat.MarshalNativeToManaged((IntPtr)pushStr);
                //xzyWeChat.Wx_ReleaseEX(ref pushStr);

                //XzyWxApis.WXQRCodeLogin(WXID, "a17600036518", "62706C6973743030D4010203040506090A582476657273696F6E58246F626A65637473592461726368697665725424746F7012000186A0A2070855246E756C6C5F102065353631656666326463363038356137383831363339653030323732336262335F100F4E534B657965644172636869766572D10B0C54726F6F74800108111A232D32373A406375787D0000000000000101000000000000000D0000000000000000000000000000007F", (int)pushStr1);
                //datas = xzyWeChat.MarshalNativeToManaged((IntPtr)pushStr);
                //xzyWeChat.Wx_ReleaseEX(ref pushStr);

                //XzyWxApis.WXSyncMsg(WXID, (int)pushStr1);
                //datas = xzyWeChat.MarshalNativeToManaged((IntPtr)pushStr);
                //xzyWeChat.Wx_ReleaseEX(ref pushStr);

                //XzyWxApis.WXSyncContact(WXID, (int)pushStr1);
                //datas = xzyWeChat.MarshalNativeToManaged((IntPtr)pushStr);
                //xzyWeChat.Wx_ReleaseEX(ref pushStr);

                //XzyWxApis.WXSyncMessage(WXID, (int)pushStr1);
                //datas = xzyWeChat.MarshalNativeToManaged((IntPtr)pushStr);
                //xzyWeChat.Wx_ReleaseEX(ref pushStr);
            }

            
        }

        private byte[] GetFileByte(string path)
        {
            using (FileStream fs = new FileStream(path, FileMode.OpenOrCreate, FileAccess.Read, FileShare.ReadWrite))
            {
                byte[] array = new byte[fs.Length];//初始化字节数组
                fs.Read(array, 0, array.Length);//读取流中数据到字节数组中
                return array;
            }
        }

        private string UnBase64String(string value)
        {
            if (value == null || value == "")
            {
                return "";
            }
            byte[] bytes = Convert.FromBase64String(value);
            return Encoding.UTF8.GetString(bytes);
        }
    }

    public class LableResult
    {
        public List<Label> label { get; set; }
    }

    public class LoginToken
    {
        public string token { get; set; }
    }

    public class Label
    {
        public int id { get; set; }

        public string name { get; set; }
    }
}

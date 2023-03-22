/****************************************************
	作者：Dein_
    邮箱: 15542236@qq.com
    日期：2022/12/30 23:55:11
	功能：登录窗口
*****************************************************/

using HOKProtocol;
using UnityEngine;
using UnityEngine.UI;

public class LoginWnd : WindowRoot
{
    public InputField iptAcct;
    public InputField iptPass;
    public Toggle TogSrv;

    protected override void InitWnd()
    {
        base.InitWnd();

        System.Random rd = new System.Random();
        iptAcct.text = rd.Next(100, 999).ToString();
        iptPass.text = rd.Next(100, 999).ToString();
    }

    //点击登录按钮的时候可以调用这个函数
    public void ClickLoginBtn()
    {
        audioSvc.PlayUIAudio("loginBtnClick");
        if (iptAcct.text.Length>=3||iptPass.text.Length >= 3)
        {
            //TODO 发送网络消息，请求登录服务器
            HOKMsg msg = new HOKMsg {
                cmd = CMD.ReqLogin,
                reqLogin = new ReqLogin {
                    acct = iptAcct.text,
                    pass=iptPass.text
                }
            };

            netSvc.SendMsg(msg, (bool result) =>
            {
                if (result == false)
                {
                    netSvc.InitSvc();
                }
            });
        }
        else
        {
            //POP Tips
            //“账号或密码不符合规范”
            root.ShowTips("账号或密码为空");
        }
    }
    public void ClickGMBattleBtn() { 
        SetWndState(false);
        GMSystem.Instance.StartSimulate();
    }
}
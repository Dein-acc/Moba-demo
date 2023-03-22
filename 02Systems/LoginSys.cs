/****************************************************
	作者：Dein_
    邮箱: 15542236@qq.com
    日期：2022/12/30 23:17:34
	功能：登录系统
*****************************************************/

using HOKProtocol;
using UnityEngine;

public class LoginSys : SysRoot
{
    public static LoginSys Instance;
    public LoginWnd loginWnd;
    public StartWnd startWnd;

    public override void InitSys()
    {
        base.InitSys();
        Instance = this;
        this.Log("Init LoginSys Done");
    }

    public void EnterLogin()
    {
        loginWnd.SetWndState();
        audioSvc.PlayBGMusic(NameDefine.MainCityBGMusin);
    }

    public void RspLogin(HOKMsg msg)
    {
        //此步已经登录成功
        root.ShowTips("登录成功");
        root.UserData = msg.rspLogin.userData;//用户的相关数据已经保存到了GameRoot里面

        startWnd.SetWndState();
        loginWnd.SetWndState(false);
    }

    public void EnterLobby()
    {
        startWnd.SetWndState(false);
        //TODO
        LobbySys.Instance.EnterLobby();
    }

    
}
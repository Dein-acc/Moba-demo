/****************************************************
	作者：Dein_
    邮箱: 15542236@qq.com
    日期：2023/1/6 8:1:7
	功能：开始窗口
*****************************************************/

using HOKProtocol;
using UnityEngine;
using UnityEngine.UI;

public class StartWnd : WindowRoot
{
    public Text txtName;

    private UserData ud = null;

    protected override void InitWnd()
    {
        base.InitWnd();
        ud = root.UserData;
        txtName.text = ud.name;
    }

    public void ClickStartBtn()
    {
        audioSvc.PlayUIAudio("com_click1");
        LoginSys.Instance.EnterLobby();
    }
    public void ClickAreaBtn()
    {
        root.ShowTips("正在开发中...");
    }
    public void ClickExitBtn()
    {
        root.ShowTips("正在开发中...");
    }
}
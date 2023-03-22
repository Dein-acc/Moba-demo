/****************************************************
	文件：ClientSession.cs
	作者：Dein_
	邮箱: 15542236@qq.com
	日期：2022/12/30 18:20   	
	功能：客户端网络连接会话
*****************************************************/
using System;
using HOKProtocol;
using PENet;

public class ClientSession : KCPSession<HOKMsg>
{

    

    protected override void OnConnected()
    {
        GameRoot.Instance.ShowTips("连接服务器成功");
    }

    protected override void OnDisConnected()
    {
        GameRoot.Instance.ShowTips("断开服务器连接");
    }

    protected override void OnReciveMsg(HOKMsg msg)
    {
        this.ColorLog(PEUtils.LogColor.Green, "RcvCMD:" + msg.cmd.ToString());
        NetSvc.Instance.AddMsgQue(msg);
    }

    protected override void OnUpdate(DateTime now)
    {

    }
}


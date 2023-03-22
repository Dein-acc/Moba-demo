/****************************************************
	作者：Dein_
    邮箱: 15542236@qq.com
    日期：2022/12/30 23:17:46
	功能：大厅系统
*****************************************************/

using UnityEngine;
using HOKProtocol;
public class LobbySys : SysRoot
{
    public static LobbySys Instance;
    public LobbyWnd lobbyWnd;
    public MatchWnd matchWnd;
    public SelectWnd selectWnd;

    public override void InitSys()
    {
        base.InitSys();
        Instance = this;
        this.Log("Init LobbySys Done");
    }

    public void EnterLobby()
    {
        lobbyWnd.SetWndState();
    }
    public void RspMatch(HOKMsg msg)
    {
        int predictTime = msg.rspMatch.predictTime;
        lobbyWnd.ShowMatchInfo(true, predictTime);
    }

    //通知确认的数据
    public void NtfConfirm(HOKMsg msg)
    {
        NtfConfirm ntf = msg.ntfConfirm;
        if (ntf.dissmiss)
        {
            matchWnd.SetWndState(false);
            lobbyWnd.SetWndState();
        }
        else
        {
            root.RoomID = ntf.roomID;
            lobbyWnd.SetWndState(false);
            if (matchWnd.gameObject.activeSelf==false)
            {
                matchWnd.SetWndState();
            }
            matchWnd.RefreshUI(ntf.confirmArr);
        }
    }

    public void NtfSelect(HOKMsg msg)
    {
        matchWnd.SetWndState(false);
        selectWnd.SetWndState();
    }

    public void NtfLoadRes(HOKMsg msg)
    {   
        root.MapID=msg.ntfLoadRes.mapID;
        root.HeroLst = msg.ntfLoadRes.heroList;
        root.SelfIndex = msg.ntfLoadRes.posIndex;
        selectWnd.SetWndState(false);
        BattleSys.Instance.EnterBattle();
    }
 }
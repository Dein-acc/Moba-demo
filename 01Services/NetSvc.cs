/****************************************************
	文件：NetSvc.cs
	作者：Dein_
	邮箱: 15542236@qq.com
	日期：2022/12/30 18:20   	
	功能：网络服务
*****************************************************/
using PENet;
using PEUtils;
using HOKProtocol;
using UnityEngine;
using System.Threading.Tasks;
using System.Collections.Generic;
using System;

public class NetSvc : MonoBehaviour
{
    //写单例
    public static NetSvc Instance;
    public static readonly string pkgque_lock = "pkgque_lock";
    private KCPNet<ClientSession, HOKMsg> client = null;
    private Queue<HOKMsg> msgPackQue = null;
    private Task<bool> checkTask = null;


    public void InitSvc()
    {
        Instance = this;
        client = new KCPNet<ClientSession, HOKMsg>();
        msgPackQue = new Queue<HOKMsg>();

        //日志接口对接过来
        KCPTool.LogFunc = this.Log;
        KCPTool.WarnFunc = this.Warn;
        KCPTool.ErrorFunc = this.Error;

        KCPTool.ColorLogFunc = (color, msg) =>
        {
            this.ColorLog((LogColor)color, msg);
        };

        string srvIP = ServerConfig.LocalDevInnerIP;
        //点击公网后则切换ip
        LoginSys login = LoginSys.Instance;
        if (login!=null)
        {
            if (login.loginWnd.TogSrv.isOn)
            {
                //TODO
            }
        }

        //这里可闯入ip以及端口
        client.StartAsClient(srvIP, ServerConfig.UdpPort);
        //检测间隔
        checkTask= client.ConnectServer(100);

        this.Log("Init NetSvc Done");
    }

    public void AddMsgQue(HOKMsg msg)
    {
        //锁起来,定义锁
        lock (pkgque_lock)
        {
            msgPackQue.Enqueue(msg);
        }
    }


    private int counter = 0;//计数看它连接了几次
    public void Update()
    {
        if (checkTask!=null&&checkTask.IsCompleted)
        {
            if (checkTask.Result)
            {
                //GameRoot.Instance.ShowTips("连接服务器成功");
                
                this.ColorLog(PEUtils.LogColor.Green,"ConnectServer Success .");//这部分日志特别重要
                checkTask = null;
                //todo send ping msg
            }
            else
            {
                ++counter;
                if (counter>4)
                {
                    this.Error(string.Format("Connect Failed {0} Times,Check Your NetWork Connection", counter));
                    GameRoot.Instance.ShowTips("无法连接服务器，请检查网络状况");
                    checkTask = null;
                }
                else
                {
                    //进行重连，第一连接失败
                    this.Warn(string.Format("Connect Failed {0} Times,Retry...", counter));
                    checkTask = client.ConnectServer(100);
                }

            }
        }

        //判断当前client以及clientSession不为空
        if (client!=null&&client.clientSession!=null)
        {
            //有无数据，有数据才取
            if (msgPackQue.Count > 0)
            {
                lock (pkgque_lock)
                {
                    HOKMsg msg = msgPackQue.Dequeue();
                    HandoutMsg(msg);
                }
            }
            return ;
        }

        if (GMSystem.Instance.isActive)
        {
            if (msgPackQue.Count > 0)
            {
                lock (pkgque_lock)
                {
                    HOKMsg msg = msgPackQue.Dequeue();
                    HandoutMsg(msg);
                }
            }
        }
    }

    /// <summary>
    /// 发送数据cmd到服务器
    /// </summary>
    /// <param name="msg"></param>
    /// <param name="cb">是否发成功，回调</param>
    public void SendMsg(HOKMsg msg,Action<bool> cb = null)
    {
        if (GMSystem.Instance.isActive)
        {
            GMSystem.Instance.SimulateServerRcvMsg(msg);
            cb?.Invoke(true);
            return;
        }

        //clientSession 连接
        if (client.clientSession!=null&&client.clientSession.IsConnected())
        {
            client.clientSession.SendMsg(msg);
            cb?.Invoke(true);
        }
        else
        {
            GameRoot.Instance.ShowTips("服务器未连接");
            this.Error("服务器未连接");
            cb?.Invoke(false);
        }
    }


    //消息分发
    private void HandoutMsg(HOKMsg msg)
    {
        //TODO业务逻辑部分
        if (msg.error!=ErrorCode.None)
        {
            switch (msg.error)
            {
                case ErrorCode.AcctIsOnline:
                    GameRoot.Instance.ShowTips("当前账号已经上线");
                    break;
                default:
                    break;
            }
            return;
        }
        switch(msg.cmd){
            case CMD.RspLogin:
                LoginSys.Instance.RspLogin(msg);
                break;
            case CMD.RspMatch:
                LobbySys.Instance.RspMatch(msg);
                break;
            case CMD.NtfConfirm:
                LobbySys.Instance.NtfConfirm(msg);
                break;
            case  CMD.NtfSelect:
                LobbySys.Instance.NtfSelect(msg);
                break;
            case CMD.NtfLoadRes:
                LobbySys.Instance.NtfLoadRes(msg);
                break;
            case CMD.NtfLoadPrg:
                BattleSys.Instance.NtfLoadPrg(msg);
                break;
            case CMD.RspBatlleStart:
                BattleSys.Instance.RspBattleStart(msg);
                break;
            case CMD.NtfOpKey:
                BattleSys.Instance.NtfOpKey(msg);
                break;
            default:
                break;
        }
    }       
}

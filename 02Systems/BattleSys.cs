/****************************************************
	作者：Dein_
    邮箱: 15542236@qq.com
    日期：2022/12/30 23:17:57
	功能：战斗系统
*****************************************************/


using HOKProtocol;
using PEMath;
using PEPhysx;
using System.Collections.Generic;
using UnityEngine;

public class BattleSys : SysRoot
{
    public static BattleSys Instance;
    public LoadWnd loadWnd;
    public PlayWnd playWnd;

    public bool isTickFight;
    private int mapID;

    private List<BattleHeroData> heroLst = null;
    private GameObject fightGO;
    private FightMgr fightMgr;
    private AudioSource battleAudio;
    uint keyID = 0;
    public uint KeyID
    {
        get
        {
            return ++keyID;
        }
    }


    public override void InitSys()
    {
        base.InitSys();

        Instance = this;
        
        this.Log("Init BattleSys Done.");
    }

    public void EnterBattle()
    {
        audioSvc.StopBGMusic();
        loadWnd.SetWndState();

        mapID = root.MapID;
        heroLst = root.HeroLst;
        resSvc.AsyncLoadScene("map_" + mapID, SceneLoadProgress, SceneLoadDone);
    }

    int lastPercent = 0;
    void SceneLoadProgress(float val)
    {
        int percent = (int)(val * 100);
        if (lastPercent != percent)
        {
            HOKMsg msg = new HOKMsg
            {
                cmd = CMD.SndLoadPrg,
                sndLoadPrg = new SndLoadPrg
                {
                    roomID = root.RoomID,
                    percent = percent
                }
            };
            netSvc.SendMsg(msg);
            lastPercent = percent;
        }
    }

    void SceneLoadDone()
    {
        //TODO
        //初始化UI
        playWnd.SetWndState();//PlayWnd UIPpefab的打开

        //加载角色及资源


        //初始化战斗
        fightGO = new GameObject
        {
            name = "fight"
        };
        fightMgr=fightGO.AddComponent<FightMgr>();
        battleAudio = fightGO.AddComponent<AudioSource>();
        MapCfg mapCfg = resSvc.GetMapConfigByID(mapID);
        fightMgr.Init(heroLst, mapCfg);

        HOKMsg msg = new HOKMsg
        {
            cmd = CMD.ReqBattleStart,
            reqBattleStart = new ReqBattleStart
            {
                roomID = root.RoomID
            }
        };
        netSvc.SendMsg(msg);
        
    }

    public void NtfLoadPrg(HOKMsg msg)
    {
        loadWnd.RefreshPrgData(msg.ntfLoadPrg.percentLst);
    }

    public void RspBattleStart(HOKMsg msg)
    {
        loadWnd.SetWndState(false);

        audioSvc.PlayBGMusic(NameDefine.BattleBGMusin);

        isTickFight = true;
    }

    public void NtfOpKey(HOKMsg msg)
    {
        if (isTickFight)
        {
            fightMgr.InputKey(msg.ntfOpKey.keyList);
            fightMgr.Tick();
        }

    }

    public List<PEColliderBase> GetEnvColliders()
    {
        return fightMgr.GetEnvColliders();
    }

    #region API Func

    /// <summary>
    /// 发送移动帧操作到服务器
    /// </summary>
    /// <param name="logicDir"></param>
    /// <returns></returns>
    public bool SendMoveKey(PEVector3 logicDir)
    {
        HOKMsg msg = new HOKMsg
        {
            cmd = CMD.SndOpKey,
            sndOpKey = new SndOpKey
            {
                roomID = root.RoomID,
                opKey = new OpKey
                {
                    opIndex = root.SelfIndex,
                    keyType = KeyType.Move,
                    moveKey = new MoveKey()
                }
            }
        };
        msg.sndOpKey.opKey.moveKey.x = logicDir.x.ScaledValue;
        msg.sndOpKey.opKey.moveKey.z=logicDir.z.ScaledValue;
        msg.sndOpKey.opKey.moveKey.keyID = KeyID;
        NetSvc.Instance.SendMsg(msg);

        return true;
    }


    #endregion
}

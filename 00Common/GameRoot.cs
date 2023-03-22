/****************************************************
	文件：GameRoot.cs
	作者：Dein_
	邮箱: 15542236@qq.com
	日期：2022/12/30 18:20   	
	功能：
*****************************************************/
using HOKProtocol;
using PEUtils;
using System.Collections.Generic;
using UnityEngine;

public class GameRoot : MonoBehaviour
{
    public static GameRoot Instance;
    public Transform uiRoot;

    public TipsWnd tipsWnd;
    void Start()
    {
        Instance = this;
        LogConfig cfg = new LogConfig()
        {
            enableLog = true,//打开日志插件
            logPrefix="",
            enableTime=true,
            logSeparate=">",
            enableThreadID = true,//打开线程，看是哪个线程
            enableTrace=true,
            enableSave=true,
            enableCover=true,//是否把上次的文件进行覆盖
            saveName="HOKClientPELog.txt",
            loggerType=LoggerType.Unity
        };
        PELog.InitSettings(cfg);
        PELog.ColorLog(LogColor.Green, "InitLogger .");
        DontDestroyOnLoad(this);//GameRoot下场景切换不进行场景切换而是隐藏
        InitRoot();
        PELog.Log("Init Root.");
        Init();
        PELog.Log("Init Done.");
    }


    // Update is called once per frame
    void Update()
    {
        
    }
    private void InitRoot()
    {
        for (int i = 0; i < uiRoot.childCount; i++)
        {
            Transform trans=uiRoot.GetChild(i);
            trans.gameObject.SetActive(false);//将子节点关闭
        }
        tipsWnd.SetWndState();
    }

    private NetSvc netSvc;
    private ResSvc resSvc;
    private AudioSvc audioSvc;
    void Init()
    {
        netSvc = GetComponent<NetSvc>();
        netSvc.InitSvc();
        resSvc = GetComponent<ResSvc>();
        resSvc.InitSvc();
        audioSvc = GetComponent<AudioSvc>();
        audioSvc.InitSvc();
        GMSystem gm=GetComponent<GMSystem>();
        gm.InitSys();

        LoginSys login = GetComponent<LoginSys>();
        login.InitSys();
        BattleSys battle = GetComponent<BattleSys>();
        battle.InitSys();
        LobbySys lobby = GetComponent<LobbySys>();
        lobby.InitSys();

        //login
        PELog.Log("EnterLogin.");
        login.EnterLogin();
    }

    public void ShowTips(string tips)
    {
        tipsWnd.AddTips(tips);
    }

    #region
    UserData userData;
    public UserData UserData
    {
        set { userData = value; }
        get { return userData ; }
    }

    private uint roomID;
    public uint RoomID {
        set { roomID = value; }
        get { return roomID; }
    }

    private int mapID;
    public int MapID
    {
        set { mapID = value; }
        get { return mapID; }
    }
    private List<BattleHeroData> heroLst;
    public List<BattleHeroData> HeroLst
    {
        set { heroLst = value; }
        get { return heroLst; }
    }

    private int selfIndex;
    public int SelfIndex
    {
        set { selfIndex = value; }
        get { return selfIndex; }
    }

    #endregion
}

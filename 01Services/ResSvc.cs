/****************************************************
   文件：ResSvc.cs
   作者：Dein_
   邮箱: 15542236@qq.com
   日期：2022/12/30 23:08   	
   功能：资源服务
*****************************************************/
using PEMath;
using PEPhysx;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResSvc : MonoBehaviour
{
        //写单例
    public static ResSvc Instance;
    public void InitSvc()
   {
       Instance = this;
       this.Log("Init ResSvc Done");
   }

    private Action prgCB = null;
    /// <summary>
    /// 异步场景加载
    /// </summary>
    /// <param name="sceneName">场景名字</param>
    /// <param name="loadRate">加载进度</param>
    /// <param name="loaded">加载完成通知服务器</param>
    public void AsyncLoadScene(string sceneName,Action<float> loadRate,Action loaded)
    {
        AsyncOperation sceneAsync=SceneManager.LoadSceneAsync(sceneName);
        prgCB = () =>
        {
            float progress = sceneAsync.progress;//获取到加载进度
            loadRate?.Invoke(progress);//加载过程中每帧回调，以反应当前进度至loadRate
            if (progress == 1) { 
                loaded?.Invoke();
                prgCB = null;
                sceneAsync = null;
            }
        };
    }

    private void Update() {
        prgCB?.Invoke();    
    }


    //缓存音频文件到dic里面
    private Dictionary<string, AudioClip> adDic = new Dictionary<string, AudioClip>();
    //根据名字以及路径加载音频文件的函数,cache是否将文件缓存起来
    public AudioClip LoadAudio(string path,bool cache = false)
    {
        AudioClip au = null;
        //先判断字典里面有没有
        if (!adDic.TryGetValue(path,out au))
        {
            au = Resources.Load<AudioClip>(path);//直接加载
            if (cache)
            {
                adDic.Add(path, au);
            }
        }
        return au;
    }


    private Dictionary<string, Sprite> spDic = new Dictionary<string, Sprite>();
    public Sprite LoadSprite(string path,bool cache = false)
    {
        Sprite sp = null;
        //先判断字典里面有没有
        if (!spDic.TryGetValue(path, out sp))
        {
            sp = Resources.Load<Sprite>(path);//直接加载
            if (cache)
            {
                spDic.Add(path, sp);
            }
        }
        return sp;
    }

    public UnitCfg GetUnitConfigByID(int unitID)
    {
        switch (unitID)
        {
            case 101:
                return new UnitCfg
                {
                    unityID = 101,
                    unitName = "亚瑟",
                    resName = "arthur",

                    hp = 6500,
                    def = 0,
                    moveSpeed = 5,
                    colliCfg = new ColliderConfig
                    {
                        mType = ColliderType.Cylinder,
                        mRadius = (PEInt)0.5f
                    }
                   
                };
            case 102:
                return new UnitCfg
                {
                    unityID = 102,
                    unitName = "后羿",
                    resName = "houyi",

                     hp = 3500,
                    def = 10,
                    moveSpeed = 5,
                    colliCfg = new ColliderConfig
                    {
                        mType = ColliderType.Cylinder,
                        mRadius = (PEInt)0.5f
                    }
                };
        }
                return null;
    }


    public MapCfg GetMapConfigByID(int mapID) {
        switch (mapID)
        {
            case 101:
                return new MapCfg
                {
                    mapID = 101,

                    bornDelay = 15000,
                    bornInterval = 2000,
                    waveInterval = 50000
                };
            case 102:
                return new MapCfg
                {
                    mapID = 102,

                    bornDelay = 15000,
                    bornInterval = 2000,
                    waveInterval = 50000
                };
            default:
                return null;
        }

    }

}



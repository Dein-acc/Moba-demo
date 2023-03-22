/****************************************************
	文件：AudioSvc.cs
	作者：Dein_
	邮箱: 15542236@qq.com
	日期：2022/12/30 23:07   	
	功能：音频服务
*****************************************************/

using UnityEngine;

public  class AudioSvc : MonoBehaviour
{
    public static AudioSvc Instance;
    public bool TurnOnVoice;//背景音效开关
    public AudioSource bgAudio;
    public AudioSource uiAudio;

    public void InitSvc()
    {
        Instance = this;
        this.Log("Init AudioSvc Done");
    }

    public void StopBGMusic()
    {
        if (bgAudio != null)
        {
            bgAudio.Stop();
        }
    }

    //播放的名字以及是否重复播放
    public void PlayBGMusic(string name,bool isLoop = true)
    {
        if (!TurnOnVoice)
        {
            return;
        }
        AudioClip audio = ResSvc.Instance.LoadAudio("ResAudio/" + name, true);
        //做切换背景音乐时，当前有个音乐在播放则做判断，不为空
        //当前播放音乐的名字不等于指定的名字，则做切换
        if (bgAudio.clip==null|| bgAudio.clip.name!=audio.name)
        {
            bgAudio.clip = audio;
            bgAudio.loop = isLoop;
            bgAudio.Play();
        }
    }

    public void PlayUIAudio(string name)
    {
        if (!TurnOnVoice)
        {
            return;
        }
        AudioClip audio=ResSvc.Instance.LoadAudio("ResAudio/" + name, true);
        uiAudio.clip = audio;
        uiAudio.Play();
    }
}


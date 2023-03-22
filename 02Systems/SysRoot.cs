/****************************************************
	文件：SysRoot.cs
	作者：Dein_
	邮箱: 15542236@qq.com
	日期：2022/12/31 1:04   	
	功能：业务系统
*****************************************************/
using UnityEngine;

public class SysRoot : MonoBehaviour
{
    protected GameRoot root;
    protected NetSvc netSvc;
    protected ResSvc resSvc;
    protected AudioSvc audioSvc;

    public virtual void InitSys()
    {
        root = GameRoot.Instance;
        netSvc = NetSvc.Instance;
        resSvc = ResSvc.Instance;
        audioSvc = AudioSvc.Instance;
    }
}


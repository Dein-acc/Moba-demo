/****************************************************
	作者：Dein_
    邮箱: 15542236@qq.com
    日期：2023/1/8 4:6:40
	功能：匹配确认界面
*****************************************************/

using HOKProtocol;
using UnityEngine;
using UnityEngine.UI;

public class MatchWnd : WindowRoot 
{
    public Text txtTime;//时间
    public Text txtConfirm;//多少人
    public Transform leftPlayerRoot;//左右节点
    public Transform rightPlayerRoot;
    public Button btnConfirm;

    private int timeCount;//倒计时

    //做初始化
    protected override void InitWnd()
    {
        base.InitWnd();

        timeCount = ServerConfig.ConfirmCountDown;
        btnConfirm.interactable = true;//确认的按钮可交互属性
        audioSvc.PlayUIAudio("matchReminder");//音效
    }

    //确认的状态，点击确认框框显示
    public void RefreshUI(ConfirmData[] confirmArr)
    {
        //TODO  先处理左边的一半
        int count = confirmArr.Length / 2;
        for (int i = 0; i < 5; i++)
        {
            Transform player = leftPlayerRoot.GetChild(i);
            //i=1时，则是1v1，将剩下的
            if (i<count)
            {
                SetActive(player);
                string iconPath = "ResImages/MatchWnd/icon_" + confirmArr[i].iconIndex;
                string framePath = "ResImages/MatchWnd/frame_"+(confirmArr[i].confirmDone ? "sure":"normal");
                Image imgIcon = GetImage(player);
                SetSprite(imgIcon, iconPath);
                Image imgFrame = GetImage(player,"img_state");
                SetSprite(imgIcon, framePath);
                imgFrame.SetNativeSize();
            }
            else
            {
                SetActive(player,false);
            }
            
        }

        for (int i = 0; i < 5; i++)
        {
            Transform player = rightPlayerRoot.GetChild(i);
            if (i < count)
            {
                SetActive(player);
                //这边需要跳过左边的数据
                string iconPath = "ResImages/MatchWnd/icon_" + confirmArr[i+count].iconIndex;
                string framePath = "ResImages/MatchWnd/frame_" + (confirmArr[i + count].confirmDone ? "sure" : "normal");
                Image imgIcon = GetImage(player);
                SetSprite(imgIcon, iconPath);
                Image imgFrame = GetImage(player, "img_state");
                SetSprite(imgFrame, framePath);

                imgFrame.SetNativeSize();
            }
            else
            {
                SetActive(player, false);
            }
        }

        int confirmCount = 0;
        for (int i = 0; i < confirmArr.Length; i++)
        {
            if (confirmArr[i].confirmDone)
            {
                ++confirmCount;
            }
        }

        txtConfirm.text = confirmCount + "/" + confirmArr.Length + "就绪";
    }

    public void ClickConfirmBtn()
    {
        audioSvc.PlayUIAudio("matchSureClick");

        HOKMsg msg = new HOKMsg { 
            cmd =CMD.SndConfirm,
            sndConfirm=new SndConfirm
            {
                roomID=root.RoomID
            }
        };

        netSvc.SendMsg(msg);
        btnConfirm.interactable = false;
    }


    private float deltaCount;
    private void Update()
    {
        float delta = Time.deltaTime;
        deltaCount += delta;
        if (deltaCount>=1)
        {
            deltaCount -= 1;
            timeCount -= 1;
            if (delta<1)
            {
                timeCount = 0;
            }
        txtTime.text = timeCount.ToString();
        }
    }

}
/****************************************************
	作者：Dein_
    邮箱: 15542236@qq.com
    日期：2022/12/30 23:56:2
	功能：UI窗口基类
*****************************************************/

using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class WindowRoot : MonoBehaviour 
{
    protected GameRoot root;
    protected NetSvc netSvc;
    protected ResSvc resSvc;
    protected AudioSvc audioSvc;

    //虚函数子类可以覆盖，isActive窗口是否激活
    public virtual void SetWndState(bool isActive = true)
    {
        //判断当前activeSelf属性是不是不为isActive
        if (gameObject.activeSelf!=isActive)
        {
            gameObject.SetActive(isActive);
        }
        //窗口打开之后要对状态进行初始化
        if (isActive)
        {
            InitWnd();
        }
        else
        {
            UnInitWnd();
        }
    }

    protected virtual void InitWnd()
    {
        root = GameRoot.Instance;
        netSvc = NetSvc.Instance;
        resSvc = ResSvc.Instance;
        audioSvc = AudioSvc.Instance;
    }

    protected virtual void UnInitWnd()
    {
        root = null;
        netSvc = null;
        resSvc = null;
        audioSvc = null;
    }

    protected void SetActive(GameObject go,bool state = true)
    {
        go.SetActive(state);
    }
    protected void SetActive(Transform trans, bool state = true)
    {
        trans.gameObject.SetActive(state);
    }
    protected void SetActive(RectTransform rectTrans, bool state = true)
    {
        rectTrans.gameObject.SetActive(state);
    }
    protected void SetActive(Image img, bool state = true)
    {
        img.gameObject.SetActive(state);
    }
    protected void SetActive(Text text, bool state = true)
    {
        text.gameObject.SetActive(state);
    }
    protected void SetActive(InputField ipt, bool state = true)
    {
        ipt.gameObject.SetActive(state);
    }

    protected void SetText(Transform trans, int num = 0)
    {
        SetText(trans.GetComponent<Text>(), num.ToString());
    }
    protected void SetText(Transform trans, string context = "")
    {
        SetText(trans.GetComponent<Text>(), context);
    }
    protected void SetText(Text txt, int num = 0)
    {
        SetText(txt, num.ToString());
    }

    protected void SetText(Text txt,string context="")
    {
        txt.text = context;
    }

    protected void SetSprite(Image image, string path)
    {
        Sprite sp = ResSvc.Instance.LoadSprite(path, true);
        image.sprite = sp;
    }

    protected Transform GetTrans(Transform trans, string name)
    {
        if (trans != null)
        {
            return trans.Find(name);
        }
        else
        {
            return transform.Find(name);
        }
    }
    protected Image GetImage(Transform trans,string path)
    {
        if (trans!=null)
        {
            return trans.Find(path).GetComponent<Image>();
        }
        else
        {
            return transform.Find(path).GetComponent<Image>();
        }
    }
    protected Image GetImage(Transform trans)
    {
        if (trans != null)
        {
            return trans.GetComponent<Image>();
        }
        else
        {
            return transform.GetComponent<Image>();
        }
    }
    protected Text GetText(Transform trans, string path)
    {
        if (trans != null)
        {
            return trans.Find(path).GetComponent<Text>();
        }
        else
        {
            return transform.Find(path).GetComponent<Text>();
        }
    }

    private T GetOrAddComponent<T>(GameObject go) where T : Component
    {
        T t = go.GetComponent<T>();
        if (t == null)
        {
            t = go.AddComponent<T>();
        }
        return t;
    }

    protected void OnClick(GameObject go, Action<PointerEventData, object[]> clickCB, params object[] args)
    {
        PEListener listener = GetOrAddComponent<PEListener>(go);
        listener.onClick = clickCB;
        if (args != null)
        {
            listener.args = args;
        }
    }
    protected void OnClickDown(GameObject go, Action<PointerEventData, object[]> clickDownCB, params object[] args)
    {
        PEListener listener = GetOrAddComponent<PEListener>(go);
        listener.onClickDown = clickDownCB;
        if (args != null)
        {
            listener.args = args;
        }
    }
    protected void OnClickUp(GameObject go, Action<PointerEventData, object[]> clickUpCB, params object[] args)
    {
        PEListener listener = GetOrAddComponent<PEListener>(go);
        listener.onClickUp = clickUpCB;
        if (args != null)
        {
            listener.args = args;
        }
    }
    protected void OnDrag(GameObject go, Action<PointerEventData, object[]> dragCB, params object[] args)
    {
        PEListener listener = GetOrAddComponent<PEListener>(go);
        listener.onDrag = dragCB;
        if (args != null)
        {
            listener.args = args;
        }
    }

}
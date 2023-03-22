/****************************************************
    文件：LogicUnit.cs
	作者：Dein_
    邮箱: 15542236@qq.com
    日期：2023/3/15 5:13:28
	功能：基础逻辑单位
*****************************************************/

using PEMath;

public abstract class LogicUnit : ILogic
{
    /// <summary>
    /// 逻辑单位角色名称
    /// </summary>
    public string unitName;

    #region Key Properties
    //逻辑位置
    PEVector3 logicPos;
    public PEVector3 LogicPos
    {
        set
        {
            logicPos = value;
        }
        get
        {
            return logicPos;
        }
    }
    //逻辑方向
    PEVector3 logicDir;
    public PEVector3 LogicDir
    {
        set
        {
            logicDir = value;
        }
        get
        {
            return logicDir;
        }
    }
    //逻辑速度
    PEInt logicMoveSpeed;
    public PEInt LogicMoveSpeed
    {
        set
        {
            logicMoveSpeed = value;
        }
        get
        {
            return logicMoveSpeed;
        }
    }
    /// <summary>
    /// 基础速度
    /// </summary>
    public PEInt moveSpeedBase;
    #endregion

    public abstract void LogicInit();
    public abstract void LogicTick();
    public abstract void LogicUnInit();
}

interface ILogic
{
    void LogicInit();
    void LogicTick();
    void LogicUnInit();
}
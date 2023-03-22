/****************************************************
    文件：MainLogicUnit.cs
	作者：Dein_
    邮箱: 15542236@qq.com
    日期：2023/3/15 5:22:57
	功能：Nothing
*****************************************************/

using HOKProtocol;
using PEMath;

public enum UnitStateEnum
{
    Alive,//存活
    Dead
}

public enum UnitTypeEnum
{
    Hero,
    Soldier,//远程兵
    Tower,
}

public enum TeamEnum
{
    None,
    Blue,
    Red,
    Neutal//中立，暂时用不上
}

public abstract partial class MainLogicUnit : LogicUnit
{
    public LogicUnitData ud;
    public UnitStateEnum unitState;
    public UnitTypeEnum unitType;

    public MainLogicUnit(LogicUnitData ud)
    {
        this.ud = ud;
        unitName=ud.unitCfg.unitName;
       
    }

    public override void LogicInit()
    {
        //初始化属性
        InitProperties();
        //初始化技能
        InitSkill();
        //初始化移动
        InitMove();

        unitState = UnitStateEnum.Alive;
    }

    public override void LogicTick()
    {
        TickSkill();
        TickMove();
    }

    public override void LogicUnInit()
    {
        UnInitSkill();
        UnInitMove();
    }

    public void InputKey(OpKey key)
    {
        switch (key.keyType) {
            case KeyType.Skill:
                //TODO
                break;
            case KeyType.Move:
                PEInt x = PEInt.zero;
                x.ScaledValue = key.moveKey.x;
                PEInt z=PEInt.zero;
                z.ScaledValue = key.moveKey.z;
                InputMoveKey(new PEVector3(x, 0, z));
                break;
            case KeyType.None:
            default:
                this.Error("KEY is not exist");
                break;
        }
    }

}
 
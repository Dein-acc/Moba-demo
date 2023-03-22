/****************************************************
	文件：MainLogicAttrs.cs
	作者：Dein_
	邮箱: 15542236@qq.com
	日期：2023/03/15 5:34   	
	功能：主要逻辑单位属性状态处理
*****************************************************/
using PEMath;

public partial class MainLogicUnit
{
    #region 属性状态数据
    private PEInt hp;
    public PEInt Hp
    {
        private set
        {
            hp = value;
        }
        get
        {
            return hp;
        }
    }
    private PEInt def;
    public PEInt Def
    {
        private set
        {
            def = value;
        }
        get
        {
            return def;
        }
    }
    #endregion

    void InitProperties()
    {
       Hp=ud.unitCfg.hp;
        Def=ud.unitCfg.def;
    }
}

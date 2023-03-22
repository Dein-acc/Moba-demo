/****************************************************
	文件：Hero.cs
	作者：Dein_
	邮箱: 15542236@qq.com
	日期：2023/03/15 5:35   	
	功能：英雄单位
*****************************************************/
public class Hero : MainLogicUnit
{
    public int heroID;
    public int posIndex;
    public string userName;//玩家名字

    public Hero(HeroData hd) : base(hd)
    {
        heroID = hd.heroID;
        posIndex = hd.posIndex;
        userName = hd.userName;

        unitType = UnitTypeEnum.Hero;
        unitName = ud.unitCfg.unitName + "_" + userName;
    }

}

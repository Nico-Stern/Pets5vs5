using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PreRoundUpgrades : MonoBehaviour
{
    public CoinsBank CB;

    public int DmgCost = 10;
    public int HpCost = 5;
    public BaseSlots[] Pet;

    public FightClass F;

    public PlayerSlots Player;

    public GameObject[] DmgPlusButon;
    public GameObject[] HpPlusButon;
    public Slider[] DmgSlider;
    public Slider[] HpSlider;


    public void StartPreRoundShop(int index)
    {
        DmgSlider[index].maxValue = 10;
        HpSlider[index].maxValue = Pet[index].StartHp;
        HpSlider[index].value = Pet[index].Hp;
        DmgSlider[index].value = Pet[index].AttackDmg - Pet[index].StartAttackDmg;
    }

    public void PlusDmg(int index)
    {
        if (DmgSlider[index].value < 10 && CB.RoundCoins >= DmgCost && Pet[index].Hp>0)
        {
            CB.RoundCoins -=DmgCost;
            Pet[index].AttackDmg++;
            DmgSlider[index].value++;
            Player.AllInfos();
        }
    }
    public void PlusHp(int index)
    {
        if (Pet[index].Hp < Pet[index].StartHp && Pet[index].Hp > 0&&CB.RoundCoins>=HpCost)
        {
            CB.RoundCoins -=HpCost;
            Pet[index].Hp++;
            F.Refresh(index);
            HpSlider[index].value++;
            Player.AllInfos();
        }
    }
}

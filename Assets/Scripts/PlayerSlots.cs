using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSlots : IngameSlots
{
    public FightClass FightClass;
    public BaseSlots[] Pet;
    public StorageSlots[] StorageSlots;

    private void Awake()
    {
        Pet[0].Hp = Pet[0].StartHp;
        Pet[1].Hp = Pet[1].StartHp;
        Pet[2].Hp = Pet[2].StartHp;

        Pet[0].AttackDmg = Pet[0].StartAttackDmg;
        Pet[1].AttackDmg = Pet[1].StartAttackDmg;
        Pet[2].AttackDmg = Pet[2].StartAttackDmg;
    }
    public void Start()
    {
        //Für jedes"Pet" wird seine Infos in text angegeben
        AllInfos();       
    }

    public void Infotext(int i)
    {
        InfoTextDmg[i].text = Pet[i].AttackDmg.ToString();
        InfoTextHp[i].text = Pet[i].Hp.ToString();
    }
    public void AllInfos()
    {
        for (int i = 0; i < 3; i++)
        {
            Infotext(i);
            InfoTextName[i].text = Pet[i].Name;
        }
    }
}

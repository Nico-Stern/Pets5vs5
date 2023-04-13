using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSlots : IngameSlots
{
    public FightClass FightClass;
    public BaseSlots[] Pet;
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

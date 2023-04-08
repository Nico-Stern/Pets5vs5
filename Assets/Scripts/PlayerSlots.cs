using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSlots : IngameSlots
{
    public BaseSlots[] Pet;
    public void Start()
    {
        for (int i = 0; i < 3; i++)
        {
            InfoTextDmg[i].text = Pet[i].AttackDmg.ToString();
            InfoTextHp[i].text = Pet[i].Hp.ToString();
            InfoTextName[i].text = Pet[i].Name;
        }
    }
}

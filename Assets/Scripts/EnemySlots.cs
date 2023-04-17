using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemySlots : IngameSlots
{
    public Pets[] AllPets;
    public string[] EnemyName;
    public int[] EnemyHp;
    public int[] EnemyDmg;
    public Sprite[] EnemySprites;
    public FightClass F;
    public void Start()
    {
        Spawn3();    
    }
    void Spawn(int E)
    {//"Spawn" Generiert ein random Pet auf der angegebenen Stelle dabei entspricht Zahl=+1. Enemy = die angegebene Zahl
        {
                int Eingabe = Random.Range(0, AnzahlPets);
                EnemyName[E] = AllPets[Eingabe].Name;
                EnemyHp[E] = AllPets[Eingabe].Hp;
                EnemyDmg[E] = AllPets[Eingabe].AttackDmg;
                InfoTextName[E].text = EnemyName[E];
                InfoTextDmg[E].text = EnemyDmg[E].ToString();
                InfoTextHp[E].text = EnemyHp[E].ToString();
        }
        
    }
    public void Spawn3()
    {
        //Spawn wird 3x ausgef�hrt
        for (int i = 0; i < 3; i++)
        {
            Spawn(i);
        }
        F.EnemyPet[0].SetActive(true);
        F.EnemyPet[1].SetActive(true);
        F.EnemyPet[2].SetActive(true);
        F.Runde++;
        print(F.Runde + ". Runde");
    }

    private void Update()
    {
        if(Input.GetKeyUp(KeyCode.Escape))
        {
            Spawn3();
        }
    }
}


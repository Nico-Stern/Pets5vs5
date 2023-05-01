using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class BaseSlots : ScriptableObject
{
    public string Name;
    //public string Description;
    public int StartHp;
    public int Hp;
    public int StartAttackDmg;
    public int AttackDmg;
    public int Level = 1;
    public int LevelEp;
    public int LevelMaxEp;
    public Sprite Body;
    public Sprite Details;
    public Sprite Normal;
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class StorageSlots : ScriptableObject
{
    public string Name;
    //public string Description;
    public int NormalHp;
    public int NormalAttackDmg;
    public int PlayerCount;
    public int Level = 1;
    public int LevelEp;
    public int LevelMaxEp;
}

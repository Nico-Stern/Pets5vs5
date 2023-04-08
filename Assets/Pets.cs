using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Pets : ScriptableObject
{
    public string Name;
    //public string Description;
    public int Hp;
    public int AttackDmg;
    public int Level = 1;
    public Sprite Body;
    public Sprite Details;
    public Sprite Normal;    
}
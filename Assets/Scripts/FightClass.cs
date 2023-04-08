using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Must;

public class FightClass : MonoBehaviour
{
    public float ZeitZwischenAtt = 1f;

    public GameObject[] Arrows;

    public bool Number1;
    public bool Number2;
    public bool Number3;
    bool Attack1;
    bool Attack2;
    bool Attack3;
    public GameObject[] EnemyPet;
    public int[] DmgOn;

    public int[] E1Dmg;
    public int[] E2Dmg;
    public int[] E3Dmg;

    public int a;

    public int[] AttackOn;

    public PlayerSlots P;
    public EnemySlots E;

    private void Start()
    {
        Arrows[0].SetActive(false);
        Arrows[1].SetActive(false);
        Arrows[2].SetActive(false);
    }

    public void KIChosing()
    {
        for (int i = 0; i < 3; i++)
        {
            if (E.EnemyHp[i] > 0)
            {
                int Eingabe = Random.Range(0, 3);
                if (Eingabe == 0)
                {
                    AttackOn[0] += E.EnemyDmg[i];
                }
                if (Eingabe == 1)
                {
                    AttackOn[1] += E.EnemyDmg[i];
                }
                if (Eingabe == 2)
                {
                    AttackOn[2] += E.EnemyDmg[i];
                }
            }
            else
            {
                return;
            }
        }
    }

    public void OneChoosing()
    {
        Number1 = true;
        Number2 = false;
        Number3 = false;      
    }

    public void TwoChoosing()
    {
        Number1 = false;
        Number2 = true;
        Number3 = false;      
    }

    public void ThreeChoosing()
    {
        Number1 = false;
        Number2 = false;
        Number3 = true;      
    }
    
    public void PlayerAttack(int i)
    {
        
        int PDmgOn = E1Dmg[i] + E2Dmg[i] + E3Dmg[i];
        if (PDmgOn != 0)
        {
            E.EnemyHp[2] -= E3Dmg[i];//E3Dmg[i]
            E.EnemyHp[1] -= E2Dmg[i];
            E.EnemyHp[0] -= E1Dmg[i];
            E.InfoTextHp[2].text = E.EnemyHp[2].ToString();
            E.InfoTextHp[1].text = E.EnemyHp[1].ToString();
            E.InfoTextHp[0].text = E.EnemyHp[0].ToString();

        }
    }

    public IEnumerator Ready()
    {
        //Player 3 greift an
        if (P.Pet[2].Hp > 0)
        {
        yield return new WaitForSeconds(ZeitZwischenAtt);
        PlayerAttack(2);
        }

        //Player 2 greift an
        if (P.Pet[1].Hp > 0)
        {
            yield return new WaitForSeconds(ZeitZwischenAtt);
            PlayerAttack(1);
        }

        //Player 1 greift an
        if (P.Pet[0].Hp > 0)
        {
            yield return new WaitForSeconds(ZeitZwischenAtt);
            PlayerAttack(0);
        }


        

        Attack1 = false; Attack2 = false; Attack3 = false;

            E1Dmg[0] = 0; E1Dmg[1] = 0; E1Dmg[2]=0;
            E2Dmg[0] = 0; E2Dmg[1] = 0; E2Dmg[2] = 0;
            E3Dmg[0] = 0; E3Dmg[1] =0; E3Dmg[2] = 0;

            Arrows[0].transform.rotation = new Quaternion(0, 0, 0, 0);
            Arrows[0].transform.localPosition = Vector3.zero;

            Arrows[1].transform.rotation = new Quaternion(0, 0, 0, 0);
            Arrows[1].transform.localPosition = Vector3.zero;

            Arrows[2].transform.rotation = new Quaternion(0, 0, 0, 0);
            Arrows[2].transform.localPosition = Vector3.zero;

            Arrows[0].SetActive(false);
            Arrows[1].SetActive(false);
            Arrows[2].SetActive(false);

            AttackOn[0] = 0;
            AttackOn[1] = 0;
            AttackOn[2] = 0;

            yield return new WaitForSeconds(ZeitZwischenAtt);

            if (E.EnemyHp[0] <= 0 && E.EnemyHp[1] <= 0 && E.EnemyHp[2] <= 0)
            {
                E.Spawn3();
            }
        

        //endsachen raus holen!
        /* KIChosing();
         int DmgOn3 = P1Dmg[2] + P2Dmg[2] + P3Dmg[2];
          if (DmgOn3!= 0)//Funktion schreiben
          {
          yield return new WaitForSeconds(ZeitZwischenAtt);
             E.EnemyHp[2] -= P3Dmg[2];
             E.EnemyHp[2] -= P2Dmg[2];
             E.EnemyHp[2] -= P1Dmg[2];
             E.InfoTextHp[2].text = E.EnemyHp[2].ToString();
          yield return new WaitForSeconds(ZeitZwischenAtt);
              if (E.EnemyHp[2]<=0)
              {
                  EnemyPet[2].SetActive(false);
              }
          }

         if (AttackOn[2] != 0)
         {
             yield return new WaitForSeconds(ZeitZwischenAtt);
             P.Pet[2].Hp -= AttackOn[2];
             P.InfoTextHp[2].text = P.Pet[2].Hp.ToString();
             yield return new WaitForSeconds(ZeitZwischenAtt);
         }
          //SpielerLeben
          //Zeit


          if (P1Dmg[1] != 0 && P2Dmg[1] != 0 && P3Dmg[1] != 0)
          {
              yield return new WaitForSeconds(ZeitZwischenAtt);
             E.EnemyHp[1] -= P3Dmg[1];
             E.EnemyHp[1] -= P2Dmg[1];
             E.EnemyHp[1] -= P1Dmg[1];
             E.InfoTextHp[1].text = E.EnemyHp[1].ToString();
              yield return new WaitForSeconds(ZeitZwischenAtt);
              if (E.EnemyHp[1] <= 0)
              {
                  EnemyPet[1].SetActive(false);
              }
          }

         if (AttackOn[1] != 0)
         {
             yield return new WaitForSeconds(ZeitZwischenAtt);
             P.Pet[1].Hp -= AttackOn[1];
             P.InfoTextHp[1].text = P.Pet[1].Hp.ToString();
             yield return new WaitForSeconds(ZeitZwischenAtt);
         }
         //SpielerLeben
         //Zeit
         if (P1Dmg[0] != 0 && P2Dmg[0] != 0 && P3Dmg[0] != 0)
          {
              yield return new WaitForSeconds(ZeitZwischenAtt);
             E.EnemyHp[0] -= P3Dmg[0];
             E.EnemyHp[0] -= P2Dmg[0];
             E.EnemyHp[0] -= P1Dmg[0];
             E.InfoTextHp[0].text = E.EnemyHp[0].ToString();
              yield return new WaitForSeconds(ZeitZwischenAtt);
              if (E.EnemyHp[0] <= 0)
              {
                  EnemyPet[0].SetActive(false);
              }
          }

         if (AttackOn[0] != 0)
         {
             yield return new WaitForSeconds(ZeitZwischenAtt);
             P.Pet[0].Hp -= AttackOn[0];
             P.InfoTextHp[0].text = P.Pet[0].Hp.ToString();
             yield return new WaitForSeconds(ZeitZwischenAtt);
         }
         //SpielerLeben
         //Zeit
         Attack1 = false; Attack2= false; Attack3 = false;
         DmgOn[0] = 0; DmgOn[1]=0; DmgOn[2] = 0;
         Arrows[0].transform.rotation=new Quaternion(0,0,0,0); 
         Arrows[0].transform.localPosition = Vector3.zero;

         Arrows[1].transform.rotation = new Quaternion(0, 0, 0, 0);
         Arrows[1].transform.localPosition = Vector3.zero;

         Arrows[2].transform.rotation = new Quaternion(0, 0, 0, 0);
         Arrows[2].transform.localPosition = Vector3.zero;

         Arrows[0].SetActive(false) ;
         Arrows[1].SetActive(false) ;
         Arrows[2].SetActive(false) ;

         AttackOn[0] = 0;
         AttackOn[1] = 0;
         AttackOn[2] = 0;

         yield return new WaitForSeconds(ZeitZwischenAtt);

         if (E.EnemyHp[0]<=0&& E.EnemyHp[1] <= 0&& E.EnemyHp[2] <= 0)
         {
             E.Spawn3();
         }*/
    }

    public void Att1(int a)
    {
        if (Number1&!Attack1)
        {
            /*if(a==2){E3Dmg[0] += P.Pet[0].AttackDmg;}
             if(a==1){E2Dmg[0] += P.Pet[0].AttackDmg;}
            if(a==0){E1Dmg[0] += P.Pet[0].AttackDmg;}*/
            Number1= false;
            Attack1 = true;
            Arrows[0].SetActive(true);
            if(a==2)
            {
                E3Dmg[0] += P.Pet[0].AttackDmg;
                Arrows[0].transform.Rotate(0, 0, 33);
            }
            if (a== 1)
            {
                E2Dmg[0] += P.Pet[0].AttackDmg;
                Arrows[0].transform.Rotate(0, 0,13);
                Arrows[0].transform.localPosition=new Vector3(0, -125, 0);
            }
            if(a==0)
            {
                E1Dmg[0] += P.Pet[0].AttackDmg;
                Arrows[0].transform.localPosition = new Vector3(0, -236, 0);
            }
        }
        else
        {
            if (Number2&!Attack2)
            {
                Number2 = false;
                Attack2 = true;
                Arrows[1].SetActive(true);
                if (a == 2)
                {
                    E3Dmg[1] += P.Pet[1].AttackDmg;
                    Arrows[1].transform.Rotate(0, 0, 20);
                    Arrows[1].transform.localPosition = new Vector3(0, +125, 0);
                }
                if (a == 1)
                {
                    E2Dmg[1] += P.Pet[1].AttackDmg;
                    Arrows[1].transform.localPosition = new Vector3(0, 0, 0);
                }
                if (a == 0)
                {
                    E1Dmg[1] += P.Pet[1].AttackDmg;
                    Arrows[1].transform.Rotate(0, 0, -13);
                    Arrows[1].transform.localPosition = new Vector3(0, -125, 0);
                }
            }
            else
            {
                if (Number3&!Attack3)
                {
                    Number3 = false;
                    Attack3 = true;
                    Arrows[2].SetActive(true);
                    if (a == 2)
                    {
                        E3Dmg[2] += P.Pet[2].AttackDmg;
                        Arrows[2].transform.Rotate(0, 0, 0);
                        Arrows[2].transform.localPosition = new Vector3(0, +330, 0);
                    }
                    if (a == 1)
                    {
                        E2Dmg[2] += P.Pet[2].AttackDmg;
                        Arrows[2].transform.Rotate(0, 0, -13);
                        Arrows[2].transform.localPosition = new Vector3(0, +225, 0);
                    }
                    if (a == 0)
                    {
                        E1Dmg[2] += P.Pet[2].AttackDmg;
                        Arrows[2].transform.localPosition = new Vector3(0, +75, 0);
                        Arrows[2].transform.Rotate(0, 0, -33);
                    }
                }
                else
                {
                    return;
                }
            }
        }
    }

    public IEnumerator AttackEnemy(int i)
    {
        yield return new WaitForSeconds(ZeitZwischenAtt);
        if (DmgOn[i] != 0)//Funktion schreiben
        {
            E.EnemyHp[i] -= DmgOn[i];
            E.InfoTextHp[i].text = E.EnemyHp[i].ToString();
            yield return new WaitForSeconds(ZeitZwischenAtt);
            if (E.EnemyHp[i] <= 0)
            {
                EnemyPet[i].SetActive(false);
            }
        }
    }

    public void ReadyButton()
    {
        StartCoroutine(Ready());
    }
}
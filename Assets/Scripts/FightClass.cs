using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.UI;

public class FightClass : MonoBehaviour
{
    public float TimeBetweenAction = 1f;

    public GameObject[] Arrow;

    public bool Number1IsPressed;
    public bool Number2IsPressed;
    public bool Number3IsPressed;
    public GameObject[] EnemyPet;
    public GameObject[] PlayerPet;
    public GameObject StartButton;
    public int[] DmgOnPlayerPet;

    public int[] DmgOnE1FromPlayer;
    public int[] DmgOnE2FromPlayer;
    public int[] DmgOnE3FromPlayer;

    public int a;

    public int[] AttackOn;

    public int Runde;

    public PlayerSlots Player;
    public EnemySlots E;
    public List<int> PlayerHpList = new List<int>();
    public List<string> PlayerNamesList = new List<string>();

    public void Awake()
    {//Pfeile die Anzeigen Welchesd Pet den gegner angreift werden inaktiv gesetzt
        Arrow[0].SetActive(false);
        Arrow[1].SetActive(false);
        Arrow[2].SetActive(false);

        Player.Pet[0].Hp = Player.Pet[0].StartHp;
        Player.Pet[1].Hp = Player.Pet[1].StartHp;
        Player.Pet[2].Hp = Player.Pet[2].StartHp;

        PlayerHpList.Add(Player.Pet[0].Hp);
        PlayerHpList.Add(Player.Pet[1].Hp);
        PlayerHpList.Add(Player.Pet[2].Hp);

        PlayerNamesList.Add(Player.Pet[0].Name);//Nr1
        PlayerNamesList.Add(Player.Pet[1].Name);//Nr2
        PlayerNamesList.Add(Player.Pet[2].Name);//Nr3

        Runde = 0;
    }

    public void EnemyIsChoosing(int i)
    {
        int Eingabe = Random.Range(0, PlayerHpList.Count);
        PlayerHpList[Eingabe] -= E.EnemyDmg[i];
        if (PlayerHpList.Count == 3)
        {
            Player.Pet[Eingabe].Hp = PlayerHpList[Eingabe];
        }      
        if (PlayerHpList.Count == 2)
        {
            if (PlayerNamesList[0] == Player.Pet[0].Name)
            {
                //nr1lebt
                Player.Pet[0].Hp = PlayerHpList[0];
                


                if (PlayerNamesList[1] == Player.Pet[1].Name)
                {
                    //nr1&nr2lebt
                    //nr3tot
                    Player.Pet[1].Hp = PlayerHpList[1];
                }
                else
                {
                    //nr1lebt&nr3
                    //nr2tot
                    Player.Pet[2].Hp = PlayerHpList[1];
                }
                
            }
            else
            {
                //nr1tot
                //nr2&nr3Lebt
                Player.Pet[2].Hp = PlayerHpList[1];
                Player.Pet[1].Hp = PlayerHpList[0];
            }
        }
        if (PlayerHpList.Count == 1)
        {
            if(PlayerNamesList[0] == Player.Pet[0].Name)
            {
                Player.Pet[0].Hp = PlayerHpList[0];
            }
            if (PlayerNamesList[0] == Player.Pet[1].Name)
            {
                Player.Pet[1].Hp = PlayerHpList[0];
            }
            if (PlayerNamesList[0] == Player.Pet[2].Name)
            {
                Player.Pet[2].Hp = PlayerHpList[0];
            }
        }
        

        if (PlayerHpList[Eingabe] < 1)
        {
            PlayerHpList.Remove(PlayerHpList[Eingabe]);
            PlayerNamesList.Remove(PlayerNamesList[Eingabe]);
        }
        //if playerpet[i]hp kleiner als 1 dann button[i] deaktivieren
        //if all dead new scene
        Player.AllInfos();

        for (int a = 0; a < 3; a++)
        {
            if (Player.Pet[a].Hp <= 0)
            {
                
                PlayerPet[a].SetActive(false);
            }
        }
    }

    public void OneChoosing()
    {//Wenn der Button des ersten PlayerPets bet�tigt wird setzt sich "Number1" Aktiv. Sp�ter f�r den Angriff wichtig
        Number1IsPressed = true;
        Number2IsPressed = false;
        Number3IsPressed = false;      
    }

    public void TwoChoosing()
    {//Wenn der Button des zweiten PlayerPets bet�tigt wird setzt sich "Number2" Aktiv. Sp�ter f�r den Angriff wichtig
        Number1IsPressed = false;
        Number2IsPressed = true;
        Number3IsPressed = false;      
    }

    public void ThreeChoosing()
    {//Wenn der Button des dritten PlayerPets bet�tigt wird setzt sich "Number3" Aktiv. Sp�ter f�r den Angriff wichtig
        Number1IsPressed = false;
        Number2IsPressed = false;
        Number3IsPressed = true;      
    }
    
    public void PlayerAttack(int i)
    {      
        int PDmgOn = DmgOnE1FromPlayer[i] + DmgOnE2FromPlayer[i] + DmgOnE3FromPlayer[i];
        if (PDmgOn != 0)
        {
            E.EnemyHp[2] -= DmgOnE3FromPlayer[i];//i=Player
            E.EnemyHp[1] -= DmgOnE2FromPlayer[i];//i=Player
            E.EnemyHp[0] -= DmgOnE1FromPlayer[i];//i=Player
            E.InfoTextHp[2].text = E.EnemyHp[2].ToString();
            E.InfoTextHp[1].text = E.EnemyHp[1].ToString();
            E.InfoTextHp[0].text = E.EnemyHp[0].ToString();
        }
    }

    void EneemyHpCheck()
    {
        if (E.EnemyHp[0] <= 0)
        {
            EnemyPet[0].SetActive(false);
        }
        if (E.EnemyHp[1] <= 0)
        {
            EnemyPet[1].SetActive(false);
        }
        if (E.EnemyHp[2] <= 0)
        {
            EnemyPet[2].SetActive(false);
        }
    }

    public IEnumerator Ready()
    {
        
        
       
        //Player 3 greift an
        int Turn = 2;
        if (Player.Pet[Turn].Hp > 0)
        {
            yield return new WaitForSeconds(TimeBetweenAction);
            PlayerAttack(Turn);
            yield return new WaitForSeconds(TimeBetweenAction);
            EneemyHpCheck();
        }

        Arrow[2].SetActive(false);
        //Enemy 3 greift an
        if (E.EnemyHp[Turn] > 0)
        {
            yield return new WaitForSeconds(TimeBetweenAction);
            EnemyIsChoosing(Turn);
        }

        Turn = 1;
        //Player 2 greift an
        if (Player.Pet[Turn].Hp > 0)
        {
            yield return new WaitForSeconds(TimeBetweenAction);
            PlayerAttack(Turn);
            yield return new WaitForSeconds(TimeBetweenAction);
            EneemyHpCheck();
        }

        Arrow[1].SetActive(false);
        //Enemy 2 greift an
        if (E.EnemyHp[Turn] > 0)
        {
            yield return new WaitForSeconds(TimeBetweenAction);
            EnemyIsChoosing(Turn);
        }

        Turn = 0;
        //Player 1 greift an
        if (Player.Pet[Turn].Hp > 0)
        {
            yield return new WaitForSeconds(TimeBetweenAction);
            PlayerAttack(Turn);
            yield return new WaitForSeconds(TimeBetweenAction);
            EneemyHpCheck();
        }

        Arrow[0].SetActive(false);
        //Enemy 1 greift an
        if (E.EnemyHp[Turn] > 0)
        {
            yield return new WaitForSeconds(TimeBetweenAction);
            EnemyIsChoosing(Turn);
        }

        //Attackenschaden wird zur�ckgesetzt
        DmgOnE1FromPlayer[0] = 0;
        DmgOnE1FromPlayer[1] = 0;
        DmgOnE1FromPlayer[2] = 0;
        DmgOnE2FromPlayer[0] = 0;
        DmgOnE2FromPlayer[1] = 0;
        DmgOnE2FromPlayer[2] = 0;
        DmgOnE3FromPlayer[0] = 0;
        DmgOnE3FromPlayer[1] = 0;
        DmgOnE3FromPlayer[2] = 0;

        //Die Pfeile die zeigen wer wenn angreift werden zur�ck gesetzt
        Arrow[0].transform.rotation = new Quaternion(0, 0, 0, 0);
        Arrow[0].transform.localPosition = Vector3.zero;

        Arrow[1].transform.rotation = new Quaternion(0, 0, 0, 0);
        Arrow[1].transform.localPosition = Vector3.zero;

        Arrow[2].transform.rotation = new Quaternion(0, 0, 0, 0);
        Arrow[2].transform.localPosition = Vector3.zero;




        AttackOn[0] = 0;
        AttackOn[1] = 0;
        AttackOn[2] = 0;


        if (E.EnemyHp[0] <= 0 && E.EnemyHp[1] <= 0 && E.EnemyHp[2] <= 0)
        {
            yield return new WaitForSeconds(TimeBetweenAction);
            E.Spawn3();
        }

        EnemyPet[0].GetComponent<Button>().interactable = true;
        EnemyPet[1].GetComponent<Button>().interactable = true;
        EnemyPet[2].GetComponent<Button>().interactable = true;

        PlayerPet[0].GetComponent<Button>().interactable = true;
        PlayerPet[1].GetComponent<Button>().interactable = true;
        PlayerPet[2].GetComponent<Button>().interactable = true;

        StartButton.SetActive(true);

    }

    public void Att1(int a)
    {
        if (Number1IsPressed)
        {
            /*if(a==2){E3Dmg[0] += P.Pet[0].AttackDmg;}
             if(a==1){E2Dmg[0] += P.Pet[0].AttackDmg;}
            if(a==0){E1Dmg[0] += P.Pet[0].AttackDmg;}*/
            Number1IsPressed= false;
            PlayerPet[0].GetComponent<Button>().interactable = false;
            Arrow[0].SetActive(true);
            if(a==2)
            {
                DmgOnE3FromPlayer[0] += Player.Pet[0].AttackDmg;
                Arrow[0].transform.Rotate(0, 0, 33);
            }
            if (a== 1)
            {
                DmgOnE2FromPlayer[0] += Player.Pet[0].AttackDmg;
                Arrow[0].transform.Rotate(0, 0,13);
                Arrow[0].transform.localPosition=new Vector3(0, -125, 0);
            }
            if(a==0)
            {
                DmgOnE1FromPlayer[0] += Player.Pet[0].AttackDmg;
                Arrow[0].transform.localPosition = new Vector3(0, -236, 0);
            }
        }
        else
        {
            if (Number2IsPressed)
            {
                Number2IsPressed = false;
                PlayerPet[1].GetComponent<Button>().interactable = false;
                Arrow[1].SetActive(true);
                if (a == 2)
                {
                    DmgOnE3FromPlayer[1] += Player.Pet[1].AttackDmg;
                    Arrow[1].transform.Rotate(0, 0, 20);
                    Arrow[1].transform.localPosition = new Vector3(0, +125, 0);
                }
                if (a == 1)
                {
                    DmgOnE2FromPlayer[1] += Player.Pet[1].AttackDmg;
                    Arrow[1].transform.localPosition = new Vector3(0, 0, 0);
                }
                if (a == 0)
                {
                    DmgOnE1FromPlayer[1] += Player.Pet[1].AttackDmg;
                    Arrow[1].transform.Rotate(0, 0, -13);
                    Arrow[1].transform.localPosition = new Vector3(0, -125, 0);
                }
            }
            else
            {
                if (Number3IsPressed)
                {
                    Number3IsPressed = false;
                    PlayerPet[2].GetComponent<Button>().interactable = false;
                    Arrow[2].SetActive(true);
                    if (a == 2)
                    {
                        DmgOnE3FromPlayer[2] += Player.Pet[2].AttackDmg;
                        Arrow[2].transform.Rotate(0, 0, 0);
                        Arrow[2].transform.localPosition = new Vector3(0, +330, 0);
                    }
                    if (a == 1)
                    {
                        DmgOnE2FromPlayer[2] += Player.Pet[2].AttackDmg;
                        Arrow[2].transform.Rotate(0, 0, -13);
                        Arrow[2].transform.localPosition = new Vector3(0, +225, 0);
                    }
                    if (a == 0)
                    {
                        DmgOnE1FromPlayer[2] += Player.Pet[2].AttackDmg;
                        Arrow[2].transform.localPosition = new Vector3(0, +75, 0);
                        Arrow[2].transform.Rotate(0, 0, -33);
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
        yield return new WaitForSeconds(TimeBetweenAction);
        if (DmgOnPlayerPet[i] != 0)//Funktion schreiben
        {
            E.EnemyHp[i] -= DmgOnPlayerPet[i];
            E.InfoTextHp[i].text = E.EnemyHp[i].ToString();
            yield return new WaitForSeconds(TimeBetweenAction);
            if (E.EnemyHp[i] <= 0)
            {
                EnemyPet[i].SetActive(false);
            }
        }
    }

    public void ReadyButton()
    {
        StartButton.SetActive(false);
        
        EnemyPet[0].GetComponent<Button>().interactable = false;
        EnemyPet[1].GetComponent<Button>().interactable = false;
        EnemyPet[2].GetComponent<Button>().interactable = false;

        PlayerPet[0].GetComponent<Button>().interactable = false;
        PlayerPet[1].GetComponent<Button>().interactable = false;
        PlayerPet[2].GetComponent<Button>().interactable = false;
        StartCoroutine(Ready());
        //Wenn Angegriffen wid
    }
}
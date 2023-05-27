using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class FightClass : MonoBehaviour
{
    public PreRoundUpgrades PreShopScript;

    public float TimeBetweenAction = 1f;

    public GameObject[] Arrow;
    public GameObject[] EnemyPet;
    public GameObject[] PlayerPet;
    public GameObject PreroundShop;
    public GameObject StartButton;

    public TMP_Text RoundCounterText;
    
    public bool[] IsPressed;
    private int Choosing;

    [SerializeField] private float dmgFeedbackTime = 0.5F;

    [SerializeField] private float forward = 10f;

    public int[] DmgOnPlayerPet;

    public int[] DmgOnE1FromPlayer;
    public int[] DmgOnE2FromPlayer;
    public int[] DmgOnE3FromPlayer;

    public int a;

    public bool[] SearchingGold;

    public int[] AttackOn;

    public int Runde=1;

    public PlayerSlots Player;
    public EnemySlots E;
    public List<int> PlayerHpList = new List<int>();
    public List<string> PlayerNamesList = new List<string>();
    public CoinsBank CB;


    bool one = false;
    bool two = false;
    bool three = false;

    public void Start()
    {
        for (int i = 0; i < 3; i++)
        {
            Arrow[i].SetActive(false);
            PlayerHpList.Add(Player.Pet[i].Hp);
            PlayerNamesList.Add(Player.Pet[i].Name);
            PlayerPet[i].GetComponent<Button>().interactable = false;
            IsPressed[i] = false;
        }
        PreroundShop.SetActive(false);

        Choosing = 0;
        ChoosingRotine();
    }

    public void EnemyIsChoosing(int i)
    {
        if (PlayerHpList.Count > 0)
        {
            int Eingabe = Random.Range(0, PlayerHpList.Count);
            
            PlayerHpList[Eingabe] -= E.EnemyDmg[i];
            

            if (PlayerHpList.Count == 3)
            {
                Player.Pet[Eingabe].Hp = PlayerHpList[Eingabe];
            }
            
            if (PlayerHpList.Count == 2)
            {
                if (PlayerNamesList[0] == Player.Pet[0].Name && PlayerNamesList[1] == Player.Pet[1].Name)
                {
                    //1 und 2 richtig
                    Player.Pet[Eingabe].Hp = PlayerHpList[Eingabe];
                }
                if (PlayerNamesList[0] == Player.Pet[0].Name && PlayerNamesList[1] == Player.Pet[2].Name)
                {
                    Player.Pet[0].Hp = PlayerHpList[0];
                    Player.Pet[2].Hp = PlayerHpList[1];
                }
                if (PlayerNamesList[0] == Player.Pet[1].Name && PlayerNamesList[1] == Player.Pet[2].Name)
                {
                    Player.Pet[1].Hp = PlayerHpList[0];
                    Player.Pet[2].Hp = PlayerHpList[1];
                }
            }
            
            if (PlayerHpList.Count == 1)
            {
                //schleife
                if (PlayerNamesList[0] == Player.Pet[0].Name)
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
            Player.AllInfos();
            if (PlayerHpList[Eingabe] < 1)
            {
                PlayerHpList.Remove(PlayerHpList[Eingabe]);
                PlayerNamesList.Remove(PlayerNamesList[Eingabe]);
            }

            StartCoroutine(PlayerHurt());

            //if playerpet[i]hp kleiner als 1 dann button[i] deaktivieren
            //if all dead new scene


            for (int a = 0; a < 3; a++)
            {
                if (Player.Pet[a].Hp <= 0)
                {
                    SearchingGold[a] = false;
                    //karte nicht ausschalten
                    //PlayerPet[a].SetActive(false);
                    //Setze alles null und namen empthy
                }
            }
        }
    }

    IEnumerator PlayerHurt()
    {
        for(int i=0; i < 3; i++)
        {

            if (Player.Pet[i].Hp != Player.PlayerCrunntHp[i])
            {
                PlayerPet[i].GetComponent<Image>().color = Color.red;
                yield return new WaitForSeconds(dmgFeedbackTime);
                PlayerPet[i].GetComponent<Image>().color = Color.white;

                Player.PlayerCrunntHp[i] = Player.Pet[i].Hp;
            }
        }
    }
    
    IEnumerator EnemyHurt(int Index)
    {
        EnemyPet[Index].GetComponent<Image>().color = Color.red;
        yield return new WaitForSeconds(dmgFeedbackTime);
        EnemyPet[Index].GetComponent<Image>().color = Color.white;
    }

    public void PlayerAttack(int i)
    {
        int PDmgOn = DmgOnE1FromPlayer[i] + DmgOnE2FromPlayer[i] + DmgOnE3FromPlayer[i];
        int currentEnmemy1Hp = E.EnemyHp[0];
        int currentEnmemy2Hp = E.EnemyHp[1];
        int currentEnmemy3Hp = E.EnemyHp[2];

        if (PDmgOn != 0)
        {
            if (E.EnemyHp[2] > 0)
            {
                E.EnemyHp[2] -= DmgOnE3FromPlayer[i];//i=Player
                if(E.EnemyHp[2] <= 0)
                {
                    E.EnemyDeath();
                }
                if(currentEnmemy3Hp!= E.EnemyHp[2])
                {

                    StartCoroutine(EnemyHurt(2));
                }
            }
            if (E.EnemyHp[1] > 0)
            {
                E.EnemyHp[1] -= DmgOnE2FromPlayer[i];//i=Player
                if (E.EnemyHp[1] <= 0)
                {
                    E.EnemyDeath();
                }
                if (currentEnmemy2Hp != E.EnemyHp[1])
                {

                    StartCoroutine(EnemyHurt(1));
                }
            }
            if (E.EnemyHp[0] > 0)
            {
                E.EnemyHp[0] -= DmgOnE1FromPlayer[i];//i=Player
                if (E.EnemyHp[0] <= 0)
                {
                    E.EnemyDeath();
                }
                if (currentEnmemy1Hp != E.EnemyHp[0])
                {

                    StartCoroutine(EnemyHurt(0));
                }
            }
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
        for (int Turn =0; Turn <3; Turn++)
        {
            if(PlayerHpList.Count>0)
            {

                if (Player.Pet[Turn].Hp > 0&&(DmgOnE1FromPlayer[Turn] != 0||DmgOnE2FromPlayer[Turn] != 0||DmgOnE3FromPlayer[Turn] != 0))
                {
                     yield return new WaitForSeconds(TimeBetweenAction);
                     PlayerAttack(Turn);
                     yield return new WaitForSeconds(TimeBetweenAction);
                     EneemyHpCheck();
                }
             
                if (SearchingGold[Turn] == true && Player.Pet[Turn].Hp > 0)
                {
                    yield return new WaitForSeconds(TimeBetweenAction);
                    int Eingabe = Random.Range(0, 3);
                    CB.RoundCoins += Eingabe;
                    print("Player "+ (Turn+1) +" Coins found "+Eingabe );
                    SearchingGold[Turn] = false;
                }
             

                 Arrow[Turn].SetActive(false);
                  //Enemy 3 greift an
                if (E.EnemyHp[Turn] > 0)
                {
                    yield return new WaitForSeconds(TimeBetweenAction);
                     EnemyIsChoosing(Turn);
                     yield return new WaitForSeconds(TimeBetweenAction);
                    for (int i = 0; i < 3; i++)
                    {
                        if(Player.Pet[i].Hp <= 0)
                        {
                            PlayerPet[i].SetActive(false);
                        }
                    }
                }
            }
        }
            PlayerOneDead();
            
            Player.AllInfos();
        yield return new WaitForSeconds(TimeBetweenAction);
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
            //E.Spawn3();

            //Show shop
            //Dmg kann nur 10 höher als normal
            //heal nur das was man hat
            //Wiederbelebung 20. wird aber jede 10. runde teuerer
            PreroundShop.SetActive(true);

            PlayerPet[0].GetComponent<Button>().interactable = true;
            PlayerPet[1].GetComponent<Button>().interactable = true;
            PlayerPet[2].GetComponent<Button>().interactable = true;

            PreShopScript.StartPreRoundShop(0);
            PreShopScript.StartPreRoundShop(1);
            PreShopScript.StartPreRoundShop(2);
        }
        else
        {
            StartNextRound();
        }                
    }

    void PlayerOneDead()
    {
        if (Player.Pet[0].Hp<=0 &&(Player.Pet[1].Hp>0|| Player.Pet[2].Hp > 0))
        {
            
            Player.Pet[0].Name = Player.Pet[1].Name;
            Player.Pet[0].StartHp = Player.Pet[1].StartHp;
            Player.Pet[0].Hp = Player.Pet[1].Hp;
            Player.Pet[0].StartAttackDmg = Player.Pet[1].StartAttackDmg;
            Player.Pet[0].AttackDmg = Player.Pet[1].AttackDmg;
            Player.Pet[0].PlayerCount = Player.Pet[1].PlayerCount;
            Player.PlayerCrunntHp[0] = Player.Pet[0].Hp;  

            Player.Pet[1].Name = Player.Pet[2].Name;
            Player.Pet[1].StartHp = Player.Pet[2].StartHp;
            Player.Pet[1].Hp = Player.Pet[2].Hp;
            Player.Pet[1].StartAttackDmg = Player.Pet[2].StartAttackDmg;
            Player.Pet[1].AttackDmg = Player.Pet[2].AttackDmg;
            Player.Pet[1].PlayerCount = Player.Pet[2].PlayerCount;
            Player.PlayerCrunntHp[1] = Player.Pet[1].Hp;

            if (PlayerPet[0].activeInHierarchy == false)
            {
                PlayerPet[0].SetActive(true);
            }

            
           

            Player.Pet[2].Name = "";
            Player.Pet[2].StartHp = 0;
            Player.Pet[2].Hp = 0;
            Player.Pet[2].StartAttackDmg = 0;
            Player.Pet[2].AttackDmg = 0;
            Player.Pet[2].PlayerCount = 0;

            PlayerPet[2].SetActive(false);



            //Level
            PlayerOneDead();
        }
        else
        {
            PlayerTwoDead();
        }
        if(PlayerHpList.Count==0)
        {
            SceneManager.LoadScene("MainMenu");
        }
    }

    void PlayerTwoDead()
    {
        if (PlayerHpList.Count < 2)
        {
            PlayerPet[1].SetActive(false);
            Player.Pet[1].Name = "";
            Player.Pet[1].StartHp = 0;
            Player.Pet[1].Hp = 0;
            Player.Pet[1].StartAttackDmg = 0;
            Player.Pet[1].AttackDmg = 0;
            Player.Pet[1].PlayerCount = 0;
        }
             
        if (Player.Pet[1].Hp<=0 && (Player.Pet[2].Hp > 0))
        {
            
            Player.Pet[1].Name = Player.Pet[2].Name;
            Player.Pet[1].StartHp = Player.Pet[2].StartHp;
            Player.Pet[1].Hp = Player.Pet[2].Hp;
            Player.Pet[1].StartAttackDmg = Player.Pet[2].StartAttackDmg;
            Player.Pet[1].AttackDmg = Player.Pet[2].AttackDmg;
            Player.Pet[1].PlayerCount = Player.Pet[2].PlayerCount;
            PlayerPet[1].SetActive(true);
            Player.PlayerCrunntHp[1] = Player.Pet[1].Hp;

            Player.Pet[2].Name = "";
            Player.Pet[2].StartHp = 0;
            Player.Pet[2].Hp = 0;
            Player.Pet[2].StartAttackDmg = 0;
            Player.Pet[2].AttackDmg = 0;
            Player.Pet[2].PlayerCount = 0;



            //Level
        }
        PlayerThreeDead();
    }

    void PlayerThreeDead()
    {
        if (Player.Pet[2].Hp <= 0)
        {

            Player.Pet[2].Name = "";
            Player.Pet[2].StartHp = 0;
            Player.Pet[2].Hp = 0;
            Player.Pet[2].StartAttackDmg = 0;
            Player.Pet[2].AttackDmg = 0;
            Player.Pet[2].PlayerCount = 0;
            
            
            PlayerPet[2].SetActive(false);
        }
    }
    

    public void Att1(int a)
    {
        if (IsPressed[2])
        {
            IsPressed[2]= false;
            PlayerPet[0].GetComponent<Button>().interactable = false;
            if (a < 3)
            {
                Arrow[2].SetActive(true);
            }
            
            if (a == 3)
            {
                SearchingGold[2] = true;
            }
            if(a==2)
            {
                DmgOnE3FromPlayer[2] += Player.Pet[2].AttackDmg;
                Arrow[2].transform.localPosition = new Vector3(0, -280, 0);
            }
            if (a== 1)
            {
                DmgOnE2FromPlayer[2] += Player.Pet[2].AttackDmg;
                Arrow[2].transform.Rotate(0, 0,20);
                Arrow[2].transform.localPosition=new Vector3(0, -225, 0);
            }
            if(a==0)
            {
                DmgOnE1FromPlayer[2] += Player.Pet[2].AttackDmg;
                
                Arrow[2].transform.Rotate(0, 0, 30);
            }
        }
        else
        {
            if (IsPressed[1])
            {
                IsPressed[1] = false;
                PlayerPet[1].GetComponent<Button>().interactable = false;
                if (a < 3)
                {
                    Arrow[1].SetActive(true);
                }
                if (a == 3)
                {
                    SearchingGold[1] = true;
                }
                if (a == 2)
                {
                    DmgOnE3FromPlayer[1] += Player.Pet[1].AttackDmg;
                    Arrow[1].transform.Rotate(0, 0, -20);
                    Arrow[1].transform.localPosition = new Vector3(0, -100, 0);
                }
                if (a == 1)
                {
                    DmgOnE2FromPlayer[1] += Player.Pet[1].AttackDmg;
                    Arrow[1].transform.localPosition = new Vector3(0, 0, 0);
                }
                if (a == 0)
                {
                    DmgOnE1FromPlayer[1] += Player.Pet[1].AttackDmg; 

                    Arrow[1].transform.Rotate(0, 0, 20);
                    Arrow[1].transform.localPosition = new Vector3(0, +100, 0);
                }
            }
            else
            {
                if (IsPressed[0])
                {
                    IsPressed[0] = false;
                    PlayerPet[2].GetComponent<Button>().interactable = false;
                    if (a < 3)
                    {
                        Arrow[0].SetActive(true);
                    }
                    if (a == 3)
                    {
                        SearchingGold[0] = true;
                    }
                    if (a == 2)
                    {
                        DmgOnE3FromPlayer[0] += Player.Pet[0].AttackDmg;
                        Arrow[0].transform.localPosition = new Vector3(0, +75, 0);
                        Arrow[0].transform.Rotate(0, 0, -30);
                    }
                    if (a == 1)
                    {
                        DmgOnE2FromPlayer[0] += Player.Pet[0].AttackDmg;
                        Arrow[0].transform.Rotate(0, 0, -20);
                        Arrow[0].transform.localPosition = new Vector3(0, +225, 0);
                    }
                    if (a == 0)
                    {
                        DmgOnE1FromPlayer[0] += Player.Pet[0].AttackDmg;

                        Arrow[0].transform.Rotate(0, 0, 0);
                        Arrow[0].transform.localPosition = new Vector3(0, +280, 0);
                    }
                }
                else
                {
                    return;
                }
            }
        }
        PlayerPet[Choosing].transform.position = PlayerPet[Choosing].transform.position + Vector3.right * -forward;
        IsPressed[Choosing] = false;
        Choosing++;
        ChoosingRotine();
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
        EnemyPet[0].GetComponent<Button>().interactable = false;
        EnemyPet[1].GetComponent<Button>().interactable = false;
        EnemyPet[2].GetComponent<Button>().interactable = false;
        
        StartCoroutine(Ready());
        //Wenn Angegriffen wid
    }
    
    //Neues Kampfsystem 2,1,0
    public void ChoosingRotine()
    {
        if (Choosing > 2)
        {
            ReadyButton();
        }
        else
        {
            if (Player.Pet[Choosing].Hp <= 0)
            {
                Choosing++;
                ChoosingRotine();
            }
            else
            {
                IsPressed[Choosing] = true;
                PlayerPet[Choosing].transform.position = PlayerPet[Choosing].transform.position + Vector3.right * forward;
            }
        }
    }

    public void StartNextRound()
    {
        Choosing = 0;

        EnemyPet[0].GetComponent<Button>().interactable = true;
        EnemyPet[1].GetComponent<Button>().interactable = true;
        EnemyPet[2].GetComponent<Button>().interactable = true;

        ChoosingRotine();
    }

    public void PreRoundReadyButton()
    {
        
        StartNextRound();
        PreroundShop.SetActive(false);
        E.Spawn3();
    }
        public void Refresh(int Index)
        {
            PlayerHpList[Index] = Player.Pet[Index].Hp;
        }
}

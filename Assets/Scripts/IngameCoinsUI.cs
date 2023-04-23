using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IngameCoinsUI : MonoBehaviour
{
    public CoinsBank CB;

    public Text RoundCoinsText;

    public void Update()
    {
        RoundCoinsText.text= CB.RoundCoins.ToString()+" Coins";
    }
}

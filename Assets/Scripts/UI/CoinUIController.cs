using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CoinUIController : MonoBehaviour
{
    public TextMeshProUGUI CoinText;
    private void Update()
    {
        CoinText.SetText(PlayerPrefs.GetInt("Coin").ToString());
    }
}

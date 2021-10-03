using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameFinishCoin : MonoBehaviour
{
    public TextMeshProUGUI EarnedCoinText;
    private int EarnedCoin;
    private int LevelStartCoin;
    public BasePanel Panel;
    
    void StartCoin()
    {
        LevelStartCoin = PlayerPrefs.GetInt("Coin");

    }

    void Earned()
    {
        EarnedCoin =  PlayerPrefs.GetInt("Coin") - LevelStartCoin;
        EarnedCoinText.SetText(EarnedCoin.ToString());
    }

    private void OnEnable()
    {
        LevelManager.Instance.OnLevelStarted.AddListener(StartCoin);        
        Panel.OnPanelActivated.AddListener(Earned);
    }

    private void OnDisable()
    {
        Panel.OnPanelActivated.RemoveListener(Earned);
        LevelManager.Instance.OnLevelStarted.RemoveListener(StartCoin);
    }
}

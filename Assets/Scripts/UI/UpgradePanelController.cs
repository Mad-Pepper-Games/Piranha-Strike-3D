using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class UpgradePanelController : MonoBehaviour
{
    public int SpeedCost = 50;
    public int FishCost = 100;
    public int SpeedCostChangeRate = 50;
    public int FishCostChangeRate = 50;
    public Button UpgradeSpeedButton;
    public Button UpgradeFishButton;
    public TextMeshProUGUI SpeedCostText;
    public TextMeshProUGUI FishCostText;

    private void Start()
    {
            SpeedCostText.SetText(PlayerPrefs.GetInt("SpeedCost", 50 ).ToString());
            FishCostText.SetText(PlayerPrefs.GetInt("FishCost", 100 ).ToString());

    }
    void Update()
    {
        
    }

    private void OnEnable()
    {
        UpgradeSpeedButton.onClick.AddListener(UpgradeSpeed);
        UpgradeFishButton.onClick.AddListener(UpgradeFishNumber);
    }
    public void UpgradeSpeed()
    {
        if (PlayerPrefs.GetInt("Coin") > SpeedCost)
        {
            PlayerPrefs.SetInt("Coin", PlayerPrefs.GetInt("Coin") - SpeedCost);
            UpgradeManager.Instance.SpeedUpgrade ++ ;
            SpeedCost = SpeedCost + SpeedCostChangeRate;
            SpeedCostText.SetText(SpeedCost.ToString());
            
        }
    }
    public void UpgradeFishNumber()
    {
        if (PlayerPrefs.GetInt("Coin") > FishCost)
        {
            PlayerPrefs.SetInt("Coin", PlayerPrefs.GetInt("Coin") - FishCost);
            UpgradeManager.Instance.FishStarterCount ++ ;
            FishCost = FishCost + FishCostChangeRate;
            FishCostText.SetText(FishCost.ToString());

        }
    }

    private void OnDisable()
    {
        PlayerPrefs.SetInt("SpeedCost", SpeedCost);
        PlayerPrefs.SetInt("FishCost", FishCost);
    }
}

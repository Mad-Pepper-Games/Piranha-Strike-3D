using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeManager : Singleton<UpgradeManager>
{
    public float SpeedUpgrade;
    public int FishStarterCount;

    private void Start()
    {
        SpeedUpgrade =  PlayerPrefs.GetFloat("SpeedUpgrade");
        FishStarterCount = PlayerPrefs.GetInt("FishStarterCount");
    }

    private void OnApplicationQuit()
    {
        PlayerPrefs.SetFloat("SpeedUpgrade", SpeedUpgrade);
        PlayerPrefs.SetInt("FishStarterCount", FishStarterCount);
    }
}


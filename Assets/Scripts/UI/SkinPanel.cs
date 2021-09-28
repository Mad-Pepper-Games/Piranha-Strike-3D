using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class SkinPanel : BasePanel
{
    public Button PanelOpenButton;
    public Button ADButton;
    public bool IsOpen;
    private void OnEnable()
    {
        if (!Managers.Instance) return;
        SceneController.Instance.OnSceneLoaded.AddListener(Activate);
        LevelManager.Instance.OnLevelStarted.AddListener(Deactivate);
        PanelOpenButton.onClick.AddListener(Slide);
        ADButton.onClick.AddListener(RewardAd);
    }

    private void OnDisable()
    {
        if (!Managers.Instance) return;
        SceneController.Instance.OnSceneLoaded.RemoveListener(Activate);
        LevelManager.Instance.OnLevelStarted.RemoveListener(Deactivate);
        PanelOpenButton.onClick.RemoveListener(Slide);
        ADButton.onClick.RemoveListener(RewardAd);
    }

    public void Slide()
    {
        if (IsOpen)
        {
            transform.DOLocalMoveY(-20f, 1.5f);
            IsOpen = false;
        }
        else
        {
            transform.DOLocalMoveY(641f, 1.5f);
            IsOpen = true;
        }
    }

    public void RewardAd()
    {
        AdManager.Instance.ShowRewardedAds();
        PlayerPrefs.SetInt("Coin", PlayerPrefs.GetInt("Coin") + 100);
    }
}

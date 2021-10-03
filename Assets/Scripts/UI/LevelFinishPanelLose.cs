using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class LevelFinishPanelLose : BasePanel
{
    public bool IsSuccessPannel;
    public UnityEngine.UI.Button LevelFinishButton;
    private void OnEnable()
    {
        if (!Managers.Instance) return;

        LevelFinishButton.transform.localScale = Vector3.zero;
        SceneController.Instance.OnSceneLoaded.AddListener(Deactivate);
        LevelFinishButton.onClick.AddListener(() => GameManager.Instance.CompleteStage(false));
        OnPanelDeactivated.AddListener(() => LevelFinishButton.transform.localScale = Vector3.zero);
        OnPanelActivated.AddListener(() => LevelFinishButton.transform.DOScale(Vector3.one, 1f));
        GameManager.Instance.OnGameFinishes.AddListener(GameLose);
    }

    private void OnDisable()
    {
        if (!Managers.Instance) return;
        LevelManager.Instance.OnLevelFinished.RemoveListener(Activate);
        SceneController.Instance.OnSceneLoaded.RemoveListener(Deactivate);
        LevelFinishButton.onClick.RemoveListener(() => GameManager.Instance.CompleteStage(true));
        OnPanelDeactivated.RemoveListener(() => LevelFinishButton.transform.localScale = Vector3.zero);
        OnPanelActivated.RemoveListener(() => LevelFinishButton.transform.DOScale(Vector3.one, 1f));
    }

    void GameLose(bool Isim)
    {
        if (Isim==false)
        {
            Activate();
        }
    }
}

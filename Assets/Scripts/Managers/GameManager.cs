using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : Singleton<GameManager>
{
    public GameEvent OnGameFinishes = new GameEvent();

    private void Start()
    {
        Application.targetFrameRate = 59;
    }
    public void CompleteStage(bool state)
    {
        
        if (state)
        {
            LevelManager.Instance.LoadNextLevel();
        }
        else
        {
            LevelManager.Instance.ReloadLevel();
        }
    }
}
public class GameEvent : UnityEvent<bool> { }
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UI_System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;

public interface IResetable
{
    void Reset();
}

[RequireComponent(typeof(GameStateManager))]
public class GameStateManager : SingletonBehaviour<GameStateManager>
{
    public UI_System.UI_System UI_Sys;
    public UI_Screen GameOverScreen;
    public UI_Screen WinScreen;

    public ScoreObject Score;
    public GameObject Game;

    private List<IResetable> _resetableBehaviours = new List<IResetable>();

    public void Start()
    {
        foreach (var root in SceneManager.GetActiveScene().GetRootGameObjects())
        {
            _resetableBehaviours.AddRange(root.GetComponentsInChildren<MonoBehaviour>(true).OfType<IResetable>());
        }
        
        Debug.Log(_resetableBehaviours.Count);
    }

    public void InitGameStart()
    {
        Game.SetActive(true);
        ResetGame();
    }

    public void InitGameOver()
    {
        Game.SetActive(false);
        if (Score.CurrentScore >= Score.WinScore)
        {
            UI_Sys.SwitchScreen(WinScreen);
        }
        else
        {
            UI_Sys.SwitchScreen(GameOverScreen);
        }
    }

    public void ResetGame()
    {
        for (int i = 0; i < _resetableBehaviours.Count; ++i)
        {
            _resetableBehaviours[i].Reset();
        }
    }
}

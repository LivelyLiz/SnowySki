using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(GameStateManager))]
public class GameStateManager : SingletonBehaviour<GameStateManager>
{
    public Animator GameStateMachine;


    public void InitGameOver()
    {
        //Debug.Log("GameOver");
    }

    public void InitGameStart()
    {

    }
}

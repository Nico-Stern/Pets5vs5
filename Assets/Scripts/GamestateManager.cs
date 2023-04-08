using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamestateManager : MonoBehaviour
{
    public static GamestateManager Instance;
    public GameState State;
    public static event Action<GameState> GameStateChanged;

    public void UpdateGameState(GameState newState)
    {
        State=newState;

        switch(newState)
        {
            case GameState.KiChoosing:

                break;
            case GameState.OneChoosing: 

                break;
            case GameState.TwoChoosing:

                break;
            case GameState.ThreeChoosing:

                break;
            case GameState.Ready: 

                break;
            case GameState.Fight: 

                break;
            
        }
        GameStateChanged?.Invoke(newState);
    }

    public enum GameState
    {
        KiChoosing,
        OneChoosing,
        TwoChoosing,
        ThreeChoosing,
        Ready,
        Fight,
    }
}

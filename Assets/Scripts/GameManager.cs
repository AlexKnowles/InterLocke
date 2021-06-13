using System;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public bool IsGameOver { get; private set; }
    public bool IsGameRunning  { 
        get
        {
            return (isRunning && !IsGameOver);
        }
    }

    public GameObject MainMenuUI;

    private Action StartGameMethods;
    private bool isRunning;

    public  void SetIsGameOver()
    {
        IsGameOver = true;
        isRunning = false;
    }

    public void StartNewGame()
    {
        MainMenuUI.SetActive(false);

        StartGameMethods();
        IsGameOver = false;
        isRunning = true;
    }

    private void Awake()
    {
        Instance = this;
        IsGameOver = false;
        isRunning = false;
    }

    internal void RegisterGameStartMethod(Action generateRope)
    {
        StartGameMethods += generateRope;
    }
}

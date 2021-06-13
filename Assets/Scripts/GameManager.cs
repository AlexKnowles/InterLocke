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
    public GameObject GameOverUI;

    private Action StartGameMethods;
    private bool isRunning;

    public void ShowMainMenu()
    {
        MainMenuUI.SetActive(true);
        GameOverUI.SetActive(false);
    }

    public  void SetIsGameOver()
    {
        GameOverUI.SetActive(true);
        IsGameOver = true;
        isRunning = false;
    }

    public void StartNewGame()
    {
        MainMenuUI.SetActive(false);
        GameOverUI.SetActive(false);

        StartGameMethods();
        IsGameOver = false;
        isRunning = true;
    }

    private void Awake()
    {
        MainMenuUI.SetActive(true);
        GameOverUI.SetActive(false);

        Instance = this;
        IsGameOver = false;
        isRunning = false;
    }

    internal void RegisterGameStartMethod(Action generateRope)
    {
        StartGameMethods += generateRope;
    }
}

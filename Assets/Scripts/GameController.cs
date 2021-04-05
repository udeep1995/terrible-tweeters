using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameController : MonoBehaviour
{
    public static GameController instance = null;
    public static UnityEvent gameOverEvent;
    public static UnityEvent restartGameEvent;
    public static UnityEvent reduceChanceEvent;

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    void Start()
    {
        AttachAllEvents();
    }

    private void AttachAllEvents()
    {
        if (gameOverEvent == null) gameOverEvent = new UnityEvent();
        if (restartGameEvent == null) restartGameEvent = new UnityEvent();
        if (reduceChanceEvent == null) reduceChanceEvent = new UnityEvent();
        restartGameEvent.AddListener(RestartGame);
        gameOverEvent.AddListener(GameOver);
        reduceChanceEvent.AddListener(ReduceChance);
    }

    private void ReduceChance()
    {
        --LevelController.instance.chances;
        UIManager.instance.DestroyChance();
    }

    private void RestartGame()
    {
        LevelController.instance.RestartGame();
    }

    private void GameOver()
    {
        UIManager.instance.EnableUI();
    }
}

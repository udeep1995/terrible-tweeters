using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
public class LevelController : MonoBehaviour
{
    private Monster[] _monsters;
    public string nextLevelName;

    public int chances = 3;
    public static LevelController instance = null;
    public bool isWon { get; private set; }

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    private void Start()
    {
        isWon = false;
    }
    private void OnEnable()
    {
        _monsters = FindObjectsOfType<Monster>();
    }

    public void RestartGame()
    {
        if(isWon)
        {
            SceneManager.LoadScene(nextLevelName);
        } else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    void Update()
    {
        if(MonstersAreAllDead())
        {
            GoToNextLevel();
        }
    }

    private void GoToNextLevel()
    {
        if(SceneManager.sceneCountInBuildSettings == SceneManager.GetActiveScene().buildIndex + 1)
        {
            GameController.gameOverEvent.Invoke();
        } else
        {
            SceneManager.LoadScene(nextLevelName);
        }
    }

    private bool MonstersAreAllDead()
    {
        foreach(var monster in _monsters)
        {
            if (monster.gameObject.activeSelf) return false;
        }
        isWon = true;
        return true;
    }
}

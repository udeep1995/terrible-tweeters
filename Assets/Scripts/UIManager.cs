using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;

public class UIManager : MonoBehaviour
{
    public string levelNumber;
    public GameObject level;
    public GameObject restartBtn;
    public GameObject gameOver;
    public GameObject livePrefab;
    public Transform livesTransformGroup;

    private List<GameObject> chances;
    public static UIManager instance = null;

    private const string TRY_AGAIN = "Try Again";
    private const string RESTART_GAME = "Restart Game";

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }
    void Start()
    {
        chances = new List<GameObject>();
        for(var i=0; i<LevelController.instance.chances; i++)
        {
            chances.Add(Instantiate(livePrefab, livesTransformGroup));
        }
        level.GetComponent<TextMeshProUGUI>().text = levelNumber;
        DisableUI();
        level.SetActive(true);
        level.GetComponent<Animator>().SetTrigger("showText");
    }

    public void DestroyChance()
    {
        if (chances.Count > 0)
        {
            Destroy(chances[0]);
            chances.RemoveAt(0);
        }
    }
    public void DisableUI()
    {
        level.SetActive(false);
        restartBtn.SetActive(false);
        gameOver.SetActive(false);
    }
    public void EnableUI()
    {
        restartBtn.SetActive(true);
        gameOver.SetActive(true);
        if(!LevelController.instance.isWon)
        {
            gameOver.GetComponent<TextMeshProUGUI>().text = TRY_AGAIN;
        } else
        {
            gameOver.GetComponent<TextMeshProUGUI>().text = RESTART_GAME;
        }
    }

    public void RestartGame()
    {
        GameController.restartGameEvent.Invoke();
    }
}

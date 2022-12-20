using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    private static bool gameOver;
    public static int numberOfCoins;

    [SerializeField]
    private GameObject gameOverPlane, tapToStartPlane, gameplayPanel;
    [SerializeField]
    private Animator animator;
    [SerializeField]
    private TextMeshProUGUI score;
    

    public static bool isGameStarted;

    private void Start()
    {
        numberOfCoins = 0;
        Time.timeScale = 1;
        tapToStartPlane.SetActive(true);
        gameOver = false;
        isGameStarted = false;
    }

    private void Update()
    {
        if (gameOver)
        {
            Time.timeScale = 0;
            gameplayPanel.SetActive(false);
            gameOverPlane.SetActive(true);
        }

        if (SwipeManager.tap)
            StartGame();

        score.text = "" + numberOfCoins;
    }

    public static void GameOver()
    {
        gameOver = true;
    }

    private void StartGame()
    {
        isGameStarted = true;
        tapToStartPlane.SetActive(false);
        animator.SetBool("Started", true);
    }
}

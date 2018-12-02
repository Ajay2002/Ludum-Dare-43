using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    public GameObject deathScreen;
    public Text status;
    public Text Tries;
    public Text points;
    public GameObject GameOverScreen;
    public Text gameOverScore;
    public Text attemptScore;
    public Text winLoss;

    public bool gameOver = false;

    private void Start()
    {
        FindObjectOfType<GameManager>().ReCount();
    }

    public void DisplaySelectionScreen()
    {
        GameObject.FindObjectOfType<CharacterMovement>().inputLock = true;
        deathScreen.SetActive(true);
    }

    public void HideSelectionScreen()
    {
        GameObject.FindObjectOfType<CharacterMovement>().inputLock = false;
        deathScreen.SetActive(false);
    }

    public void InSufficientFunds()
    {
        status.text = "You don't have enough points for this, kill more enemies...";   
    }

    public void Successful()
    {
        status.text = "SELECT";
    }

    private void LateUpdate()
    {
        points.text = "POINTS : "+ GameManager.points.ToString();

        if (gameOver)
        {
            Tries.enabled = false;
            points.enabled = false;
            deathScreen.SetActive(false);
        }
    }

    

    public void GameOver(bool won, int score, int attempts)
    {
        gameOver = true;
        Tries.enabled = false;
        points.enabled = false;
        deathScreen.SetActive(false);
        GameOverScreen.SetActive(true);

        if (won)
        {
            winLoss.text = "You Won!";
        }
        else
        {
            winLoss.text = "You Lost!";
        }

        gameOverScore.text = score.ToString();
        attemptScore.text ="In " +  attempts.ToString() + "/40 ATTEMPTS";

    }

}

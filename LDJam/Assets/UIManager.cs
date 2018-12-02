using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public int moveToNext = 2;
    public GameObject deathScreen;
    public Text status;
    public Text Tries;
    public Text points;
    public GameObject GameOverScreen;
    public Text gameOverScore;
    public Text attemptScore;
    public Text winLoss;

    public Button respawn;
    public Button nextLevel;

    public bool gameOver = false;

    private void Start()
    {
        FindObjectOfType<GameManager>().ReCount();
        GameManager.called = false;
    }

    public void DisplaySelectionScreen()
    {
        respawn.gameObject.SetActive(false);
        GameObject.FindObjectOfType<CharacterMovement>().inputLock = true;
        deathScreen.SetActive(true);
    }

    public void HideSelectionScreen()
    {
        respawn.gameObject.SetActive(true);
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
            respawn.gameObject.SetActive(false);
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
            nextLevel.gameObject.SetActive(true);
            winLoss.text = "You Won!";
        }
        else
        {
            nextLevel.gameObject.SetActive(false);
            winLoss.text = "You Lost!";
        }

        gameOverScore.text = score.ToString();
        attemptScore.text ="In " +  attempts.ToString() + "/40 ATTEMPTS";

    }

}

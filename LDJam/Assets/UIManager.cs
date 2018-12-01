using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    public GameObject deathScreen;
    public Text status;
    public Text points;

    public void DisplaySelectionScreen()
    {
        deathScreen.SetActive(true);
    }

    public void HideSelectionScreen()
    {
        deathScreen.SetActive(false);
    }

    public void InSufficientFunds()
    {
        status.text = "You don't have enough points, kill more enemies...";   
    }

    public void Successful()
    {
        status.text = "SELECT";
    }

    private void Update()
    {
        points.text = GameManager.points.ToString();
    }

}

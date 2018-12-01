using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    public GameObject deathScreen;

    public void DisplaySelectionScreen()
    {
        deathScreen.SetActive(true);
    }

    public void HideSelectionScreen()
    {
        deathScreen.SetActive(false);
    }

}

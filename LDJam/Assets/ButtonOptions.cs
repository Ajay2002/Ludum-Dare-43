using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonOptions : MonoBehaviour
{
    public CharacterMovement mainPlayer;
    public void GoToScene (int index)
    {
        Application.LoadLevel(index);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void KillMainPlayer()
    {
        mainPlayer.Injure(10000);
    }

}

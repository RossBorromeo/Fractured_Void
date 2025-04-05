using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitGame : MonoBehaviour
{
        void Update()
        {
            //key commands for quitting game 
            if ((Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl)) && Input.GetKeyDown(KeyCode.Escape))
            {
                QuitGame();
            }
        }
     public void QuitGame()
        {
            Application.Quit();
        }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowedView : MonoBehaviour
{
   
    public void ToggleWindowMode()
    {
        Screen.fullScreen = !Screen.fullScreen;
    }

    public void SetWindowedMode(bool isWindowed)
    {
        Screen.fullScreen = !isWindowed;
    }
}

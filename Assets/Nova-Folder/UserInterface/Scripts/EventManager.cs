using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public static Action<string> OnKeyCollected; // Event for key collection

    public static void KeyCollected(string keyID)
    {
        OnKeyCollected?.Invoke(keyID);
    }
}

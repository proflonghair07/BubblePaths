using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ambience : MonoBehaviour
{
    private void Awake()
    {
        // Keep this GameObject when a new scene is loaded
        DontDestroyOnLoad(this.gameObject);
    }
}

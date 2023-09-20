using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GMKiller : MonoBehaviour
{
    private GameObject gm;
    private GameObject ambience;
    // Start is called before the first frame update
    void Start()
    {
        gm = GameObject.FindWithTag("GM");
        ambience = GameObject.FindWithTag("Ambience");
    }

    public void DestroyGM()
    {
        Destroy(gm);
        Destroy(ambience);
    }
    
}

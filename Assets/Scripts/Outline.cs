using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Outline : MonoBehaviour
{
    public GameObject reloadPanel;
    // Start is called before the first frame update

    public void StartSceneLoad()
    {
        reloadPanel.GetComponent<AnimationHandler>().ChangeAnimationState("LoadNewLevel");
    }
  
}

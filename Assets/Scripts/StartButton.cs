using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;

public class StartButton : MonoBehaviour
{
    //rewired
    [SerializeField] public int playerID = 0;
    [SerializeField] public Player player;
    public GameObject reloadPanel;
    // Start is called before the first frame update
    void Start()
    {
        player = ReInput.players.GetPlayer(playerID);
    }

    // Update is called once per frame
    void Update()
    {
        if (player.GetButtonDown("Jump"))
        {
            GetComponent<AnimationHandler>().ChangeAnimationState("Pressed");
            reloadPanel.GetComponent<AnimationHandler>().ChangeAnimationState("LoadNewLevel");
        }
    }
}

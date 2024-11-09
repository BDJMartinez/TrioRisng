using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mindseyeAttack : MonoBehaviour
{


    public GameObject player;
    public PlayerController playerController;

    private void Start()
    {
        player = gamemanager.instance.player.GetComponent<GameObject>();
        playerController = gamemanager.instance.player.GetComponent<PlayerController>();
    }

    public void FreezePlayer()
    {
        playerController.IsFrozen = true; //Turn on freeze state
        stunPlayer();
    }

    public void UnfreezePlayer()
    {
        playerController.IsFrozen = false;  //Turn off freeze state
    }

    public void stunPlayer()
    {
        playerController.Speed = 0;
    }

    public void OnTriggerStay(Collider other)
    {
        

    }

    public void OnTriggerExit(Collider other)
    {


    }


}

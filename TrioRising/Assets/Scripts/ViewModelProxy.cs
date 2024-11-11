using System.Collections;
using System.Collections.Generic;
using UndeadWarfare.Player;
using UnityEngine;

public class ViewModelProxy : MonoBehaviour
{
    private PlayerController player; // Refrence the player holding the View Model

    // Start is called before the first frame update
    private void StartReload()
    {
        if (player.HeldWeapon)
        {

        }
    }

    // Update is called once per frame
    private void EndReload()
    {
        
    }
}

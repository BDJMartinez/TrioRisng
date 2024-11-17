using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class mindseyeAttack : MonoBehaviour
{
    public float AttackCooldown { get => attackCooldown; set => attackCooldown = value; }
    public float AttackCooldownTimer { get => attackCooldownTimer; set => attackCooldownTimer = value; }
    public float StunTime { get => stunTime; set => stunTime = value; }
   // public float StunTimer { get => stunTimer; set => stunTimer = value; }


    public GameObject player;
    public PlayerController playerController;

    float speedOrig;
    private float attackCooldown = 10f;
    private float attackCooldownTimer;
    private float stunTime = 5f;
    //private float stunTimer;

    private int health;

    public spawnManager[] enemySpawns;


    private void Start()
    {
        player = gamemanager.instance.player.GetComponent<GameObject>();
        playerController = gamemanager.instance.player.GetComponent<PlayerController>();
        speedOrig = playerController.Speed;
    }

    private void Update()
    {
        if (AttackCooldownTimer > 0)
        {
            AttackCooldownTimer -= Time.deltaTime;
        }    
    }


    public void FreezePlayer()
    {
        if (attackCooldownTimer <= 0)
        {
            if (playerController != null)
            {
                playerController.IsFrozen = true;
                playerController.Speed = 0;  // Stop movement immediately
                Debug.Log($"Player is frozen. Speed set to 0.");
            }

            StartCoroutine(stunEffect());
        }
    }

    public void UnfreezePlayer()
    {
        if (playerController != null)
        {
            playerController.IsFrozen = false;
            playerController.Speed = speedOrig;  // Restore original speed
            Debug.Log("Player is unfrozen and can move again.");
        }
    }

    //public void stunPlayer()
    //{
    //    if (playerController != null)
    //    {
    //        playerController.Speed = 0;
    //        Debug.Log("Player is stunned...");
    //    }
    //    else
    //        Debug.LogWarning($"playerController is not set: Use inspector to assign.");

    //}

    public void TakeDamage(int damage)
    { 
        health -= damage;

        if (health <= 0)
        {
            Death();
        }
    }


    private void Death()
    {
        StopEnemySpawners();
        Destroy(gameObject);
    }

    private void StopEnemySpawners()
    {
        foreach (var spawner in enemySpawns)
        {
            spawner.StopAllCoroutines();
        }
    }
    

    IEnumerator stunEffect()
    {
        gamemanager.instance.StunnedPlayer = true;
        Debug.Log($"StunnedPlayer is set to {gamemanager.instance.StunnedPlayer}");
        yield return new WaitForSecondsRealtime(StunTime);


       
        UnfreezePlayer();        
        gamemanager.instance.StunnedPlayer = false;
        Debug.Log($"StunnedPlayer is set to {gamemanager.instance.StunnedPlayer}");
    }

    public void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (!playerController.IsFrozen)
            {
                FreezePlayer(); 
            }
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
           UnfreezePlayer();
        }

    }

    


}

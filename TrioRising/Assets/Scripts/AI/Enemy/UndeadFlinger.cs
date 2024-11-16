using System.Collections;
using System.Collections.Generic;
using UndeadWarfare.AI;
using UnityEngine;

public class UndeadFlinger : BaseUndead
{
    #region PUBLIC_PROPERTIES
    public GameObject ProjectilePrefab { get => projectilePrefab; set => projectilePrefab = value;  }
    public Transform ShootPosition { get => shootPosition; set => shootPosition = value; }
    public float AttackRange { get => attackRange; set => attackRange = value; }
    public float AttackCooldown { get => attackCooldown; set => attackCooldown = value; }
    #endregion

    #region BASIC_PROPERTIES
    [Header(" ---- Basic Properties ---- ")]
    [SerializeField] private GameObject projectilePrefab;       // Prefab for the ranged attack projectile 
    [SerializeField] private Transform shootPosition;           // Firing position for ranged attack projectile
    [SerializeField] private float attackRange;                 // Enemies attack range
    [SerializeField] private float attackCooldown;              // Cooldown time for next attack 

    private float attackCooldownTimeer;                         // Timer to track time since last attack
    #endregion

    private void Update()
    {
       // Call base update handle common enemy behavior

       // Handle ranged attack logic when in the attacking state
    }

    // Handles ranged attack logic
    private void HandleRangedAttack()
    {
        // Ensure enough time has passed the last attack

        // Check if the player is within attack range
    }

    // Fires projectile towards player
    private void FireProjectile()
    {
        // Instantiate the projectile and set it's direction toward the player
    }
}

using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace UndeadWarfare.AI
{
    public class EnemyHotspot : BaseUndead, IDamage
    {
        [Header(" ---- Hot Spot Properties ---- ")]

        [Tooltip("Reference to this base enemy health component.")]
        [SerializeField] private IDamage enemyDamageReciever;       // Reference to the parent recieving the damage

        private void Start()
        {
            if (enemyDamageReciever == null)
            {
                Debug.LogWarning("Enemy Hot Spot: No IDamage reciever found. Refer to the inspector for assignment.");
            }
        }
        // Detects collision with a projectile to determine if the hotspot was hit
        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.CompareTag("PlayerProjectile")) { HandleProjectileHit(collision.gameObject); }
        }
        // Handles the logic with the hotspot is hit with a projectile 
        private void HandleProjectileHit(GameObject _projectile)
        {
            
        }
    }
}
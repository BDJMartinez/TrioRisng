using System.Buffers;
using System.Collections;
using System.Collections.Generic;
using UndeadWarfare.AI.State;
using UndeadWarfare.Player;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

namespace UndeadWarfare.AI.Undead
{
    public class UndeadSeeker : BaseUndead
    {
        [Header(" ---- Seeker Properties ---- ")]

        public float DetectionRange = 10f;

        public float AttackRange = 2f;

        public float FleeHealthThreshold = 20f;

        private BaseAIState _currentState;
        // Initialize the Seeker and set it's initial state
        private void Start()
        {
            // Initialize with Idle state
            TransitionToState(new IdleState(this));
        }
        // Update checks the current state and runs its logic
        private void Update()
        {
            _currentState?.Run();
            if (IsPlayerInSightRange())
            {
                EngageTarget();
            }
        }
        // Changes the Seekers current state to a new state
        public void TransitionToState(BaseAIState _newState)
        {
            _currentState = _newState;
            Debug.Log($"Seeker transitioned to {_currentState.Name} state.");
        }
        // Checks if the player is within detection range
        public bool IsPlayerInSightRange()
        {
            if (_currentState != null) return false;
            float distanceToPlayer = Vector3.Distance(transform.position, Target.transform.position);
            return distanceToPlayer <= DetectionRange;
        }
        // Checks if the player is within attack range
        public bool IsPlayerInAttackRange()
        {
            if (_currentState != null) return false;
            float distanceToPlayer = Vector3.Distance(transform.position, Target.transform.position);
            return distanceToPlayer <= AttackRange;
        }

        public void EngageTarget()
        {
            if (IsTargetVisible)
            {
                TransitionToState(new EngageState(this));
                Debug.Log("Seeker has engaged the target!");
            }

        }
        // Triggers attack action against the player
        public void AttackTarget()
        {
            Debug.Log($"Seeker attacks the target");
            TransitionToState(new BaseAttackState(this));
            // TODO: Attack animation logic
        }
        // Inflicts damage on the AI's target when contact is made
        public void InflictDamage() { Target.GetComponent<PlayerController>().TakeDamage(attackDamage, this.gameObject); }         // Inflict damage on the player

        // Triggers fleeing behavior if Seeker health falls below the threshold
        public void FleeTarget()
        {
            Debug.Log("Seeker's healh is extremely low; Seeker Fleeing...");
            TransitionToState(new FleeState(this));
        }
        // Triggers dead state and clean up
        public void Die()
        {
            TransitionToState(new DeadState(this));
            Debug.Log("Seeker has died!");
            Destroy(gameObject);
        }
        // Checks if collision is with the player via tag, if so inflicts damage on the player
        public void OnCollisionEnter(Collision collision)
        {
            if (collision == null) return;
            // If the player is the collider is tagged as the player
            if (collision.collider.CompareTag("Player"))
                InflictDamage();
        }
    }
}
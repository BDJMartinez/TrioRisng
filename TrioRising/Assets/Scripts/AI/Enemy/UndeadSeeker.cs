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
        #region SEEKER_PROPERTIES
        [Header(" ---- Seeker Properties ---- ")]

        public float DetectionRange = 10f;

        public float AttackRange = 2f;

        public float FleeHealthThreshold = 20f;

        private BaseAIState _currentState;
        #endregion

        #region LIFECYCLE METHODS
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
            if (IsPlayerInSightRange() && IsTargetVisible)
            {
                if (IsPlayerInAttackRange() && IsNearTarget) 
                    AttackTarget(); 
                else
                    EngageTarget();
            }

        }
        #endregion

        #region STATE_CONTROL_&_BEHAVIOR
        #region STATE
        // Changes the Seekers current state to a new state
        public void TransitionToState(BaseAIState _newState)
        {
            _currentState = _newState;
            Debug.Log($"Seeker transitioned to {_currentState.Name} state.");
        }

        // Triggers engage behavior at the player
        public void EngageTarget()
        {
            TransitionToState(new EngageState(this));           // Transition to engage state
            Debug.Log("Seeker has engaged the target!");
        }

        // Triggers attack behavior against the player
        public void AttackTarget()
        {
            this.IsEngagingTarget = !this.IsEngagingTarget;     // Toggle off when transitioning to the new state
            Debug.Log($"{this} is attacking the target");
            TransitionToState(new BaseAttackState(this));       // Transition to attack state
            Debug.Log($"{this} has transitioned to {this._currentState}");
            // TODO: Attack animation logic
        }

        // Completes attack state then transitions to retreat state
        private void OnAttackFinished()
        {
            TransitionToState(new RetreatingState(this));
            Debug.Log("Enemy is retreating...");
        }

        // Adds the Undead enemy to the attack queue
        public void QueueToAttack()
        {
            TransitionToState(new QueueState(this));
            Debug.Log("Seeker has joined the queue to attack...");
        }

        // Triggers retreat from the target and rejoins the attack queue
        public void RetreatFromTarget()
        {
            Debug.Log("Seeker is retreating to reenter the queue...");
            TransitionToState(new RetreatingState(this));
        }

        // Moves the enemy to the retreat position
        private void HandleRetreating()
        {
            if (!isRetreating) { return; }

            // Move toward the retreat point 
            transform.position = Vector3.MoveTowards(transform.position, retreatPosition.position, retreatSpeed * Time.deltaTime);
            // Check if the AI has reached the retreat point
            if (Vector3.Distance(transform.position, retreatPosition.position) < 0.1f)
                FinishRetreat();
        }

        // Triggers the end of the retreat state 
        private void FinishRetreat()
        {
            isRetreating = false;
            enemyManager.AddToQueue(this);
            TransitionToState(new QueueState(this));
        }

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
        #endregion

        #region BEHAVIOR
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

        // Inflicts damage on the AI's target when contact is made
        public void InflictDamage() { Target.GetComponent<PlayerController_Deprecated>().TakeDamage(attackDamage, this.gameObject); }    // Inflict damage on the player
        #endregion
        #endregion

        #region HANDLERS
        // Checks if collision is with the player via tag, if so inflicts damage on the player
        private void OnCollisionEnter(Collision collision)
        {
            if (collision == null) return;
            // If the player is the collider is tagged as the player
            if (collision.collider.CompareTag("Player"))
                InflictDamage();
        }
        #endregion
    }
}
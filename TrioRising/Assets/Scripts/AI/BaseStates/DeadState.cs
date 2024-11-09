using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UndeadWarfare.AI;

namespace UndeadWarfare.AI.State
{
    public class DeadState : BaseAIState
    {
        public DeadState(BaseUndead owner) : base(owner)
        {
            Name = EnemyState.Dead;
            EnterDeadState();
        }

        public override void Run()
        {
            // NOOP
        }

        private void EnterDeadState()
        {
            Owner.NavAgent.isStopped = true;        // Stop the AI agent to prevent any movement
            Owner.GetComponent<Collider>().enabled = false;         // Disable collisions and interactions
            // Play death effect
            PlayDeathEffect();
        }

        private void PlayDeathEffect()
        {
            GameObject.Destroy(this);       // Destroy the game object for now
        }
    }
}

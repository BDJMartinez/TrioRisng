using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UndeadWarfare.AI.State
{
    public class FleeState : BaseAIState
    {
        public FleeState(BaseUndead owner) : base(owner) { Name = EnemyState.Flee; }
        public override void Run()
        {
            Owner.IsAvailable = false;
            if (Owner.Target == null)
            {
                Owner.UpdateState(new IdleState(Owner));
                return;
            }
            // Calculate the flee direction, which is directly opposite to the targets' position
            Vector3 fleeDirection = CalculateFleeDirection();
            Vector3 fleePosition = Owner.transform.position + fleeDirection;
            // Set speed and destination to simulate a hasty retreat
            Owner.NavAgent.SetDestination(fleePosition);
            Owner.NavAgent.speed = Owner.MovementSpeed * 1.5f; // Increase speed for fleeing behavior
            // Continously check target's distance to ensure AI is moving away from the target
            Owner.CheckTargetProximity();
            // If AI is far enough from the target switch to safe state
            if (!Owner.IsNearTarget) { Owner.UpdateState(new IdleState(Owner)); }       // Transitiion to idle state if far from the player
        }
        // Calculates a direction to flee by in the opposite direction of the players' position
        public Vector3 CalculateFleeDirection()
        {
            Vector3 toTarget = Owner.Target.transform.position - Owner.transform.position;
            Vector3 fleeDirection = -toTarget.normalized;        // Invert the vector to get the opposite direction
            return fleeDirection * 5;       // Returns a vector multiplied by a offset value
        }
    } 
}

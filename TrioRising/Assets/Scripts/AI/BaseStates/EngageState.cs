using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UndeadWarfare.AI.State
{
    public class EngageState : BaseAIState
    {
        public EngageState(BaseUndead owner) : base(owner) { Name = EnemyState.Engage; }
        public override void Run()
        {
            GetOwner();
            Owner.IsAvailable = false;
            Owner.RotateTowardsTarget();
            Owner.CheckTargetProximity();
            Owner.CheckTargetVisiblity();
            
            if (Owner.IsTargetVisible)
                Owner.NavAgent.speed = Owner.MovementSpeed * Owner.MovementSpeedMultiplier;         // Multiply the movement speed if the target is visible
            else
                Owner.NavAgent.speed = Owner.MovementSpeed;         // Normal movement speed when the enemy is not visible 
        }
    } 
}
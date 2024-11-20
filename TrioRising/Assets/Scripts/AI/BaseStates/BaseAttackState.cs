using System.Collections;
using System.Collections.Generic;
using UndeadWarfare.AI;
using UnityEngine;

namespace UndeadWarfare.AI.State
{
    public class BaseAttackState : UndeadWarfare.AI.BaseAIState
    {
        public BaseAttackState(BaseUndead owner) : base(owner) { Name = EnemyState.Attack; }
        public override void Run()
        {
            Owner.MoveToPosition();         // Move the AI based on its current move settings
            Owner.CheckTargetProximity();       // Check if the target is close enough
            Owner.CheckTargetVisiblity();      // Check if the Target is visible
            if (Owner.IsAttacking) { return; }      // If the target is already attacking, do not continue
            if (Owner.IsNearTarget && Owner.IsTargetVisible) { Owner.UpdateState(new BaseAttackState(Owner)); }      // If the target is near and visible, enter another attack state
        }
    }
}

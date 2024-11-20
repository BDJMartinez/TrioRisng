using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UndeadWarfare.AI.State
{
    public class IdleState : BaseAIState
    {
        public IdleState(BaseUndead owner) : base(owner) { Name = EnemyState.Idle;  }
        public override void Run()
        {
            Owner.NavAgent.speed = 0;
            Owner.IsAvailable = true;
            Owner.CheckTargetProximity();
            Owner.CheckTargetVisiblity();
            if (Owner.IsNearTarget && Owner.IsTargetVisible)
                Owner.UpdateState(new EngageState(Owner));
        }
    }
}
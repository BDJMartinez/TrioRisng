using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UndeadWarfare.AI.State
{
    public class FlankState : BaseAIState
    {
        public FlankState(BaseUndead owner) : base(owner) { Name = EnemyState.Flank; }
        public override void Run()
        {
            Owner.IsAvailable = false;
            Owner.Target = gamemanager.instance.player.gameObject;
            Vector3 flankDirection = CalculateFlankDirection();
        }

        private Vector3 CalculateFlankDirection()
        {
            Vector3 toTarget = Owner.Target.transform.position - Owner.transform.position;
            Vector3 flankDirection = Quaternion.Euler( 0, 90, 0  ) * toTarget.normalized;
            flankDirection = Random.value > 0.5f ? flankDirection : -flankDirection;
            return flankDirection * 3f;
        }
    }
}

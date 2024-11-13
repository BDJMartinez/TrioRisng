using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UndeadWarfare.AI.State
{
    public class QueueState : BaseAIState
    {
        public QueueState(BaseUndead owner) : base(owner) { Name = EnemyState.Queue; }

        public override void Run()
        {
            // NOOP
        }
    } 
}

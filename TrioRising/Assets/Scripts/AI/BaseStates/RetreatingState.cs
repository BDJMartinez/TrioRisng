using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UndeadWarfare.AI.State
{
	public class RetreatingState : BaseAIState
	{
		public RetreatingState(BaseUndead owner) : base(owner) { Name = EnemyState.Retreat;  }

        public override void Run()
        {
            
        }
    }
}
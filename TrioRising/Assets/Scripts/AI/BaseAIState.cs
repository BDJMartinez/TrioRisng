using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UndeadWarfare.AI
{
    public class RegisterState : BaseAIState
    {
        public RegisterState(BaseUndead owner) : base(owner) { Name = EnemyState.Seek; }

        public override void Run()
        {
            // NOOP
        }
    }

    public abstract class BaseAIState : MonoBehaviour
    {
        public BaseUndead Owner;

        public EnemyState Name;

        public string FunctionName;


        public BaseAIState(BaseUndead owner)
        {
            Owner = owner; 
        }

        public abstract void Run();
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UndeadWarfare.AI
{
    public interface IDamage
    {
        public virtual void TakeDamage(int amount, GameObject source = null, bool weakspot = false) { }
        public void TakeDamage(int amount, Vector3 impulsePosition, GameObject source = null, bool weakspot = false);
    }
}
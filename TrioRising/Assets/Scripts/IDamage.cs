using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

namespace UndeadWarfare.AI
{
    public interface IDamage
    {
        public virtual void TakeDamage(int amount, GameObject source = null) { }
        public virtual void TakeDamage(int amount, GameObject source = null, bool weakspot = false) { }
        public virtual void TakeDamage(int amount, Vector3 impulsePosition, GameObject source = null, bool weakspot = false) {  }

        public virtual void takeDamage(int amount, Vector3 point) { }
    }
}
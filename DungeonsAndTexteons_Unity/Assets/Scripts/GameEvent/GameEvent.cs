using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DungeonsAndTexteons
{

    public class GameEvent : ScriptableObject
    {
        public virtual void Trigger() { Debug.Log("Event triggered: " + name); }
    }

}

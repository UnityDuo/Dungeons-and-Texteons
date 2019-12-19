using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DungeonsAndTexteons
{

    public class Damageable : MonoBehaviour
    {
        #region Fields

        public event Action<int> DamageReceived;

        [ReadOnly] public bool canBeDamaged = true;

        #endregion

        #region Methods

        public virtual void ReceiveDamage(int amount)
        {
            DamageReceived?.Invoke(amount);
        }

        #endregion
    }

}

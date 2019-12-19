using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DungeonsAndTexteons
{

    public class DamageableWithHp : Damageable
    {
        #region Fields

        public int maxHp = 5;

        private int currHp = 5;
        public int CurrHp
        {
            get
            {
                return currHp;
            }
            set
            {
                if(currHp != value)
                {
                    currHp = value;
                    if(currHp <= 0)
                    {
                        Die();
                    }
                }
            }
        }

        #endregion

        #region Unity Callbacks

        private void Start()
        {
            CurrHp = maxHp;
        }

        #endregion

        #region Methods

        public override void ReceiveDamage(int amount)
        {
            base.ReceiveDamage(amount);
            CurrHp -= amount;
        }

        private void Die()
        {
            Destroy(gameObject);
        }

        #endregion
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DungeonsAndTexteons
{

    public class Damager : MonoBehaviour
    {
        #region Fields

        public int damage = 1;
        public LayerMask damageLayers;

        #endregion

        #region Unity Callbacks

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if(IsLayerInLayermask(damageLayers, collision.gameObject.layer))
            {
                var damageable = collision.gameObject.GetComponent<Damageable>();
                if(damageable != null)
                {
                    if(damageable.canBeDamaged)
                        damageable.ReceiveDamage(damage);
                }
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (IsLayerInLayermask(damageLayers, collision.gameObject.layer))
            {
                var damageable = collision.gameObject.GetComponent<Damageable>();
                if (damageable != null)
                {
                    if (damageable.canBeDamaged)
                        damageable.ReceiveDamage(damage);
                }
            }
        }

        #endregion

        #region Methods

        public bool IsLayerInLayermask(LayerMask layerMask, int layer)
        {
            return layerMask == (layerMask | (1 << layer));
        }

        #endregion
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DungeonsAndTexteons
{

    public class PlayerCombat : MonoBehaviour
    {
        #region Fields

        public PlayerController playerController;
        public float invulnerabilityTimeAfterHit = 2f;
        public Color invulnerabilityColor;

        private Damageable damageable;

        #endregion

        #region Unity Callbacks
        private void Start()
        {
            damageable = GetComponent<Damageable>();
            if (damageable != null)
            {
                damageable.DamageReceived += ReceiveDamage;
            }
        }

        #endregion

        #region Methods

        public void ReceiveDamage(int amount)
        {
            GameManager.instance.CurrPlayerHp -= amount;
            InvulnerablePeriod();
        }

        private void InvulnerablePeriod()
        {
            StartCoroutine(InvulnerabilityCor());
        }

        private IEnumerator InvulnerabilityCor()
        {
            damageable.canBeDamaged = false;
            playerController.Blink(invulnerabilityTimeAfterHit, invulnerabilityColor);
            yield return new WaitForSeconds(invulnerabilityTimeAfterHit);
            damageable.canBeDamaged = true;
        }

        #endregion
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DungeonsAndTexteons
{

    public class PlayerCombat : MonoBehaviour
    {
        #region Fields

        public PlayerController playerController;
        public Transform projectileSpawnPoint;
        public float invulnerabilityTimeAfterHit = 2f;
        public Color invulnerabilityColor;
        public KeyCode attackKey = KeyCode.Space;

        public ProjectileData projectileData = new ProjectileData();

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

        private void Update()
        {
            if(Input.GetKeyDown(attackKey))
            {
                SpawnProjectiles();
            }
        }

        #endregion

        #region Methods

        public void ReceiveDamage(int amount)
        {
            InvulnerablePeriod();
            GameManager.instance.CurrPlayerHp -= amount;
        }

        private void SpawnProjectiles()
        {
            for (int i = 0; i < projectileData.numberOfProjcetiles; i++)
            {
                var newProjectile = Instantiate(projectileData.projectileObject);
                newProjectile.transform.position = projectileSpawnPoint.position;

                var projScript = newProjectile.GetComponent<Projectile>();
                projScript.speed *= projectileData.speedMultiplier;
                projScript.damager.damage *= projectileData.damageMultiplier;
            }
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

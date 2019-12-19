using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace DungeonsAndTexteons
{

    public class EndlessMovement : MonoBehaviour
    {
        #region Fields

        [Range(0f, 359f),InfoBox("0 is up, 180 is down, 90 is right, 270 is left")] public float direction = 180f;
        public float speedMultiplier = 1f;

        #endregion

        #region Unity Callbacks

        private void Start()
        {
            transform.Rotate(Vector3.forward, direction);
            var gameManager = GameManager.instance;
            if(gameManager != null)
            {
                gameManager.WorldResetted += Destroy;
            }
            var damageableComponenet = GetComponent<DamageableWithHp>();
            if(damageableComponenet != null)
            {
                damageableComponenet.DamageableDestroyed += Destroy;
            }

            
        }

        private void FixedUpdate()
        {
            MoveForward();
        }

        #endregion

        #region Methods

        public void Destroy()
        {
            var gameManager = GameManager.instance;
            if (gameManager != null)
            {
                gameManager.WorldResetted -= Destroy;
            }
            Destroy(gameObject);
        }

        private void MoveForward()
        {
            transform.position += transform.up * GameManager.instance.globalSpeed * speedMultiplier * 5.1f * Time.deltaTime;
        }

        #endregion
    }

}

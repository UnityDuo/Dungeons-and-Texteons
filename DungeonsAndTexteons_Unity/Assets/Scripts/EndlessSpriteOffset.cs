using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DungeonsAndTexteons
{

    [RequireComponent(typeof(SpriteRenderer))]
    public class EndlessSpriteOffset : MonoBehaviour
    {
        #region Fields

        public float speed = 1f;

        private SpriteRenderer spriteRenderer;

        #endregion

        #region Unity Callbacks

        private void Awake()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }

        private void Start()
        {
            WorldMovementManager worldMovementManager = FindObjectOfType<WorldMovementManager>();
            if (worldMovementManager != null)
            {
                worldMovementManager.WorldMoved += Move;
            }
        }

        #endregion

        #region Methods

        public void Move(Vector2 worldMovement)
        {
            spriteRenderer.material.mainTextureOffset += worldMovement * speed;
        }

        #endregion
    }

}

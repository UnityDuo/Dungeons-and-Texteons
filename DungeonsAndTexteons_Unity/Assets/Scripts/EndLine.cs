using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DungeonsAndTexteons
{

    public class EndLine : MonoBehaviour
    {
        public List<SpriteRenderer> spriteRenderers;
        public Sprite brokenSprite;

        #region Unity Callbacks

        private void OnTriggerEnter2D(Collider2D collision)
        {
            var gameManager = GameManager.instance;
            if (gameManager != null)
            {
                gameManager.LevelPassed();
                foreach(SpriteRenderer spriteRenderer in spriteRenderers)
                {
                    spriteRenderer.sprite = brokenSprite;
                }
            }
        }

        #endregion
    }

}

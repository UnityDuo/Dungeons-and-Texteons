﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DungeonsAndTexteons
{

    public class CheckPoint : MonoBehaviour
    {
        #region Fields

        public int checkPointId = 0;
        public SpriteRenderer spriteRenderer;
        public Sprite activatedSprite;

        #endregion

        #region Unity Callbacks

        private void OnTriggerEnter2D(Collider2D collision)
        {
            var gameManager = GameManager.instance;
            if (gameManager != null)
            {
                gameManager.currCheckPointIndex = checkPointId;
                spriteRenderer.sprite = activatedSprite;
            }
        }

        #endregion

        #region Unity Methods

        public void Initialize(int id)
        {
            checkPointId = id;
        }

        #endregion
    }

}

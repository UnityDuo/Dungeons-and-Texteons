using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DungeonsAndTexteons
{

    public class EndLine : MonoBehaviour
    {
        #region Unity Callbacks

        private void OnTriggerEnter2D(Collider2D collision)
        {
            var gameManager = GameManager.instance;
            if (gameManager != null)
            {
                gameManager.LevelPassed();
            }
        }

        #endregion
    }

}

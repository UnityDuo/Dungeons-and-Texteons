using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DungeonsAndTexteons
{

    public class UIHealthBar : MonoBehaviour
    {
        #region Fields

        public Transform heartsContainer;
        public GameObject heartPrefab;

        private List<UIHeart> createdHearts = new List<UIHeart>();

        #endregion

        #region Unity Callbacks

        private void Start()
        {
            var gameManager = GameManager.instance;
            if(gameManager != null)
            {
                gameManager.PlayerHpChanged += UpdateHearts;
                gameManager.CallPlayerHpChanged();
            }
        }

        #endregion

        #region Methods

        private void UpdateHearts(int currHp, int maxHp)
        {
            int i = 0;
            for(i = 0; i < maxHp; i++)
            {
                if (i < createdHearts.Count)
                {
                    createdHearts[i].SetOn(i < currHp);
                }
                else
                {
                    var newHeart = CreateNewHeart();
                    newHeart.SetOn(i < currHp);
                }
            }
            for(int j = createdHearts.Count-1; j > i; j--)
            {
                Destroy(createdHearts[j].gameObject);
                createdHearts.RemoveAt(j);
            }
        }

        private UIHeart CreateNewHeart()
        {
            var newHeart = Instantiate(heartPrefab, heartsContainer);
            var newHeartScript = newHeart.GetComponent<UIHeart>();
            createdHearts.Add(newHeartScript);
            return newHeartScript;
        }

        #endregion
    }

}

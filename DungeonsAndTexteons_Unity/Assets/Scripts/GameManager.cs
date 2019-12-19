using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using System;

namespace DungeonsAndTexteons
{

    public class GameManager : Singleton<GameManager>
    {
        #region Fields

        public event Action<int, int> PlayerHpChanged;

        public string sceneLoadedOnDeath = "";
        public float globalSpeed = 1f;
        public int playerMaxHp = 3;

        [ReadOnly] public int currPlayerHp = 3;
        public int CurrPlayerHp
        {
            get
            {
                return currPlayerHp;
            }
            set
            {
                if(currPlayerHp != value)
                {
                    currPlayerHp = value;
                    CallPlayerHpChanged();
                    if (currPlayerHp <= 0)
                    {
                        GameLost();
                    }
                }
            }
        }

        #endregion

        #region Unity Callbacks

        #endregion

        #region Methods

        public void CallPlayerHpChanged()
        {
            PlayerHpChanged?.Invoke(CurrPlayerHp, playerMaxHp);
        }

        private void GameLost()
        {
            SceneLoader.instance.LoadScene(sceneLoadedOnDeath);
        }

        #endregion
    }

}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DungeonsAndTexteons
{

    public class WorldMovementManager : MonoBehaviour
    {
        #region Fields

        public event Action<Vector2> WorldMoved;

        public float globalVSpeed = 1f;
        public float globalHSpeed = 0f;

        #endregion

        #region Unity Callbacks

        private void Update()
        {
            WorldMoved?.Invoke(new Vector2(globalHSpeed, globalVSpeed) * Time.deltaTime);
        }

        #endregion

        #region Methods

        #endregion
    }

}

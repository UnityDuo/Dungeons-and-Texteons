using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace DungeonsAndTexteons
{

    [CreateAssetMenu(fileName = "change_global_speed_event", menuName = "Game Event/New Change Global Speed Event")]
    public class ChangeGlobalSpeedEvent : GameEvent
    {
        #region Fields

        public float targetSpeed = .75f;

        #endregion

        #region Methods

        public override void Trigger()
        {
            base.Trigger();
            SetGlobalSpeed();
        }

        [Button("Set Default Speed")]
        private void SetTargetSpeedDefault()
        {
            targetSpeed = .75f;
        }

        private void SetGlobalSpeed()
        {
            var gameManager = GameManager.instance;
            if(gameManager != null)
            {
                gameManager.SetGlobalSpeed(targetSpeed);
            }
        }

        #endregion
    }

}

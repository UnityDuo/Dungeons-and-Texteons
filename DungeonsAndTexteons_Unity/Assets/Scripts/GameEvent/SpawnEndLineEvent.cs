using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace DungeonsAndTexteons
{

    [CreateAssetMenu(fileName = "spawn_end_line_event", menuName = "Game Event/New Spawn End Line Event")]
    public class SpawnEndLineEvent : GameEvent
    {
        #region Fields

        public GameObject endLineObject;

        #endregion

        #region Methods

        public override void Trigger()
        {
            base.Trigger();
            SpawnCheckPoint();
        }

        private void SpawnCheckPoint()
        {
            GameObject objectSpawned = Instantiate(endLineObject);
            ElementSpawner.instance.SpawnElement(objectSpawned, .5f, SpawnSide.Top);

        }

        #endregion
    }

}

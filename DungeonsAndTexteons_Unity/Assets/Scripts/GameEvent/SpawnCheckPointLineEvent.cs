using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace DungeonsAndTexteons
{

    [CreateAssetMenu(fileName = "spawn_checkpoint_line_event", menuName = "Game Event/New Spawn CheckPoint Line Event")]
    public class SpawnCheckPointLineEvent : GameEvent
    {
        #region Fields

        public GameObject gameLineObject;
        public int checkpointId = 0;

        #endregion

        #region Methods

        public override void Trigger()
        {
            base.Trigger();
            SpawnCheckPoint();
        }

        private void SpawnCheckPoint()
        {
            GameObject objectSpawned = Instantiate(gameLineObject);
            CheckPoint checkPoint = objectSpawned.GetComponent<CheckPoint>();
            checkPoint.Initialize(checkpointId);
            ElementSpawner.instance.SpawnElement(objectSpawned, .5f, SpawnSide.Top);

        }

        #endregion
    }

}

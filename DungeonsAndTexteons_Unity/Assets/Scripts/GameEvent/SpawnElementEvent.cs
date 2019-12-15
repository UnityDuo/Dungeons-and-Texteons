using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace DungeonsAndTexteons
{

    [CreateAssetMenu(fileName = "spawn_element_event", menuName = "Game Event/New Spawn Element Event")]
    public class SpawnElementEvent : GameEvent
    {
        #region Fields

        public GameObject spawnedObject;
        public SpawnSide spawnSide = SpawnSide.Top;
        public bool isRandomPos = false;
        [HideIf("isRandomPos"), Range(0f,1f)]
        public float normalizedPosition = .5f;

        #endregion

        #region Methods

        public override void Trigger()
        {
            base.Trigger();
            SpawnObject();
        }

        private void SpawnObject()
        {
            GameObject objectSpawned = Instantiate(spawnedObject);
            float posValue = normalizedPosition;
            if (isRandomPos)
            {
                posValue = Random.Range(0f,1f);
            }

            ElementSpawner.instance.SpawnElement(objectSpawned, posValue, spawnSide);

        }

        #endregion
    }

}

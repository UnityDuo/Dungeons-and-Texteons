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
        public List<TimedGameEvent> gameEvents;

        private int currentGameEventIndex = 0;

        #endregion

        #region Unity Callbacks

        private void Start()
        {
            InitializeWorld();
        }

        private void FixedUpdate()
        {
            WorldMoved?.Invoke(new Vector2(globalHSpeed, globalVSpeed) * GameManager.instance.globalSpeed * Time.deltaTime);
        }

        #endregion

        #region Methods

        public void InitializeWorld()
        {
            StartCoroutine(GameEventsCor());
        }

        private IEnumerator GameEventsCor()
        {
            while(currentGameEventIndex < gameEvents.Count)
            {
                yield return new WaitForSecondsRealtime(gameEvents[currentGameEventIndex].timerBeforeTrigger);

                foreach(GameEvent gameEvent in gameEvents[currentGameEventIndex].eventsToTrigger)
                {
                    gameEvent.Trigger();
                }

                currentGameEventIndex++;
            }
        }

        #endregion
    }

    [System.Serializable]
    public class TimedGameEvent
    {
        public List<GameEvent> eventsToTrigger;
        public float timerBeforeTrigger = 1f;
    }

}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace DungeonsAndTexteons
{

    public class WorldMovementManager : Singleton<WorldMovementManager>
    {
        #region Fields

        public event Action<Vector2> WorldMoved;
        public event Action<float> TrackPositionChanged;

        public float globalVSpeed = 1f;
        public float globalHSpeed = 0f;
        public int currentGameEventIndex;
        public List<TimedGameEvent> gameEvents;

        #endregion

        #region Unity Callbacks

        private void Start()
        {
            InitializeWorld();
            CallTrackPositionChanged(0);
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

        public void CallTrackPositionChanged(float forcedValue = -1)
        {
            TrackPositionChanged?.Invoke(forcedValue == -1 ? (float)currentGameEventIndex / (gameEvents.Count - 1) : forcedValue);
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
                CallTrackPositionChanged();
            }
        }

        #endregion
    }

    [System.Serializable]
    public class TimedGameEvent
    {
        public float timerBeforeTrigger = 1f;
        public List<GameEvent> eventsToTrigger;
    }

}

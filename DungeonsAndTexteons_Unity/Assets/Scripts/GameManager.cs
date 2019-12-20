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
        public event Action WorldResetted;
        public event Action<float> TrackPositionChanged;

        public int thisLevelIndex = 0;
        public List<string> levelsScenes = new List<string>();
        public float globalSpeed = 1f;
        public int playerMaxHp = 3;

        public string menuSceneName = "Menu";

        [ReadOnly] public int currCheckPointIndex = -1;
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

        private Coroutine globalSpeedChangeCor = null;

        #endregion

        #region Unity Callbacks

        private void Update()
        {
            if(Input.GetKeyDown(KeyCode.Escape))
            {
                GoToMainMenu();
            }
        }

        #endregion

        #region Methods

        public void ResetWorldToLastCheckpoint()
        {
            WorldResetted?.Invoke();
            CurrPlayerHp = playerMaxHp;
            var worldMovementManager = WorldMovementManager.instance;
            if (worldMovementManager != null)
            {
                int lastCheckpointIndex = -1;
                for (int i = 0; i < worldMovementManager.gameEvents.Count; i++)
                {
                    var checkpointEvent = worldMovementManager.gameEvents[i].eventsToTrigger[0] as SpawnCheckPointLineEvent;
                    if (checkpointEvent != null && currCheckPointIndex == checkpointEvent.checkpointId)
                    {
                        lastCheckpointIndex = i + 1;
                    }
                }
                worldMovementManager.currentGameEventIndex = lastCheckpointIndex != -1 ? lastCheckpointIndex : 0;
                worldMovementManager.CallTrackPositionChanged();
            }
        }

        public void CallPlayerHpChanged()
        {
            PlayerHpChanged?.Invoke(CurrPlayerHp, playerMaxHp);
        }

        public void LevelPassed()
        {
            var sceneLoader = SceneLoader.instance;
            thisLevelIndex++;
            if (thisLevelIndex < levelsScenes.Count && sceneLoader != null)
            {
                StartCoroutine(NextLevelCor(levelsScenes[thisLevelIndex], sceneLoader));
            }
            else
            {
                StartCoroutine(GameWin());
            }
        }

        public void SetGlobalSpeed(float value)
        {
            if (globalSpeedChangeCor != null)
            {
                StopCoroutine(globalSpeedChangeCor);
            }
            globalSpeedChangeCor = StartCoroutine(GlobalSpeedCor(value));
        }

        private IEnumerator GlobalSpeedCor(float desiredSpeed)
        {
            float startingSpeed = globalSpeed;
            float timeRequired = Math.Abs(startingSpeed - desiredSpeed) / .5f;
            float t = 0;
            while (t < timeRequired)
            {
                t += Time.deltaTime;
                float a = t / timeRequired;
                globalSpeed = Mathf.Lerp(startingSpeed, desiredSpeed, a);
                yield return null;
            }
            globalSpeed = desiredSpeed;
            globalSpeedChangeCor = null;
        }

        private void GameLost()
        {
            ResetWorldToLastCheckpoint();
        }

        private IEnumerator GameWin()
        {
            SetGlobalSpeed(.5f);
            UIWinPanel.instance.Win();
            yield return new WaitForSeconds(2f);
            GoToMainMenu();
        }

        private IEnumerator NextLevelCor(string sceneToLoadName, SceneLoader sceneLoader)
        {
            SetGlobalSpeed(.5f);
            UIWinPanel.instance.NextLevel();
            yield return new WaitForSeconds(2f);
            sceneLoader.LoadScene(sceneToLoadName);
        }
        
        private void GoToMainMenu()
        {
            UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(menuSceneName);
        }

        #endregion
    }

}

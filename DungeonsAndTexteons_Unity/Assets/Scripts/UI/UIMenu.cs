using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace DungeonsAndTexteons
{

    public class UIMenu : MonoBehaviour
    {
        #region Fields

        public Button startButton;
        public string startSceneName = "";
        public Button quitButton;

        #endregion

        #region Unity Callbakcs

        private void Start()
        {
            startButton.onClick.AddListener(StartGame);
            quitButton.onClick.AddListener(QuitGame);
        }

        #endregion

        #region Methods

        public void StartGame()
        {
            SceneManager.LoadSceneAsync(startSceneName);
        }

        public void QuitGame()
        {
            Application.Quit();
        }

        #endregion
    }

}

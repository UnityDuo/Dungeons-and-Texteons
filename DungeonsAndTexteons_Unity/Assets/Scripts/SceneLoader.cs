using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using UnityEngine.SceneManagement;

namespace DungeonsAndTexteons
{

    public class SceneLoader : SingletonDDOL<SceneLoader>
    {
        #region Fields

        #endregion

        #region Unity Callbacks

        #endregion

        #region Methods

        public void LoadScene(string sceneName)
        {
            SceneManager.LoadSceneAsync(sceneName);
        }

        #endregion
    }

}

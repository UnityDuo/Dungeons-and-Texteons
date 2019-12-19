using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DungeonsAndTexteons
{

    public class UIHeart : MonoBehaviour
    {
        #region Fields

        public List<Image> heartImages = new List<Image>();

        #endregion

        #region Methods

        public void SetOn(bool value)
        {
            foreach(Image image in heartImages)
            {
                image.enabled = value;
            }
        }

        #endregion
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DungeonsAndTexteons
{

    public class UILevelTracker : MonoBehaviour
    {
        #region Fields

        public Slider slider;

        private Coroutine changeSliderValueCor = null;

        #endregion

        #region Unity Callbacks

        private void Start()
        {
            var worldMovementManager = WorldMovementManager.instance;
            if(worldMovementManager != null)
            {
                worldMovementManager.TrackPositionChanged += SetSliderValue;
            }
        }

        #endregion

        #region Methods

        private void SetSliderValue(float normalizedValue)
        {
            if(changeSliderValueCor != null)
            {
                StopCoroutine(changeSliderValueCor);
            }
            changeSliderValueCor = StartCoroutine(ChangeSliderValueCor(normalizedValue));
        }

        private IEnumerator ChangeSliderValueCor(float targetValue)
        {
            float startingValue = slider.value;
            float t = 0, maxT = 2f, a = 0;
            while(t < maxT)
            {
                t += Time.deltaTime;
                a = t / maxT;
                slider.value = Mathf.Lerp(startingValue, targetValue, a);
                yield return null;
            }
            slider.value = targetValue;
            changeSliderValueCor = null;
        }

        #endregion
    }

}

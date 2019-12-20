using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace DungeonsAndTexteons
{

    public class UIWinPanel : Singleton<UIWinPanel>
    {
        #region Fields

        public TextMeshProUGUI youWinText;
        public TextMeshProUGUI nextLevelText;
        public CanvasGroup canvasGroup;

        #endregion

        #region Methods

        public void Win()
        {
            Fade(.7f, true);
            youWinText.gameObject.SetActive(true);
            nextLevelText.gameObject.SetActive(false);
        }

        public void NextLevel()
        {
            Fade(.7f, true);
            youWinText.gameObject.SetActive(false);
            nextLevelText.gameObject.SetActive(true);
        }

        private void Fade(float time, bool isIn)
        {
            if(isIn)
            {
                StartCoroutine(FadeIn(time));
            }
            else
            {
                StartCoroutine(FadeOut(time));
            }
        }

        private IEnumerator FadeIn(float time)
        {
            float t = 0;
            while(t < time)
            {
                t += Time.deltaTime;
                float a = t / time;
                canvasGroup.alpha = a;
                yield return null;
            }
            canvasGroup.alpha = 1;
            yield return new WaitForSeconds(.13f);

        }

        private IEnumerator FadeOut(float time)
        {
            float t = 0;
            while (t < time)
            {
                t += Time.deltaTime;
                float a = t / time;
                canvasGroup.alpha = 1 - a;
                yield return null;
            }
            canvasGroup.alpha = 0;
        }

        #endregion
    }

}

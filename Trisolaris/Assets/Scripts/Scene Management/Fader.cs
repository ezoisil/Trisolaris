using System.Collections;
using UnityEngine;

namespace Trisolaris.SceneManagement
{
    public class Fader : MonoBehaviour
    {
        // With canvas group we can controll all of the components under a canvas.
        CanvasGroup canvasGroup;

        private void Awake()
        {
            canvasGroup = GetComponent<CanvasGroup>();
        }
            
        public IEnumerator FadeOut(float time)
        {
            while (canvasGroup.alpha < 1)
            {
                // Time.deltaTime/time makes it run every frame in a smooth way
                canvasGroup.alpha += Time.deltaTime / time;
                yield return null;
            }          
        }

        public IEnumerator FadeIn(float time)
        {
            while (canvasGroup.alpha > 0)
            {
                canvasGroup.alpha -= Time.deltaTime / time;
                yield return null;
            }
        }

        public void FadeOutImmediate()
        {
            canvasGroup.alpha = 1;
        }
    }
}

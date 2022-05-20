using System.Collections;
using UnityEngine;

namespace Trisolaris.SceneManagement
{
    public class Fader : MonoBehaviour
    {
        // With canvas group we can controll all of the components under a canvas.
        CanvasGroup canvasGroup;
        Coroutine currentlyActiveFade = null;

        private void Awake()
        {
            canvasGroup = GetComponent<CanvasGroup>();
        }
            
        public Coroutine FadeOut(float time)
        {
            return Fade(1, time);
        }

        private IEnumerator FadeRoutine(float target, float time)
        {
            while ( !Mathf.Approximately(canvasGroup.alpha,target))
            {
                // Time.deltaTime/time makes it run every frame in a smooth way
                canvasGroup.alpha = Mathf.MoveTowards(canvasGroup.alpha, target, Time.deltaTime / time) ;
                yield return null;
            }
        }

        public Coroutine FadeIn(float time)
        {
            return Fade(0,time);
        }


        public Coroutine Fade(float target, float time)
        {
            if (currentlyActiveFade != null)
            {
                StopCoroutine(currentlyActiveFade);
            }
            currentlyActiveFade = StartCoroutine(FadeRoutine(target, time));
            return currentlyActiveFade;
        }
        public void FadeOutImmediate()
        {
            canvasGroup.alpha = 1;
        }
    }
}

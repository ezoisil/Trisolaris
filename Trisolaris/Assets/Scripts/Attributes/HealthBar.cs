using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Trisolaris.Attributes
{
    public class HealthBar : MonoBehaviour
    {
        [SerializeField] RectTransform foreground = null;
        [SerializeField] Health healthComponent = null;
       
        void Update()
        {
            SetScale();
        }

        private void SetScale()
        {
            float scale = healthComponent.GetFraction();
            foreground.localScale = new Vector3(scale, 1, 1);
        }
    }
}

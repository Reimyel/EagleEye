using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FourZeroFourStudios
{
    public class ButtonBehaviours : MonoBehaviour
    {
        public Canvas CanvasA;
        public Canvas CanvasB;
        public float DelayInSeconds = 0.2f;

        public void SwitchCanvas()
        {
            StartCoroutine(SwitchCanvasCoroutine(DelayInSeconds));
        }

        IEnumerator SwitchCanvasCoroutine(float _delayInSeconds)
        {
            yield return new WaitForSeconds(_delayInSeconds);
            if (CanvasA != null & CanvasB != null)
            {
                bool _isCanvasAActive = CanvasA.gameObject.activeSelf;
                bool _isCanvasBActive = CanvasB.gameObject.activeSelf;

                CanvasA.gameObject.SetActive(!_isCanvasAActive);
                CanvasB.gameObject.SetActive(!_isCanvasBActive);
            }
        }
    }
}

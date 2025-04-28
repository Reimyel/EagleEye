using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FourZeroFourStudios
{
    public class ButtonBehaviours : MonoBehaviour
    {
        public Canvas EagleEyeCanvas;
        public Canvas InitialCanvas;

        public void OpenEagleEyeCanvas()
        {
            EagleEyeCanvas.GetComponent<Canvas>().enabled = true;
            InitialCanvas.GetComponent<Canvas>().enabled = false;
        }
    }
}

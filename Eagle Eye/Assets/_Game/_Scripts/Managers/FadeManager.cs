using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FourZeroFourStudios
{
    public class FadeManager : MonoBehaviour
    {
        public static FadeManager Instance;

        [SerializeField] GameObject cg_fade; 

        void Awake() => Instance = this;

        public void StartFade() => cg_fade.SetActive(true);

        void StopFade() => cg_fade.SetActive(false);
    }
}

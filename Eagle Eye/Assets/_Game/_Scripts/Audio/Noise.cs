using System.Collections;
using UnityEngine;

namespace FourZeroFourStudios
{
    public class Noise : MonoBehaviour
    {
        // Inspector:
        [Header("References:")]
        [SerializeField] GameObject _go_cameraHolder;

        // Components:
        AmbienceMusicController _ambienceController;

        void Start()
        {
            _ambienceController = GetComponent<AmbienceMusicController>();
            _ambienceController.BeginAttached(_go_cameraHolder);
        }
    }
}

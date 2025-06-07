using UnityEngine;

namespace FourZeroFourStudios
{
    public class Noise : MonoBehaviour
    {
        // Inspector:
        [Header("References:")]
        [SerializeField] Transform _transf_camera;

        // Components:
        AmbienceMusicController _ambienceController;

        void Start()
        {
            _ambienceController = GetComponent<AmbienceMusicController>();
            _ambienceController.Begin(_transf_camera.position);
        }

        void Update() => _ambienceController.UpdatePos(_transf_camera.position);
    }
}

using UnityEngine;

namespace FourZeroFourStudios 
{
    public class CameraHolder : MonoBehaviour
    {
        Transform _cameraPosition;
        public bool IsPlayerSeated = false;

        void Awake() => _cameraPosition = GameObject.FindGameObjectWithTag("CameraPosition").transform;

        void Update() => UpdateTransform(_cameraPosition);

        void UpdateTransform(Transform cameraLocation)
        {
            transform.position = cameraLocation.position;
            transform.rotation = cameraLocation.rotation;
        }
    }
}


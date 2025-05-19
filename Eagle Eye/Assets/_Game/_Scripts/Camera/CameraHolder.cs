using UnityEngine;

namespace FourZeroFourStudios 
{
    public class CameraHolder : MonoBehaviour
    {
        Transform _cameraPosition;
        Transform _chairCameraPosition;
        public bool IsPlayerSeated = false;

        void Awake()
        {
            _cameraPosition = GameObject.FindGameObjectWithTag("CameraPosition").transform;
            _chairCameraPosition = GameObject.FindGameObjectWithTag("ChairCameraPosition").transform;
        }

        void Update()
        {
            if (!IsPlayerSeated)
                UpdateTransform(_cameraPosition);
            else
                UpdateTransform(_chairCameraPosition);
        }

        void UpdateTransform(Transform cameraLocation)
        {
            transform.position = cameraLocation.position;
            transform.rotation = cameraLocation.rotation;
        }
    }
}


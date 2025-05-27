using UnityEngine;

namespace FourZeroFourStudios 
{
    public class CameraHolder : MonoBehaviour
    {
        Transform _cameraPosition;
        Transform _chairCameraPosition;
        Transform _toiletCameraPosition;
        public bool IsPlayerSeated = false;
        public bool IsPlayerSeatedToilet = false;

        void Awake()
        {
            _cameraPosition = GameObject.FindGameObjectWithTag("CameraPosition").transform;
            _chairCameraPosition = GameObject.FindGameObjectWithTag("ChairCameraPosition").transform;
            _toiletCameraPosition = GameObject.FindGameObjectWithTag("ToiletCameraPosition").transform;
        }

        void Update()
        {
            if (IsPlayerSeatedToilet)
            {
                UpdateTransform(_toiletCameraPosition);
            }
            else if (IsPlayerSeated)
            {
                UpdateTransform(_chairCameraPosition);
            }
            else
            {
                UpdateTransform(_cameraPosition);
            }
        }

        void UpdateTransform(Transform cameraLocation)
        {
            transform.position = cameraLocation.position;
            transform.rotation = cameraLocation.rotation;
        }
    }
}


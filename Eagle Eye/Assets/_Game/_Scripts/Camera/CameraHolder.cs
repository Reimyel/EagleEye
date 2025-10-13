using UnityEngine;

namespace FourZeroFourStudios 
{
    public class CameraHolder : MonoBehaviour
    {
        public bool IsSeating = false;

        // Não serializadas
        Transform _cameraTransform;

        void Start() => _cameraTransform = GameObject.FindGameObjectWithTag("CameraPosition").transform;

        void Update()
        {
            if (IsSeating) return;

            UpdateTransform(_cameraTransform.position, _cameraTransform.rotation);
        }

        public void UpdateTransform(Vector3 position, Quaternion rotation)
        {
            transform.position = position;
            transform.rotation = rotation;
        }
    }
}


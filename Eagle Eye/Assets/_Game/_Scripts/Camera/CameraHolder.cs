using UnityEngine;

namespace FourZeroFourStudios 
{
    public class CameraHolder : MonoBehaviour
    {
        Transform _cameraPosition;

        void Awake() => _cameraPosition = GameObject.FindGameObjectWithTag("CameraPosition").transform;

        void Update() => UpdateTransform();

        void UpdateTransform()
        {
            transform.position = _cameraPosition.position;
            transform.rotation = _cameraPosition.rotation;
        }
    }
}


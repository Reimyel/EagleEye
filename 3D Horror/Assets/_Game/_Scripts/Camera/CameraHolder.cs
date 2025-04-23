using UnityEngine;

public class CameraHolder : MonoBehaviour
{
    Transform _transfCameraPosition;

    void Awake() => _transfCameraPosition = GameObject.FindGameObjectWithTag("CameraPosition").transform;

    void Update() => UpdateTransform();

    void UpdateTransform()
    {
        transform.position = _transfCameraPosition.position;
        transform.rotation = _transfCameraPosition.rotation;
    }
}

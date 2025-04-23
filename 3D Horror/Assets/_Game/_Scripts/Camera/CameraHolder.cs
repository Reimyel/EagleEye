using UnityEngine;

public class CameraHolder : MonoBehaviour
{
    Transform _transfCameraPosition;

    void Awake() => _transfCameraPosition = GameObject.FindGameObjectWithTag("CameraPosition").transform;

    void Update() => UpdatePosition();

    void UpdatePosition() => transform.position = _transfCameraPosition.position;
}

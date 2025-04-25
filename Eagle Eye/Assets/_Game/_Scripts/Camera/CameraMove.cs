using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FourZeroFourStudios 
{
    public class CameraMove : MonoBehaviour
    {
        [Header("Settings:")]
        [Space]

        [Header("Parameters:")]
        [SerializeField] float _mouseSensitivity = 200f;
        [SerializeField] Transform _player;

        float _xRotation = 0f;

        void Start() => HideCursor();

        void Update() => ApplyMove();

        void HideCursor() 
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }

        void ApplyMove() 
        {
            float mouseXLocal = Input.GetAxis("Mouse X") * _mouseSensitivity * Time.deltaTime;
            float mouseYLocal = Input.GetAxis("Mouse Y") * _mouseSensitivity * Time.deltaTime;

            // Up & Down
            _xRotation -= mouseYLocal;
            _xRotation = Mathf.Clamp(_xRotation, -90f, 90f);

            transform.localRotation = Quaternion.Euler(_xRotation, 0f, 0f);

            // Left & Right
            _player.Rotate(Vector3.up * mouseXLocal);
        }
    }
}

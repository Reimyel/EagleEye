using System.Threading;
using UnityEngine;

namespace FourZeroFourStudios 
{
    public class CameraMove : MonoBehaviour
    {
        #region Members

        #region Inspector
        [Header("Settings:")]
        [Space]

        [Header("Parameters:")]
        [SerializeField] float _mouseSensitivity = 200f;
        [SerializeField] Transform _player;
        [SerializeField] float _xRotation = 0f;
        #endregion

        // Mouse Position
        float _mouseX, _mouseY;
        #endregion

        #region Mono
        void Start() => HideCursor();

        void Update()
        {
            GetMousePosition();
            ApplyRotation();
        }
        #endregion

        #region Custom
        void HideCursor()
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }

        void GetMousePosition()
        {
            float _mouseX = Input.GetAxis("Mouse X") * _mouseSensitivity * Time.deltaTime;
            float _mouseY = Input.GetAxis("Mouse Y") * _mouseSensitivity * Time.deltaTime;
        }

        void ApplyRotation()
        {
            // Up & Down
            _xRotation -= _mouseY;
            _xRotation = Mathf.Clamp(_xRotation, -90f, 90f);

            transform.localRotation = Quaternion.Euler(_xRotation, 0f, 0f);

            // Left & Right
            _player.Rotate(Vector3.up * _mouseX);
        }
        #endregion
    }
}
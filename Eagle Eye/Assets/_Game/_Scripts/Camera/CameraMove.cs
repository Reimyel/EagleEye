using UnityEngine;

namespace FourZeroFourStudios 
{
    public class CameraMove : MonoBehaviour
    {
        [Header("Parâmetros:")]
        [SerializeField] float _mouseSensitivity = 200f;
        public bool MouseCanMoveScreen = true;

        [Header("Referências")]
        [SerializeField] CameraHolder _cameraHolder;
        [SerializeField] Transform _playerTransform;

        // Não serializadas
        float _xRotation = 0f;

        void Start() => HideCursor();

        void Update()
        {
            if (MouseCanMoveScreen)
                ApplyMove();
        }

        public void ShowCursor()
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }

        public void HideCursor() 
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
            _cameraHolder.transform.Rotate(Vector3.up * mouseXLocal);
            
            GetCurrentTransform().Rotate(Vector3.up * mouseXLocal);
        }
        
        Transform GetCurrentTransform()
        {
            Transform curTransform = _cameraHolder.IsSeating ? _cameraHolder.transform : _playerTransform;

            return curTransform;
        }
    }
}

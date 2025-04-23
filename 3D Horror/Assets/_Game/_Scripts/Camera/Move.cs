using UnityEngine;

public class Move : MonoBehaviour
{
    public float MouseSensitivity = 200f;
    public Transform PlayerBody;
    float _xRotation = 0f;

    void Update()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        float mouseX = Input.GetAxis("Mouse X") * MouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * MouseSensitivity * Time.deltaTime;

        _xRotation -= mouseY;
        _xRotation = Mathf.Clamp(_xRotation, -90f, 90f);

        //cima e baixo
        transform.localRotation = Quaternion.Euler(_xRotation, 0f, 0f);

        //esquerda e direita
        PlayerBody.Rotate(Vector3.up * mouseX);
    }
}

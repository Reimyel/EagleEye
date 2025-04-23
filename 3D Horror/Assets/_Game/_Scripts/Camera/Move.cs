using UnityEngine;

public class Move : MonoBehaviour
{
    public float MouseSensitivity = 200f;
    public Transform Player;
    float _xRotation = 0f;

    void Update()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        float mouseX = Input.GetAxis("Mouse X") * MouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * MouseSensitivity * Time.deltaTime;

        //cima e baixo
        _xRotation -= mouseY;
        _xRotation = Mathf.Clamp(_xRotation, -90f, 90f);

        transform.localRotation = Quaternion.Euler(_xRotation, 0f, 0f);

        //esquerda e direita
        Player.Rotate(Vector3.up * mouseX);
    }
}

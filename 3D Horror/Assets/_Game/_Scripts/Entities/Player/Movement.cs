using UnityEngine;

public class Movement : MonoBehaviour
{
    public CharacterController controller;

    public float Speed = 6.5f;
    public float Gravity = -9.81f;
    public float JumpHeight = 3f;

    public Transform GroundCheck;
    public float GroundDistance = 0.4f;
    public LayerMask GroundMask;

    Vector3 _velocity;
    public bool IsGrounded;

    void Update()
    {
        IsGrounded = Physics.CheckSphere(GroundCheck.position, GroundDistance, GroundMask);

        if (IsGrounded && _velocity.y < 0)
        {
            _velocity.y = -2f;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move * Speed * Time.deltaTime);

        if (Input.GetButtonDown("Jump") && IsGrounded)
        {
            _velocity.y = Mathf.Sqrt(JumpHeight * -2f * Gravity);
        }

        _velocity.y += Gravity * Time.deltaTime;

        controller.Move(_velocity * Time.deltaTime);
    }
}

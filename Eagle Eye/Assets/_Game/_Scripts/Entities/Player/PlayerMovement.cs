using FMOD.Studio;
using Unity.VisualScripting;
using UnityEngine;

namespace FourZeroFourStudios 
{
    public class PlayerMovement : MonoBehaviour
    {
        #region Members

        #region Inspector
        [Header("Settings:")]
        [Space]

        [Header("References:")]
        [SerializeField] EntitySFXController _sfxController;
        [SerializeField] CharacterController _charController;
        [SerializeField] Transform _groundCheck;
        [SerializeField] CameraHeadBob _headBob;
        [Space]

        [Header("Parameters:")]
        [SerializeField] float _speed = 6.5f;
        [SerializeField] float _gravity = -9.81f;
        //[SerializeField] float _jumpHeight = 3f;
        [SerializeField] float _groundDistance = 0.4f;
        [SerializeField] LayerMask _groundMask;
        [Space]
        #endregion

        // Control
        Vector2 _moveInput;
        float _gravityForce;
        bool _isGrounded;

        EventInstance _sfxLoop_walk;
        bool _playingSFXWalk = false;

        #endregion

        #region Mono
        void Start() => _sfxLoop_walk = _sfxController.CreateLoop("Walk");
        
        void Update()
        {
            GroundCheck();
            MoveInput();
            CheckGravityForce();
            ApplyMove();
            ApplyGravityForce();
        }

        void OnEnable() => _headBob.enabled = true;
        #endregion

        #region Custom

        #region Ground Check
        void GroundCheck() => _isGrounded = Physics.CheckSphere(_groundCheck.position, _groundDistance, _groundMask);
        #endregion

        #region Movement
        void MoveInput() => _moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        void ApplyMove()
        {
            Vector3 moveLocal = transform.right * _moveInput.x + transform.forward * _moveInput.y;

            if (moveLocal.magnitude > 0f) 
            {
                _sfxLoop_walk.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(transform.position));
                
                if (!_playingSFXWalk) 
                {
                    _sfxLoop_walk.start();
                    _playingSFXWalk = true;
                }
            }
            else 
            {
                _sfxLoop_walk.stop(STOP_MODE.ALLOWFADEOUT);
                _playingSFXWalk = false;
            }
            
            _charController.Move(moveLocal * _speed * Time.deltaTime);
        }
        #endregion

        #region Gravity
        void CheckGravityForce()
        {
            if (_isGrounded && _gravityForce < 0)
                _gravityForce = -2f;
        }

        void ApplyGravityForce()
        {
            _gravityForce += _gravity * Time.deltaTime;

            _charController.Move(_gravityForce * Vector3.up * Time.deltaTime);
        }

        /*
        void ApplyJump() 
        {
           if (Input.GetButtonDown("Jump") && _isGrounded)
               _gravityForce = Mathf.Sqrt(_jumpHeight * -2f * _gravity);
        }
        */
        #endregion

        #endregion
    }
}


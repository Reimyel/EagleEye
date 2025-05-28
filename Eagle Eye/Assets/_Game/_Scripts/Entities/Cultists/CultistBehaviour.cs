using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FourZeroFourStudios
{
    public class CultistBehaviour : MonoBehaviour
    {
        // Inspector:
        public float Speed;
        [SerializeField] Animator _anim;
        Collider _cultistCollider;

        Vector3 _beginningPos = new Vector3(-56.21124f, 5.082796f, 39.05781f);
        Quaternion _beginningRot = Quaternion.Euler(0f, 270f, 0f);

        Vector3 _endingPos = new Vector3(-80.05568f, 5.082796f, 39.05781f);

        Vector3 _hidingPos = new Vector3(-29.63957f, 5.082796f, 11f);
        Quaternion _HidingRot = Quaternion.Euler(0f, -110f, 0f);
        Vector3 _vanishPos = new Vector3(-29.4647f, 5.082796f, 7f);

        Vector3 _bathroomPos = new Vector3(-80.72201f, 5.082796f, 15.56195f);
        Quaternion _bathroomRot = Quaternion.Euler(0f, 360f, 0f);

        bool _isMoving = false;
        bool _isHiding = false;
        bool _isVanishing = false;
        bool _isInBathroom = false;
        bool _canMoveBathroom = false;

        void Start()
        {
            _cultistCollider = GetComponent<CapsuleCollider>();

            transform.position = _beginningPos;
            transform.rotation = _beginningRot;
        }

        void Update()
        {
            if (_isMoving)
            {
                StartMoving();
            }

            if (_isHiding)
            {
                StartHiding();
            }

            if (_isVanishing)
            {
                StartVanishing();
            }

            if (_isInBathroom)
            {
                GoToBathroom();
                if (_canMoveBathroom)
                {
                    StartMovingBathroom();
                }
            }
        }

        #region CALL FUNCTIONS
        public void Move()
        {
            _isMoving = true;
            SetWalkAnimation();
        }

        public void Hide()
        {
            _isHiding = true;
            SetHidingAnimation();
        }

        public void Vanish()
        {
            _isVanishing = true;
            _isHiding = false;
            _cultistCollider.enabled = false;
        }

        public void HideBathroom()
        {
            _isInBathroom = true;
            SetHidingAnimation();
        }

        public void MoveBathroom()
        {
            _canMoveBathroom = true;
            SetWalkAnimation();
        }
        #endregion

        #region Walk Sequence
        void StartMoving() 
        {
            transform.position = Vector3.MoveTowards(transform.position, _endingPos, Speed * Time.deltaTime);

            float _distance = Vector3.Distance(transform.position, _endingPos);
            if (_distance < 0.1f)
            {
                _isMoving = false;
                gameObject.SetActive(false);
            }
        }
        #endregion

        #region Hide Sequence
        void StartHiding()
        {
            transform.position = _hidingPos;
            transform.rotation = _HidingRot;
        }

        void StartVanishing()
        {
            transform.position = Vector3.MoveTowards(transform.position, _vanishPos, Speed * Time.deltaTime);

            float _distance = Vector3.Distance(transform.position, _vanishPos);
            if (_distance < 0.1f)
            {
                _isVanishing = false;
                gameObject.SetActive(false);
            }
        }
        #endregion

        #region Bathroom Sequence
        void GoToBathroom()
        {
            transform.position = _bathroomPos;
            transform.rotation = _bathroomRot;
        }

        void StartMovingBathroom()
        {
            Vector3 _walkPoint1 = new Vector3(-80.72201f, 5.082796f, 20.99621f);
            Vector3 _walkPoint2 = new Vector3(-66.12108f, 5.082796f, 20.99621f);
            Quaternion _rotation = Quaternion.Euler(0f, 90f, 0f);

            transform.position = Vector3.MoveTowards(transform.position, _walkPoint1, Speed * Time.deltaTime);
            transform.rotation = _rotation;

            //if close to point, walk to other point
            if (Vector3.Distance(transform.position, _walkPoint1) < 0.1f)
            {
                transform.position = Vector3.MoveTowards(transform.position, _walkPoint2, Speed * Time.deltaTime);

                if (Vector3.Distance(transform.position, _walkPoint2) < 0.1f)
                {
                    gameObject.SetActive(false);
                }
            }
        }
        #endregion

        #region Set Animations
        void SetWalkAnimation() => _anim.Play("Anim_Cultist_Walk");

        void SetHidingAnimation() => _anim.Play("Anim_Cultist_Hiding");
        #endregion
    }
}

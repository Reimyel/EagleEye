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

        bool _isMoving = false;
        bool _isHiding = false;
        bool _isVanishing = false;

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
        }

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

        void SetWalkAnimation() => _anim.Play("Anim_Cultist_Walk");

        void SetHidingAnimation() => _anim.Play("Anim_Cultist_Hiding");
    }
}

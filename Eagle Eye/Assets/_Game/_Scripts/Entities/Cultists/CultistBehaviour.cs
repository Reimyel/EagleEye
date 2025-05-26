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

        Vector3 _beginningPos = new Vector3(-56.21124f, 4.861425f, 39.05781f);
        Vector3 _endingPos = new Vector3(-80.05568f, 4.861425f, 39.05781f);
        bool _canMove = false;

        void Start()
        {
            gameObject.transform.position = _beginningPos;
            SetWalkAnimation();
        }

        void Update() => Move();

        void Move() 
        {
            if (!_canMove) return;
            
            gameObject.transform.position = Vector3.MoveTowards(transform.position, _endingPos, Speed * Time.deltaTime);

            float _distance = Vector2.Distance(transform.position, _endingPos);
            if (_distance < 0.1f)
                 gameObject.SetActive(false);
        }

        void SetWalkAnimation() => _anim.Play("Anim_Cultist_Walk");

        void SetHidingAnimation() => _anim.Play("Anim_Cultist_Hiding");

        public void SetCanMove() =>_canMove = true;
    }
}

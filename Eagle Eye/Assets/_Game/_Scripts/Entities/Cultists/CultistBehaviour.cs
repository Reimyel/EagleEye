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

        Vector3 _endingPos = new Vector3(-88.9f, 3.15063f, 35.15703f);
        bool _canMove = false;

        void Start() => SetWalkAnimation();

        void Update() => Move();

        void Move() 
        {
            if (!_canMove) return;
            
            gameObject.transform.position = Vector3.MoveTowards(transform.position, _endingPos, Speed * Time.deltaTime);

            float _distance = Vector2.Distance(transform.position, _endingPos);
            if (_distance < 0.1f)
                 Destroy(gameObject);
        }

        void SetWalkAnimation() => _anim.Play("Anim_Cultist_Walk");

        public void SetCanMove() =>_canMove = true;
    }
}

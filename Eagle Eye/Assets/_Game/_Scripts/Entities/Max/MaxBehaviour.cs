using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FourZeroFourStudios
{
    public class MaxBehaviour : MonoBehaviour
    {
        [SerializeField] TriggerDialogue _triggerDialogue;
        [SerializeField] GameObject _workers;
        Vector3 _endingPos = new Vector3(-28.95579f, 3.15063f, 6.15123f);
        bool _canMove = false;
        public float Speed;

        private void Update()
        {
            if (_canMove)
            {
                gameObject.transform.position = Vector3.MoveTowards(transform.position, _endingPos, Speed * Time.deltaTime);

                float _distance = Vector2.Distance(transform.position, _endingPos);
                if (_distance < 0.1f)
                {
                    Destroy(gameObject);
                }
            }
        }

        public void StartSequence()
        {
            Destroy(_workers);
            _triggerDialogue.enabled = true;
            transform.position = new Vector3(-10.87671f, 3.15063f, 6.15729f);
        }

        public void SetCanMove()
        {
            _canMove = true;
        }
    }
}

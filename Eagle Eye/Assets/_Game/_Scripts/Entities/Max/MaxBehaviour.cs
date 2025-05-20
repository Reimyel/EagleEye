using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FourZeroFourStudios
{
    public class MaxBehaviour : MonoBehaviour
    {
        [SerializeField] TriggerDialogue _triggerDialogue;
        Vector3 _endingPos = new Vector3(-28.95579f, 3.15063f, 6.15123f);
        bool _canMove = false;
        public float Speed;

        private void Update()
        {
            if (_canMove)
            {
                //not working
                gameObject.transform.position = Vector3.MoveTowards(transform.position, _endingPos, Speed * Time.deltaTime);
            }
        }

        public void StartSequence()
        {
            _triggerDialogue.enabled = true;
            transform.position = new Vector3(-9.551909f, 3.15063f, 6.15123f);
        }

        public void SetMove()
        {
            _canMove = true;
        }
    }
}

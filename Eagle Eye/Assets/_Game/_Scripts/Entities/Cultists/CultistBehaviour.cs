using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FourZeroFourStudios
{
    public class CultistBehaviour : MonoBehaviour
    {
        Vector3 _endingPos = new Vector3(-74.26485f, 3.15063f, 35.15703f);
        bool _canMove = false;
        public float Speed;

        private void Start()
        {
            this.gameObject.SetActive(false);
        }

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

        public void SetCanMove()
        {
            this.gameObject.SetActive(true);
            _canMove = true;
        }
    }
}

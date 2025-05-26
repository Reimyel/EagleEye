using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FourZeroFourStudios
{
    public class CultistHideTrigger : MonoBehaviour
    {
        [SerializeField] CultistBehaviour _cultistBehaviour;

        private void OnTriggerEnter(Collider other)
        {
            _cultistBehaviour.Vanish();
            Destroy(gameObject);
        }
    }
}

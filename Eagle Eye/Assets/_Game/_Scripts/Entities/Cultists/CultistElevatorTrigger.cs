using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FourZeroFourStudios
{
    public class CultistElevatorTrigger : MonoBehaviour
    {
        [SerializeField] CultistBehaviour _cultistBehaviour;

        private void OnTriggerEnter(Collider other)
        {
            _cultistBehaviour.SetCanMove();
        }
    }
}

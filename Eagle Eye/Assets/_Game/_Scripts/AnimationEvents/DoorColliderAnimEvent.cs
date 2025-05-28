using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FourZeroFourStudios
{
    public class NewBehaviourScript : MonoBehaviour
    {
        [SerializeField] Collider _doorCollider;

        public void DisableDoorCollider()
        {
            _doorCollider.enabled = false;
        }

        public void EnableDoorCollider()
        {
            _doorCollider.enabled = true;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FourZeroFourStudios
{
    public class Elevator : MonoBehaviour
    {
        [SerializeField] Animator _doorAnimator = null;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                OpenElevatorDoors();
            }
            if (Input.GetKeyDown(KeyCode.R))
            {
                CloseElevatorDoors();
            }
        }

        public void OpenElevatorDoors()
        {
            _doorAnimator.Play("Anim_Elevator_Open");
        }

        public void CloseElevatorDoors()
        {
            _doorAnimator.Play("Anim_Elevator_Close");
        }
    }
}

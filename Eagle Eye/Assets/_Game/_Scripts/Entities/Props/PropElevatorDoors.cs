using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FourZeroFourStudios
{
    public class PropElevatorDoors : MonoBehaviour
    {
        [Header("References:")]
        [SerializeField] Animator _doorAnimator = null;

        private void OnTriggerEnter(Collider other)
        {
            CloseElevatorDoors();
            gameObject.SetActive(false);
        }

        public void OpenElevatorDoors()
        {
            _doorAnimator.Play("Anim_Elevator_Open");
        }

        public void CloseElevatorDoors()
        {
            _doorAnimator.Play("Anim_Elevator_Close");
            Debug.Log("bolas");

        }
    }
}

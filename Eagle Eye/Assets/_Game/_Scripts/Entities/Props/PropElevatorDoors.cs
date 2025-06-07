using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FourZeroFourStudios
{
    public class PropElevatorDoors : MonoBehaviour
    {
        [Header("References:")]
        [SerializeField] Animator _doorAnimator = null;
        [SerializeField] EntitySFXController _sfxController;

        private void OnTriggerEnter(Collider other)
        {
            CloseElevatorDoors();
            gameObject.SetActive(false);
        }

        public void OpenElevatorDoors()
        {
            Invoke("OpenAnim", 2.22f);
            _sfxController.Play("Open");
        }

        public void CloseElevatorDoors()
        {
            _doorAnimator.Play("Anim_Elevator_Close");
        }

        void OpenAnim() => _doorAnimator.Play("Anim_Elevator_Open");
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FourZeroFourStudios
{
    public class HeroPropMaxLocker : BaseHeroProp
    {
        [Header("References:")]

        [Header("Audio:")]
        [SerializeField] EntitySFXController _sfxController;

        [Header("Hierarchy:")]
        [SerializeField] Animator _anim_doorOrigin;
        bool _lockerDoorOpen = false;

        public override void Interact()
        {
            base.Interact();

            if (!_lockerDoorOpen)
            {
                OpenLockerDoor();
                _sfxController.Play("Open");

                this.enabled = false;
            }
            else 
            {
                CloseLockerDoor();
                _sfxController.Play("Close");

                this.enabled = false;
            }
        }

        public void OpenLockerDoor()
        {
            _anim_doorOrigin.Play("Anim_HeroProp_Locker_Open");
            _lockerDoorOpen = true;
        }

        public void CloseLockerDoor()
        {
            _anim_doorOrigin.Play("Anim_HeroProp_Locker_Close");
            _lockerDoorOpen = false;
        }
    }
}

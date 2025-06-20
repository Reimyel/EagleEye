using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FourZeroFourStudios
{
    public class HeroPropBathroomDoor : BaseHeroProp
    {
        [Header("References:")]
        [SerializeField] EntitySFXController _sfxController;
        [SerializeField] Animator _bathroomDoorAnimator = null;
        public bool DoorLocked = false;
        bool _bathroomDoorOpen = false;

        public override void Interact()
        {
            base.Interact();

            if (!_bathroomDoorOpen && !DoorLocked)
            {
                OpenBathroomDoor();
            }
            else if (!DoorLocked)
            {
                CloseBathroomDoor();
            }
        }

        public void OpenBathroomDoor()
        {
            _bathroomDoorAnimator.Play("Anim_Bathroom_Door_Open");
            _sfxController.Play("Open");
            _bathroomDoorOpen = true;
        }

        public void CloseBathroomDoor()
        {
            _bathroomDoorAnimator.Play("Anim_Bathroom_Door_Close");
            _sfxController.Play("Close");
            _bathroomDoorOpen = false;
        }
    }
}

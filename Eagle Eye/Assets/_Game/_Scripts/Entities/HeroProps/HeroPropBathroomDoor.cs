using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FourZeroFourStudios
{
    public class HeroPropBathroomDoor : BaseHeroProp
    {
        [Header("References:")]
        [SerializeField] Animator _bathroomDoorAnimator = null;
        public bool DoorLocked = false;
        bool _bathroomDoorOpen = false;

        public override void Interact()
        {
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
            _bathroomDoorOpen = true;
        }

        public void CloseBathroomDoor()
        {
            _bathroomDoorAnimator.Play("Anim_Bathroom_Door_Close");
            _bathroomDoorOpen = false;
        }
    }
}

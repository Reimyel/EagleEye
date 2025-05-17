using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FourZeroFourStudios
{
    public class HeroPropBathroomDoor : BaseHeroProp
    {
        [Header("References:")]
        [SerializeField] Animator _bathroomDoorAnimator = null;
        bool _bathroomDoorOpen = false;

        public override void Interact()
        {
            if (!_bathroomDoorOpen)
            {
                OpenBathroomDoor();
            }
            else
            {
                CloseBathroomDoor();
            }
        }

        void OpenBathroomDoor()
        {
            _bathroomDoorAnimator.Play("Anim_Bathroom_Door_Open");
            _bathroomDoorOpen = true;
        }

        void CloseBathroomDoor()
        {
            _bathroomDoorAnimator.Play("Anim_Bathroom_Door_Close");
            _bathroomDoorOpen = false;
        }
    }
}

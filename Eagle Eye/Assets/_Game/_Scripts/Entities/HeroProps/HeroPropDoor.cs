using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FourZeroFourStudios
{
    public class HeroPropDoor : BaseHeroProp
    {
        [Header("References:")]
        [SerializeField] Animator _doorAnimator = null;
        bool _doorOpen = false;

        public override void Interact()
        {
            if (!_doorOpen)
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
            _doorAnimator.Play("Anim_Door_Open");
            _doorOpen = true;
        }

        void CloseBathroomDoor()
        {
            _doorAnimator.Play("Anim_Door_Close");
            _doorOpen = false;
        }
    }
}

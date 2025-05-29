using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FourZeroFourStudios
{
    public class HeroPropMaxLocker : BaseHeroProp
    {
        [Header("References:")]
        [SerializeField] Animator _anim_doorOrigin;
        [SerializeField] HeroPropDoorOffice _heroPropDoorOffice;
        [SerializeField] GameObject _waterBottleHandObject;
        [SerializeField] GameObject _propMaxWatterBottle;
        bool _lockerDoorOpen = false;

        public override void Interact()
        {
            base.Interact();

            if (!_lockerDoorOpen)
            {
                OpenLockerDoor();
            }
            else 
            {
                CloseLockerDoor();
                _heroPropDoorOffice.EnableCanOpenIN();
                Destroy(_waterBottleHandObject);
                _propMaxWatterBottle.SetActive(true);

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

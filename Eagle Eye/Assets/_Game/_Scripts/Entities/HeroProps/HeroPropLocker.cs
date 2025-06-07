using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FourZeroFourStudios
{
    public class HeroPropLocker : BaseHeroProp
    {
        [Header("References:")]

        [Header("Audio:")]
        [SerializeField] EntitySFXController _sfxController;

        [Header("Hierarchy:")]
        [SerializeField] Animator _anim_doorOrigin;
        [SerializeField] HeroPropDoorOffice _heroPropDoorOffice;
        [SerializeField] GameObject _connorStuff;
        bool _lockerDoorOpen = true;

        public override void Interact()
        {
            base.Interact();

            if (!_lockerDoorOpen)
            {
                OpenLockerDoor();
                _connorStuff.GetComponent<HeroProp_DialogueOnly>().enabled = true;
                _sfxController.Play("Open");
                StartCoroutine(SetDisableInterval());
            }
            else
            {
                CloseLockerDoor();
                _heroPropDoorOffice.EnableCanOpenIN();
                _connorStuff.SetActive(true);
                _sfxController.Play("Close");
                StartCoroutine(SetDisableInterval());
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

        IEnumerator SetDisableInterval() 
        {
            yield return null;
            this.enabled = false;
        }
    }
}

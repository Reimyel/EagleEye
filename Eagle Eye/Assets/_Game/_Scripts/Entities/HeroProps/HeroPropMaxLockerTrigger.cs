using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FourZeroFourStudios
{
    public class HeroPropMaxLockerTrigger : BaseHeroProp
    {
        [Header("References:")]
        [SerializeField] HeroPropDoorOffice _heroPropDoorOffice;
        [SerializeField] GameObject _waterBottleHandObject;
        [SerializeField] GameObject _propMaxWatterBottle;

        [SerializeField] HeroPropMaxLocker _heroPropMaxLocker;
        [SerializeField] EntitySFXController _sfxController;

        public override void Interact()
        {
            base.Interact();

            _heroPropDoorOffice.EnableCanOpenIN();
            Destroy(_waterBottleHandObject);
            _propMaxWatterBottle.SetActive(true);

            _heroPropMaxLocker.CloseLockerDoor();
            _sfxController.Play("Close");

            this.enabled = false;
        }
    }
}

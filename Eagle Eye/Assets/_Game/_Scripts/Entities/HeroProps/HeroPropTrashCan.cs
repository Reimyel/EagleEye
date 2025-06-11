using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FourZeroFourStudios
{
    public class HeroPropTrashCan : BaseHeroProp
    {
        [Header("References:")]
        [SerializeField] PropCoffeeCup _propCoffeeCup;
        [SerializeField] CultistBehaviour _cultistBehaviour;
        [SerializeField] GameObject _cultistHideTrigger;
        [SerializeField] HeroPropDoorOffice _heroPropDoorOffice;
        [SerializeField] EntitySFXController _sfxController;

        private void Start()
        {
            this.enabled = false;
        }

        public override void Interact()
        {
            base.Interact();

            _sfxController.Play("Deposit");
            Destroy(_propCoffeeCup.gameObject);
            _cultistHideTrigger.SetActive(true);

            _cultistBehaviour.gameObject.SetActive(true);
            _cultistBehaviour.Hide();

            _heroPropDoorOffice.EnableCanOpenIN();

            this.enabled = false;
        }
    }
}

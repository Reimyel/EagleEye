using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FourZeroFourStudios
{
    public class HeroPropVendingMachine : BaseHeroProp
    {
        [Header("References:")]
        [SerializeField] private TriggerDialogue _triggerDialogue;
        [SerializeField] GameObject _coffeeCup;
        [SerializeField] bool _canTakeCoffee = false;

        public override void Interact()
        {
            base.Interact();

            if (_canTakeCoffee)
            {
                _coffeeCup.SetActive(true);
                _canTakeCoffee = false;
                this.enabled = false;
            }
            else
            {
                _triggerDialogue.enabled = true;
            }
        }

        public void SetCanTakeCoffee()
        {
            _canTakeCoffee = true;
        }
    }
}

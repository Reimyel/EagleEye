using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FourZeroFourStudios
{
    public class HeroPropVendingMachine : BaseHeroProp
    {
        [Header("References:")]
        [SerializeField] EntitySFXController _sfxController;
        [SerializeField] private TriggerDialogue _triggerDialogue;
        [SerializeField] GameObject _coffeeCup;
        [SerializeField] bool _canTakeCoffee = false;
        [SerializeField] float _takeCoffeeDelay;

        public override void Interact()
        {
            base.Interact();

            if (_canTakeCoffee)
            {
                _sfxController.Play("Take");
                StartCoroutine(GiveCoffee());
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

        IEnumerator GiveCoffee()
        {
            yield return new WaitForSeconds(_takeCoffeeDelay);
            _coffeeCup.SetActive(true);
        }
    }
}

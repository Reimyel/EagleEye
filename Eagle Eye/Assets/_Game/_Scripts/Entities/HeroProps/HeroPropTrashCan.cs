using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FourZeroFourStudios
{
    public class HeroPropTrashCan : BaseHeroProp
    {
        [SerializeField] PropCoffeeCup _propCoffeeCup;
        [SerializeField] CultistBehaviour _cultistBehaviour;
        [SerializeField] GameObject _cultistHideTrigger;

        private void Start()
        {
            this.enabled = false;
        }

        public override void Interact()
        {
            base.Interact();

            Destroy(_propCoffeeCup.gameObject);
            _cultistHideTrigger.SetActive(true);

            _cultistBehaviour.gameObject.SetActive(true);
            _cultistBehaviour.Hide();

            this.enabled = false;
        }
    }
}

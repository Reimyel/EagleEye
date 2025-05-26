using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FourZeroFourStudios
{
    public class HeroPropTrashCan : BaseHeroProp
    {
        [SerializeField] PropCoffeeCup _propCoffeeCup;

        private void Start()
        {
            this.enabled = false;
        }

        public override void Interact()
        {
            base.Interact();

            Destroy(_propCoffeeCup.gameObject);
            this.enabled = false;
        }
    }
}

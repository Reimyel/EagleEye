using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FourZeroFourStudios
{
    public class HeroPropMaxWaterBottle : BaseHeroProp
    {
        [SerializeField] GameObject _waterBottleHandObject;

        public override void Interact()
        {
            base.Interact();

            _waterBottleHandObject.SetActive(true);

            Destroy(gameObject);
        }
    }
}

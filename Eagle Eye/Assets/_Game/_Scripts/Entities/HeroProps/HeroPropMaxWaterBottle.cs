using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FourZeroFourStudios
{
    public class HeroPropMaxWaterBottle : BaseHeroProp
    {
        [SerializeField] GameObject _waterBottleHandObject;
        [SerializeField] EntitySFXController _sfxController;

        public override void Interact()
        {
            base.Interact();

            _waterBottleHandObject.SetActive(true);
            _sfxController.Play("Catch");

            Destroy(gameObject);
        }
    }
}

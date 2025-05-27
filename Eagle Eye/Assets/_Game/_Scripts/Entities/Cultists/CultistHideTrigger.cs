using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FourZeroFourStudios
{
    public class CultistHideTrigger : MonoBehaviour
    {
        [SerializeField] CultistBehaviour _cultistBehaviour;
        HeroPropChairOffice _heroPropChairOffice;

        private void Start()
        {
            _heroPropChairOffice = FindObjectOfType<HeroPropChairOffice>();
        }

        private void OnTriggerEnter(Collider other)
        {
            _cultistBehaviour.Vanish();

            _heroPropChairOffice.enabled = true;

            Destroy(gameObject);
        }
    }
}

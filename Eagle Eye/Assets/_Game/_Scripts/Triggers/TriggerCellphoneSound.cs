using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FourZeroFourStudios
{
    public class TriggerCellphoneSound : MonoBehaviour
    {
        [SerializeField] EntitySFXController _sfxController;

        private void OnTriggerEnter(Collider other)
        {
            _sfxController.CreateLoop("Cellphone ringing name");
            Destroy(gameObject);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FourZeroFourStudios
{
    public class TriggerEnterOffice : MonoBehaviour
    {
        [SerializeField] GameObject _penDrive;

        private void OnTriggerEnter(Collider other)
        {
            _penDrive.SetActive(true);
        }
    }
}

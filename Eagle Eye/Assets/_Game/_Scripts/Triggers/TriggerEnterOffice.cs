using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FourZeroFourStudios
{
    public class TriggerEnterOffice : MonoBehaviour
    {
        [SerializeField] GameObject _penDrive;
        NarrativeManager _narrativeManager;

        private void Start()
        {
            _narrativeManager = FindObjectOfType<NarrativeManager>();
        }

        private void OnTriggerEnter(Collider other)
        {
            _penDrive.SetActive(true);
            _narrativeManager.Progress();
            Destroy(gameObject);
        }
    }
}

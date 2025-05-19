using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FourZeroFourStudios
{
    public class MaxBehaviour : MonoBehaviour
    {
        [SerializeField] TriggerDialogue _triggerDialogue;

        public void StartSequence()
        {
            _triggerDialogue.enabled = true;
            transform.position = new Vector3(-9.551909f, 3.15063f, 6.15123f);
        }
    }
}

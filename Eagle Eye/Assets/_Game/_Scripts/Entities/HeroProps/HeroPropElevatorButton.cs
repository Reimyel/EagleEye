using Newtonsoft.Json.Bson;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FourZeroFourStudios
{
    public class HeroPropElevatorButton : BaseHeroProp
    {
        [Header("References:")]
        [SerializeField] private TriggerDialogue _triggerDialogue;
        [SerializeField] private TriggerDialogue _triggerDialogue2;
        public int ElevatorButtonTrigger = 1;

        public override void Interact()
        {
            switch (ElevatorButtonTrigger)
            {
                case 1:
                    _triggerDialogue.enabled = true;
                    break;
                case 2:
                    _triggerDialogue2.enabled = true;
                    break;
            }
        }
    }
}

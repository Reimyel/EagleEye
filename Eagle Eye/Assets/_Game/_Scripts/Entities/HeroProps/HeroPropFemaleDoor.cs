using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FourZeroFourStudios
{
    public class HeroPropFemaleDoor : BaseHeroProp
    {
        [Header("References:")]
        [SerializeField] private TriggerDialogue _triggerDialogue;

        public override void Interact() => _triggerDialogue.enabled = true;
    }
}

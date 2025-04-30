using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FourZeroFourStudios
{
    public class HeroPropLocker : BaseHeroProp
    {
        [Header("References:")]
        [SerializeField] Animator _anim_doorOrigin;
        [SerializeField] HeroPropDoorOffice _heroPropDoorOffice;
        
        public override void Interact()
        {
            base.Interact();

            _anim_doorOrigin.enabled = true;
            _heroPropDoorOffice.ActivateCanOpen();

            this.enabled = false;
        }
    }
}

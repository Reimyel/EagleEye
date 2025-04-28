using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FourZeroFourStudios
{
    public class HeroPropTest : BaseHeroProp
    {
        public override void Interact()
        {
            base.Interact();
            Destroy(gameObject);
        }
    }
}

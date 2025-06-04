using System;
using System.Collections;
using System.Collections.Generic;
using FMODUnity;
using UnityEngine;

namespace FourZeroFourStudios
{
    public class EntitySFXController : MonoBehaviour
    {
        [Header("Sound Effects:")]
        [SerializeField] List<SFX> _list_sfxs;

        public void PlayAudios(string nameValue)
        {
            if (string.IsNullOrEmpty(nameValue)) return;

            SFX sfxLocal = _list_sfxs.Find(x => x.Name.Equals(nameValue));

            if (sfxLocal == null) return;

            AudioManager.Instance.Play_OneShotAtPosition(sfxLocal.EventRef, transform.position);

        }
    }

    [Serializable]
    public class SFX
    {
        public string Name;
        public EventReference EventRef;
    }
}

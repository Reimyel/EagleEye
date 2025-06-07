using System;
using System.Collections.Generic;
using FMOD.Studio;
using FMODUnity;
using UnityEngine;

namespace FourZeroFourStudios
{
    /// <summary>
    /// General purpose SFX Controller for Entities in Gameplay
    /// </summary>
    public class EntitySFXController : MonoBehaviour
    {
        // Inspector:
        [Header("Sound Effects:")]
        [SerializeField] List<SFX> _list_sfxs;

        List<EventInstance> _events;

        public void Play(string nameValue)
        {
            if (string.IsNullOrEmpty(nameValue)) return;

            SFX sfxLocal = _list_sfxs.Find(x => x.Name.Equals(nameValue));

            if (sfxLocal == null) return;

            AudioManager.Instance.Play_OneShotAtPosition(sfxLocal.EventRef, transform.position);

        }

        public EventInstance CreateLoop(string nameValue) 
        {
            if (string.IsNullOrEmpty(nameValue)) return new EventInstance();

            SFX sfxLocal = _list_sfxs.Find(x => x.Name.Equals(nameValue));

            if (sfxLocal == null) return new EventInstance();

            return AudioManager.Instance.Create_EventInstance(sfxLocal.EventRef);
        }

    }

    [Serializable]
    public class SFX
    {
        public string Name;
        public EventReference EventRef;
    }
}

using FMOD.Studio;
using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FourZeroFourStudios
{
    /// <summary>
    /// Responsible for executing audio events and configuring FMOD parameters
    ///
    /// SFXs 1 OneShot => Only simple and immediate audios that do not have parameters
    ///
    /// EventInstances => Audios that can loop, have variation, etc. (their parameters are changed locally through the created instance)
    ///
    /// Play any type of music through Play_Music (parameters can be passed via Set_MusicParameter)
    ///
    /// Play any type of ambience through Play_Ambience (parameters can be passed via Set_AmbienceParameter)
    ///
    /// Play any Event Instances using Play_EventInstance / Play_EventInstanceAtPosition
    /// Stop any Event Instance with Stop_EventInstance (can use fade)
    /// Remove any Event Instance from AudioManager's storage with Delete_EventInstance
    /// </summary>
    public class AudioManager : MonoBehaviour
    {
        #region Members
        public static AudioManager Instance { get; private set; }

        // FMOD Mixers
        Bus _bus_master;
        Bus _bus_music;
        Bus _bus_sfx;
        Bus _bus_ambience;

        // Initial Volume
        public static float DefaultVolume_master { get; private set; }
        public static float DefaultVolume_music { get; private set; }
        public static float DefaultVolume_sfx { get; private set; }
        public static float DefaultVolume_ambience { get; private set; }

        static bool _isDefaultSet;

        // Stores all created EventInstances so they can be cleared from memory at the end to avoid possible leaks
        List<EventInstance> _eventsInstances;
        #endregion

        #region Unity
        void Awake() => Setup();

        void Setup()
        {
            Instance = this;

            Get_Buses();

            if (!_isDefaultSet)
            {
                StartCoroutine(Set_Default_Volumes());
                _isDefaultSet = false;
            }

            _eventsInstances = new List<EventInstance>();
        }

        void OnDestroy() => CleanEvents();
        #endregion

        #region Mixers / Bus
        void Get_Buses()
        {
            _bus_master = FMODUnity.RuntimeManager.GetBus("bus:/");
            _bus_music = FMODUnity.RuntimeManager.GetBus("bus:/Music");
            _bus_sfx = FMODUnity.RuntimeManager.GetBus("bus:/SFX");
            _bus_ambience = FMODUnity.RuntimeManager.GetBus("bus:/Ambience");

            DefaultVolume_master = Get_Bus_Volume(_bus_master);
            DefaultVolume_music = Get_Bus_Volume(_bus_music);
            DefaultVolume_sfx = Get_Bus_Volume(_bus_sfx);
            DefaultVolume_ambience = Get_Bus_Volume(_bus_ambience);
        }

        IEnumerator Set_Default_Volumes()
        {
            yield return new WaitForEndOfFrame();
            //SettingsManager.Instance.AudioSettings_Reset();
        }

        float Get_Bus_Volume(Bus value)
        {
            float curVolumeLocal;

            value.getVolume(out curVolumeLocal);

            return curVolumeLocal;
        }

        /*
        public void Set_Bus_Volume(SettingsManager.AudioSettings settingsValue, float value)
        {
            float newVolumeLocal = Mathf.Clamp(value, 0.0f, 1.0f);

            switch (settingsValue)
            {
                case SettingsManager.AudioSettings.Master:
                    _bus_master.setVolume(newVolumeLocal);
                    break;

                case SettingsManager.AudioSettings.Music:
                    _bus_music.setVolume(newVolumeLocal);
                    break;

                case SettingsManager.AudioSettings.SFX:
                    _bus_sfx.setVolume(newVolumeLocal);
                    break;

                case SettingsManager.AudioSettings.Ambience:
                    _bus_ambience.setVolume(newVolumeLocal);
                    break;
            }
        }
        */
        #endregion

        #region SFXs

        public void Play_OneShotAtPosition(EventReference sfxRefValue, Vector3 posValue)
        {
            RuntimeManager.PlayOneShot(sfxRefValue, posValue);
        }

        public void Play_OneShot(EventReference sfxRefValue)
        {
            RuntimeManager.PlayOneShot(sfxRefValue);
        }
        #endregion

        #region EventInstances
        /// <summary>
        /// Creates an EventInstance for handling sound effects with logic.
        /// </summary>
        /// <param name="refValue">FMOD Reference</param>
        /// <returns></returns>
        public EventInstance Create_EventInstance(EventReference refValue)
        {
            EventInstance newInstanceLocal = FMODUnity.RuntimeManager.CreateInstance(refValue);
            _eventsInstances.Add(newInstanceLocal);
            return newInstanceLocal;
        }

        /// <summary>
        /// Plays the informed EventInstance
        /// </summary>
        /// <param name="instanceValue">Instance</param>
        /// <param name="initialPosValue">Start Position</param>
        public void Play_EventInstance(EventInstance instanceValue, Vector3 initialPosValue)
        {
            instanceValue.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(initialPosValue));
            instanceValue.start();
        }

        /// <summary>
        /// Attaches the EventInstance to the specified GameObject.
        /// </summary>
        /// <param name="instanceValue">Instance</param>
        /// <param name="targetValue">GameObject where it will be attached</param>
        public void Attach_EventInstance(EventInstance instanceValue, GameObject targetValue)
        {
            FMODUnity.RuntimeManager.AttachInstanceToGameObject(instanceValue, targetValue);
        }

        /// <summary>
        /// Updates the EventInstance to the desired location.
        /// </summary>
        /// <param name="instanceValue">Instance</param>
        /// <param name="posValue">Position</param>
        public void Set_EventInstancePosition(EventInstance instanceValue, Vector3 posValue)
        {
            instanceValue.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(posValue));
        }

        /// <summary>
        /// Stops the provided Event Instance.
        /// </summary>
        /// <param name="instanceValue">Instância</param>
        /// <param name="applyFadeValue">Aplica efeito de FadeOut</param>
        public void Stop_EventInstance(EventInstance instanceValue, bool applyFadeValue)
        {
            if (applyFadeValue)
                instanceValue.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
            else
                instanceValue.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        }

        /// <summary>
        /// Removes the Event Instance from the Audio Manager’s storage.
        /// </summary>
        /// <param name="instanceValue">Instância</param>
        public void Delete_EventInstance(EventInstance instanceValue)
        {
            _eventsInstances.Remove(instanceValue);
        }

        public void CleanEvents()
        {
            foreach (EventInstance eventInstanceLocal in _eventsInstances)
            {
                eventInstanceLocal.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
                eventInstanceLocal.release();
            }
        }

        public void Set_GlobalParameter(string nameValue, float value)
        {
            FMOD.Studio.System systemLocal = RuntimeManager.StudioSystem;

            systemLocal.setParameterByName(nameValue, value);
        }
        #endregion
    }
}

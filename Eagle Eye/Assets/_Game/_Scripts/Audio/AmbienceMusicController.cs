using FMOD.Studio;
using FMODUnity;
using UnityEngine;

namespace FourZeroFourStudios
{
    /// <summary>
    /// General purpose Ambience / Music Controller
    /// </summary>
    public class AmbienceMusicController : MonoBehaviour
    {
        [Header("Ambience:")]
        [SerializeField] EventReference _eventRef;

        EventInstance _instance;

        public void Begin(Vector3 initialPosValue) 
        {
            _instance = AudioManager.Instance.Create_EventInstance(_eventRef);
            _instance.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(initialPosValue));
            _instance.start();
        }

        public void UpdatePos(Vector3 posValue) 
        {
            _instance.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(posValue));
        }

        public void Stop(bool selfDestroyValue=false) 
        {
            _instance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);

            if (selfDestroyValue) 
            {
                AudioManager.Instance.Delete_EventInstance(_instance);
                Destroy(this);
            }
        }
    }
}

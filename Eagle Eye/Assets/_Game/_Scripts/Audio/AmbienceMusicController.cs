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

        // Instância de Evento que será criada e manipulada
        EventInstance _instance;

        public void Begin(Vector3 initialPosValue)
        {
            _instance = AudioManager.Instance.Create_EventInstance(_eventRef);
            AudioManager.Instance?.Play_EventInstance(_instance, initialPosValue);
        }

        public void BeginAttached(GameObject targetValue) 
        {
            _instance = AudioManager.Instance.Create_EventInstance(_eventRef);
            AudioManager.Instance?.Attach_EventInstance(_instance, targetValue);
            AudioManager.Instance?.Play_EventInstance(_instance, Vector3.zero);
        }

        public void UpdatePos(Vector3 posValue)
        {
            AudioManager.Instance?.Set_EventInstancePosition(_instance, posValue);
        }

        public void Stop(bool selfDestroyValue = false)
        {
            AudioManager.Instance?.Stop_EventInstance(_instance, true);

            if (selfDestroyValue)
            {
                AudioManager.Instance?.Delete_EventInstance(_instance);
                Destroy(this);
            }
        }
    }
}

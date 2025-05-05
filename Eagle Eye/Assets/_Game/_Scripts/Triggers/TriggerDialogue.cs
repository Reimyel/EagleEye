using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace FourZeroFourStudios
{
    public class TriggerDialogue : MonoBehaviour
    {
        // Inspector:
        [Header("Settings:")]
        [Space]

        [Header("References:")]
        [SerializeField] ScriptableDialogueSequence _dialogue;
        [Space]

        [Header("Parameters:")]
        [SerializeField] DestroyMode _destroyAfterShow = DestroyMode.No;

        [Header("Event:")]
        public UnityEvent AfterDialogueEvent = new UnityEvent();

        enum DestroyMode 
        {
            No,
            GameObject,
            Component
        }

        void OnEnable() => Raycaster.OnRaycast += CheckTrigger;

        void OnDisable() => Raycaster.OnRaycast -= CheckTrigger;

        void CheckTrigger(GameObject gameObjectValue, TextMeshProUGUI tmpValue) 
        {
            if (gameObjectValue != gameObject || HudDialogueManager.Instance.IsCurrentSequence(_dialogue)) return;

            HudDialogueManager.Instance.StartDialogue(_dialogue, AfterDialogueEvent, true);

            if (_destroyAfterShow == DestroyMode.GameObject)
                Destroy(gameObject);
            else if (_destroyAfterShow == DestroyMode.Component)
                this.enabled = false;
        }
    }
}

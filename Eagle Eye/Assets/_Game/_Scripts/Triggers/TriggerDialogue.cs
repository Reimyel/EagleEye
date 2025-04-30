using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

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

            HudDialogueManager.Instance.StartDialogue(_dialogue);

            if (_destroyAfterShow == DestroyMode.GameObject)
                Destroy(gameObjectValue);
            else if (_destroyAfterShow == DestroyMode.Component)
                this.enabled = false;
        }
    }
}

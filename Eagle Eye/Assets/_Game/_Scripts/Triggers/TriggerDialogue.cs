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
        [SerializeField] bool _destroyAfterShow;

        void Start() => Raycaster.OnRaycast += CheckTrigger;

        void OnDestroy() => Raycaster.OnRaycast -= CheckTrigger;

        void CheckTrigger(GameObject gameObjectValue, TextMeshProUGUI tmpValue) 
        {
            if (gameObjectValue != gameObject || HudDialogueManager.Instance.IsCurrentSequence(_dialogue)) return;

            HudDialogueManager.Instance.StartDialogue(_dialogue);

            if (_destroyAfterShow)
                Destroy(gameObjectValue);
        }
    }
}

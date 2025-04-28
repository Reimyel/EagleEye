using System.Collections;
using System.Collections.Generic;
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

        void Start() => Raycaster.OnRaycast += CheckTrigger;

        void OnDestroy() => Raycaster.OnRaycast -= CheckTrigger;

        void CheckTrigger(GameObject gameObjectValue) 
        {
            if (gameObjectValue != gameObject || HudDialogueManager.Instance.IsCurrentSequence(_dialogue)) return;

            HudDialogueManager.Instance.StartDialogue(_dialogue);

            Destroy(gameObjectValue);
        }
    }
}

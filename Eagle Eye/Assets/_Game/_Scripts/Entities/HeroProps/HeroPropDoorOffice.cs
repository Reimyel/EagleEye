using UnityEngine;

namespace FourZeroFourStudios
{
    public class HeroPropDoorOffice : BaseHeroProp
    {
        [Header("Settings:")]
        [Space]

        [Header("References:")]
        [SerializeField] TriggerDialogue[] _triggerBlockDialogues;
        
        int _curTriggerIndex = 0;
        bool _canOpen = false;

        public override void Interact()
        {
            base.Interact();

            if (_canOpen) OpenDoor();
            else ShowBlockDialogue();
        }

        public void ActivateCanOpen() => _canOpen = true;
        
        void OpenDoor() 
        {
            if (_curTriggerIndex < _triggerBlockDialogues.Length - 1)
                _curTriggerIndex++;
        
            // TODO: Open Animation
        }

        void ShowBlockDialogue() => _triggerBlockDialogues[_curTriggerIndex].enabled = true;
    }
}

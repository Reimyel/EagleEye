using UnityEngine;

namespace FourZeroFourStudios
{
    public class HeroPropDoorOffice : BaseHeroProp
    {
        #region Members
        // Inspector
        [Header("Settings:")]
        [Space]

        [Header("References:")]
        [SerializeField] TriggerDialogue[] _trigger_blockDialogues;
        [SerializeField] TriggerRotateDoor _trigger_rotateDoor;
        [SerializeField] TriggerDisableRotateDoor[] _triggers_disableRotateDoor;
        [SerializeField] GameObject _go_light_on;
        [SerializeField] GameObject _go_light_off;

        int _curTriggerIndex = 0;
        bool _canOpen = false;

        DisableDoor _disableDoor;

        public enum DisableDoor 
        {
            IN = 0,
            OUT = 1
        }
        #endregion

        public override void Interact()
        {
            base.Interact();

            if (_canOpen) OpenDoor();
            else ShowBlockDialogue();
        }

        public void EnableCanOpen(DisableDoor disableDoorValue)
        {
            _canOpen = true;

            _disableDoor = disableDoorValue;
        }

        public void DisableCanOpen() 
        {
            _canOpen = false;

            _trigger_rotateDoor.enabled = false;

            _go_light_off.SetActive(true);
            _go_light_on.SetActive(false);
        }

        void OpenDoor() 
        {
            if (_curTriggerIndex < _trigger_blockDialogues.Length - 1)
                _curTriggerIndex++;

            _trigger_rotateDoor.enabled = true;

            _go_light_off.SetActive(false);
            _go_light_on.SetActive(true);

            _triggers_disableRotateDoor[(int)_disableDoor].gameObject.SetActive(true);
        }

        void ShowBlockDialogue() => _trigger_blockDialogues[_curTriggerIndex].enabled = true;
    }
}

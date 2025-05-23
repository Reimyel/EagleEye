using UnityEngine;

namespace FourZeroFourStudios
{
    public class HeroPropDoorOffice : BaseHeroProp
    {
        #region Members
        // Inspector
        [Header("Settings:")]
        [Space]

        [Header("Reset Door Rotation:")]
        [SerializeField] float _resetSpeed;

        [Header("References:")]
        [SerializeField] Transform _transf_door;
        [SerializeField] TriggerDialogue[] _trigger_blockDialogues;
        [SerializeField] TriggerRotateDoor _trigger_rotateDoor;
        [SerializeField] TriggerDisableRotateDoor[] _triggers_disableRotateDoor;
        [SerializeField] GameObject _go_light_on;
        [SerializeField] GameObject _go_light_off;

        int _curTriggerIndex = 0;
        bool _canOpen = false;

        bool _resetRotation = false;

        float _defaultRotationY;

        DisableDoor _disableDoor;

        public enum DisableDoor 
        {
            IN = 0,
            OUT = 1
        }
        #endregion

        #region Mono
        void Start() => _defaultRotationY = _transf_door.rotation.y;


        void Update() 
        {
            if (_resetRotation) ApplyRotationReset();
        }
        #endregion

        #region Custom
        void ApplyRotationReset() 
        {
            if (_transf_door.rotation.y != _defaultRotationY) 
            {
                float rotationDirLocal = 0;

                if (_transf_door.rotation.y > _defaultRotationY)
                    rotationDirLocal = -1;
                else if (_transf_door.rotation.y < _defaultRotationY)
                    rotationDirLocal = 1;

                _transf_door.rotation *= Quaternion.Euler(0f, rotationDirLocal * _resetSpeed * Time.deltaTime, 0f);
            }
            else 
            {
                _transf_door.rotation = Quaternion.Euler(0f, _defaultRotationY, 0f);
                _resetRotation = false;
            }
        }

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

        //when used in trigger dialogue
        public void TriggerDialogue_EnableCanOpenIN()
        {
            EnableCanOpen(DisableDoor.IN);
        }
        public void TriggerDialogue_EnableCanOpenOUT()
        {
            EnableCanOpen(DisableDoor.OUT);
        }

        public void DisableCanOpen() 
        {
            _canOpen = false;

            _trigger_rotateDoor.enabled = false;

            _resetRotation = true;

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
        #endregion
    }
}

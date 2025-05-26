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
        [SerializeField] TriggerRotateDoor[] _trigger_rotateDoors;
        [SerializeField] TriggerDisableRotateDoor[] _triggers_disableRotateDoor;
        [SerializeField] GameObject _go_light_on;
        [SerializeField] GameObject _go_light_off;

        int _curTriggerIndex = 0;
        bool _canOpen = false;

        float _defaultRotationY;
        bool _resetRotation = false;
        bool _flippedRotation = false;

        DisableDoor _disableDoor;

        public enum DisableDoor 
        {
            IN = 0,
            OUT = 1
        }
        #endregion

        #region Mono
        void Start() => _defaultRotationY = _transf_door.localEulerAngles.y;

        void Update() 
        {
            if (_resetRotation) ApplyRotationReset();
        }
        #endregion

        #region Custom
        void ApplyRotationReset() 
        {
            float curYLocal = _transf_door.localEulerAngles.y;

            float newYLocal = Mathf.MoveTowards(curYLocal, _defaultRotationY,  _resetSpeed * Time.deltaTime);
            _transf_door.localRotation = Quaternion.Euler(0f, newYLocal, 0f);

            if (Mathf.Approximately(newYLocal, _defaultRotationY)) 
                _resetRotation = false;
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
        public void TriggerDialogue_EnableCanOpenIN() => EnableCanOpen(DisableDoor.IN);
        public void TriggerDialogue_EnableCanOpenOUT() => EnableCanOpen(DisableDoor.OUT);

        public void DisableCanOpen() 
        {
            _canOpen = false;

            for (int i = 0; i < _trigger_rotateDoors.Length; i++)
                _trigger_rotateDoors[i].enabled = false;

            _resetRotation = true;

            _go_light_off.SetActive(true);
            _go_light_on.SetActive(false);
        }

        void OpenDoor() 
        {
            if (_curTriggerIndex < _trigger_blockDialogues.Length - 1)
                _curTriggerIndex++;

            for (int i = 0; i < _trigger_rotateDoors.Length; i++)
                _trigger_rotateDoors[i].enabled = true;

            _go_light_off.SetActive(false);
            _go_light_on.SetActive(true);

            _triggers_disableRotateDoor[(int)_disableDoor].gameObject.SetActive(true);
        }

        void ShowBlockDialogue() => _trigger_blockDialogues[_curTriggerIndex].enabled = true;
        #endregion
    }
}

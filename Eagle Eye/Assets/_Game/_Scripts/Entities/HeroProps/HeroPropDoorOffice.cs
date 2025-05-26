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
        [SerializeField] Collider _collider_blocker;
        [SerializeField] TriggerDisableRotateDoor _trigger_disableRotateDoor;
        [SerializeField] GameObject _go_light_on;
        [SerializeField] GameObject _go_light_off;

        int _curTriggerIndex = 0;
        bool _canOpen = false;

        float _defaultRotationY;
        bool _resetRotation = false;
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

        public void EnableCanOpen() => _canOpen = true;

        // when used in trigger dialogue
        public void TriggerDialogue_EnableCanOpenIN()
        {
            GameObject.FindGameObjectWithTag("Entrance").GetComponent<HeroPropDoorOffice>().EnableCanOpen();
        }
        
        public void TriggerDialogue_EnableCanOpenOUT() 
        {
            GameObject.FindGameObjectWithTag("Exit").GetComponent<HeroPropDoorOffice>().EnableCanOpen();
        }

        public void DisableCanOpen() 
        {
            _canOpen = false;

            _resetRotation = true;

            _go_light_off.SetActive(true);
            _go_light_on.SetActive(false);

            _trigger_disableRotateDoor.gameObject.SetActive(false);
            _trigger_rotateDoor.enabled = false;
            _collider_blocker.enabled = true;
        }

        void OpenDoor() 
        {
            if (_curTriggerIndex < _trigger_blockDialogues.Length - 1)
                _curTriggerIndex++;

            _go_light_off.SetActive(false);
            _go_light_on.SetActive(true);

            _trigger_disableRotateDoor.gameObject.SetActive(true);
            _trigger_rotateDoor.enabled = true;
            _collider_blocker.enabled = false;
        }

        void ShowBlockDialogue()
        {
            if (_trigger_blockDialogues[_curTriggerIndex] != null)
                _trigger_blockDialogues[_curTriggerIndex].enabled = true;
        }
        #endregion
    }
}

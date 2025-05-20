using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace FourZeroFourStudios
{
    public class HudDialogueManager : MonoBehaviour
    {
        #region Members
        // Singleton
        public static HudDialogueManager Instance;

        #region Inspector
        [Header("Settings:")]
        [Space]
        
        [Header("References:")]
        [SerializeField] CanvasGroup _cg;
        [SerializeField] TextMeshProUGUI _tmp_dialogue;
        [SerializeField] PlayerMovement _playerMove;
        [SerializeField] CameraHeadBob _cameraHeadBob;
        [Space]

        [Header("Debug:")]
        [SerializeField] ScriptableDialogueSequence _curSequence;
        [SerializeField] UnityEvent _curEvent;
        [SerializeField] ScriptableDialogueSequence _testDialogue;
        #endregion

        int _curIndex;
        [HideInInspector] public bool IsTrigger = false;
        #endregion

        #region Mono
        void Awake() => Instance = this;

        void Update()
        {
            if (_curSequence != null && Input.GetMouseButtonDown(0))
            {
                if (_tmp_dialogue.text == _curSequence.DialogueLines[_curIndex].Text)
                {
                    NextLine();
                }
                else
                {
                    StopAllCoroutines();
                    _tmp_dialogue.text = _curSequence.DialogueLines[_curIndex].Text;
                }
            }
        }
        #endregion

        #region Custom
        public void StartDialogue(ScriptableDialogueSequence sequenceValue, UnityEvent eventValue=null, bool isTriggerValue=false)
        {
            StopAllCoroutines();
            _curSequence = sequenceValue;
            _curIndex = 0;

            _cg.alpha = 1;
            _tmp_dialogue.text = string.Empty;
            _tmp_dialogue.color = _curSequence.DialogueLines[_curIndex].Color;

            if (_curSequence.StopMove)
            {
                _playerMove.enabled = false;
                _cameraHeadBob.enabled = false;
            }

            if (eventValue != null)
                _curEvent = eventValue;

            StartCoroutine(TypeLine());

            IsTrigger = isTriggerValue;
        }

        public void StopDialogue() 
        {
            StopAllCoroutines();
            _cg.alpha = 0;
        }

        IEnumerator TypeLine()
        {
            foreach (char c in _curSequence.DialogueLines[_curIndex].Text.ToCharArray())
            {
                _tmp_dialogue.text += c;
                yield return new WaitForSeconds(_curSequence.TypeSpeed);
            }
        }

        void NextLine()
        {
            if (_curIndex < _curSequence.DialogueLines.Length - 1)
            {
                _curIndex++;
                _tmp_dialogue.text = string.Empty;
                _tmp_dialogue.color = _curSequence.DialogueLines[_curIndex].Color;
                StartCoroutine(TypeLine());
            }
            else
            {
                if (_curSequence.StopMove)
                {
                    _playerMove.enabled = true;
                    _cameraHeadBob.enabled = true;
                }

                if (_curEvent != null)
                    _curEvent.Invoke();

                _curEvent = null;

                _curSequence = null;
                _cg.alpha = 0;
            }
        }

        public bool IsCurrentSequence(ScriptableDialogueSequence checkValue) 
        {
            if (_cg.alpha == 0) return false;

            return _curSequence == checkValue;
        }
        #endregion
    }
}

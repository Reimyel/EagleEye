using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

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
        [Space]

        [Header("Debug:")]
        [SerializeField] ScriptableDialogueSequence _curSequence;
        [SerializeField] ScriptableDialogueSequence _testDialogue;
        #endregion

        int _curIndex;
        #endregion

        #region Mono
        void Awake() => Instance = this;

        void Update()
        {
            if (_curSequence != null && Input.GetMouseButtonDown(0))
            {
                if (_tmp_dialogue.text == _curSequence.Lines[_curIndex])
                {
                    NextLine();
                }
                else
                {
                    StopAllCoroutines();
                    _tmp_dialogue.text = _curSequence.Lines[_curIndex];
                }
            }
        }
        #endregion

        #region Custom
        public void StartDialogue(ScriptableDialogueSequence sequenceValue)
        {
            StopAllCoroutines();
            _curSequence = sequenceValue;
            _curIndex = 0;

            _cg.alpha = 1;
            _tmp_dialogue.text = string.Empty;
            _tmp_dialogue.color = _curSequence.Color;
            
            if (_curSequence.StopMove)
                _playerMove.enabled = false;

            StartCoroutine(TypeLine());
        }

        public void StopDialogue() 
        {
            StopAllCoroutines();
            _cg.alpha = 0;
        }

        IEnumerator TypeLine()
        {
            foreach (char c in _curSequence.Lines[_curIndex].ToCharArray())
            {
                _tmp_dialogue.text += c;
                yield return new WaitForSeconds(_curSequence.TypeSpeed);
            }
        }

        void NextLine()
        {
            if (_curIndex < _curSequence.Lines.Length - 1)
            {
                _curIndex++;
                _tmp_dialogue.text = string.Empty;
                StartCoroutine(TypeLine());
            }
            else
            {
                if (_curSequence.StopMove)
                    _playerMove.enabled = true;

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

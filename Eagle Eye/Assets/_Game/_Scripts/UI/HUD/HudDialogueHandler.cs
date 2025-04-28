using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace FourZeroFourStudios
{
    public class HudDialogueHandler : MonoBehaviour
    {
        #region Members
        #region Inspector
        [Header("Settings:")]
        [Space]
        
        [Header("References:")]
        [SerializeField] CanvasGroup _cg;
        [SerializeField] TextMeshProUGUI _tmp_dialogue;
        [Space]

        [Header("Debug:")]
        [SerializeField] ScriptableDialogueSequence _curSequence;
        [SerializeField] ScriptableDialogueSequence _testDialogue;
        #endregion

        int _curIndex;
        #endregion

        #region Mono
        void Start() => StartDialogue(_testDialogue);

        void Update()
        {
            if (Input.GetMouseButtonDown(0))
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
        void StartDialogue(ScriptableDialogueSequence sequenceValue)
        {
            _curSequence = sequenceValue;
            _curIndex = 0;

            _cg.alpha = 1;
            _tmp_dialogue.text = string.Empty;
            _tmp_dialogue.color = _curSequence.Color;
            
            StartCoroutine(TypeLine());
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
                _cg.alpha = 0;
            }
        }
        #endregion
    }
}

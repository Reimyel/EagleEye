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
        [SerializeField] TextMeshProUGUI _tmp_dialogue;
        #endregion

        public string[] lines;
        public float textSpeed;

        int _curIndex;
        #endregion

        #region Mono
        void Start() => StartDialogue();

        void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (_tmp_dialogue.text == lines[_curIndex])
                {
                    NextLine();
                }
                else
                {
                    StopAllCoroutines();
                    _tmp_dialogue.text = lines[_curIndex];
                }
            }
        }
        #endregion

        #region Custom
        void StartDialogue()
        {
            _tmp_dialogue.text = string.Empty;
            _curIndex = 0;
            StartCoroutine(TypeLine());
        }

        IEnumerator TypeLine()
        {
            foreach (char c in lines[_curIndex].ToCharArray())
            {
                _tmp_dialogue.text += c;
                yield return new WaitForSeconds(textSpeed);
            }
        }

        void NextLine()
        {
            if (_curIndex < lines.Length - 1)
            {
                _curIndex++;
                _tmp_dialogue.text = string.Empty;
                StartCoroutine(TypeLine());
            }
            else
            {
                gameObject.SetActive(false);
            }
        }
        #endregion
    }
}

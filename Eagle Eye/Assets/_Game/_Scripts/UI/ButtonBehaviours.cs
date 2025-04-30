using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Security;

namespace FourZeroFourStudios
{
    public class ButtonBehaviours : MonoBehaviour
    {
        [Header("Canvas References")]
        public Canvas EagleEyeCanvas;
        public Canvas InitialCanvas;
        public float Delay = 0.2f;

        [Header("Button References")]
        public Button NoViolationButton;
        public Button TakeDownButton;
        public Button SendAnalysisButton;
        public Button CrimeButton;
        public Button HatredButton;
        public Button NSFWButton;

        public enum Selection { None, NoViolation, TakeDown, Crime, Hatred, NSFW }
        Selection _currentSelection = Selection.None;
        ScriptablePostsData.Tag? _takeDownTag = null;
        Button _currentlySelectedButton;
        Button _currentlySelectedTagButton = null;
        PostBehaviours _postBehaviours;

        private void Start()
        {
            _postBehaviours = FindObjectOfType<PostBehaviours>();

            //listeners "hear" the Player button clicks
            NoViolationButton.onClick.AddListener(() => SelectOption(Selection.NoViolation));
            TakeDownButton.onClick.AddListener(() => SelectOption(Selection.TakeDown));
            SendAnalysisButton.onClick.AddListener(SendAnalysis);

            CrimeButton.onClick.AddListener(() => SelectTag(ScriptablePostsData.Tag.Crime));
            HatredButton.onClick.AddListener(() => SelectTag(ScriptablePostsData.Tag.Hatred));
            NSFWButton.onClick.AddListener(() => SelectTag(ScriptablePostsData.Tag.NSFW));

            //send analysis and tags cannot be interactable before the Player makes his decision
            SendAnalysisButton.interactable = false;
            SetTagButtonsInteractable(false);
        }

        void SelectOption(Selection _selection)
        {
            _currentSelection = _selection;
            _takeDownTag = null;

            if (_currentlySelectedButton != null)
            {
                ResetButtonVisual(_currentlySelectedButton);
            }

            _currentlySelectedButton = (_selection == Selection.NoViolation) ? NoViolationButton : TakeDownButton;
            SetButtonSelectedVisual(_currentlySelectedButton);

            if (_selection == Selection.NoViolation)
            {
                SendAnalysisButton.interactable = true;
                SetTagButtonsInteractable(false);
            }
            else if (_selection == Selection.TakeDown)
            {
                SendAnalysisButton.interactable = false;
                SetTagButtonsInteractable(true);
            }

            if (_currentlySelectedTagButton != null)
            {
                ResetButtonVisual(_currentlySelectedTagButton);
                _currentlySelectedTagButton = null;
            }
        }

        void SelectTag(ScriptablePostsData.Tag _tag)
        {
            _takeDownTag = _tag;
            Debug.Log("Selected Tag: " + _tag);

            if (_currentSelection == Selection.TakeDown)
            {
                SendAnalysisButton.interactable = true;

                if (_currentlySelectedTagButton != null)
                {
                    ResetButtonVisual(_currentlySelectedTagButton);
                }

                switch (_tag)
                {
                    case ScriptablePostsData.Tag.Crime:
                        _currentlySelectedTagButton = CrimeButton;
                        break;
                    case ScriptablePostsData.Tag.Hatred:
                        _currentlySelectedTagButton = HatredButton;
                        break;
                    case ScriptablePostsData.Tag.NSFW:
                        _currentlySelectedTagButton = NSFWButton;
                        break;
                }

                SetButtonSelectedVisual(_currentlySelectedTagButton);
            }
        }

        void SendAnalysis()
        {
            if (_currentSelection == Selection.None) return;
            if (_currentSelection == Selection.TakeDown && _takeDownTag == null) return;

            //ref post atual
            ScriptablePostsData _currentPost = _postBehaviours.Posts[_postBehaviours.CurrentPostIndex];
            ScriptablePostsData.Tag _playerTag; //decisão do Jogador

            if (_currentSelection == Selection.NoViolation)
            {
                _playerTag = ScriptablePostsData.Tag.NoViolation;
                Debug.Log("Analysis: No Violation");
            }
            else
            {
                _playerTag = _takeDownTag.Value;
                Debug.Log($"Analysis: Take Down/Tag: {_takeDownTag}");
            }

            //verifica acerto ou erro
            if (_playerTag == _currentPost.CorrectTag)
            {
                _postBehaviours.PlayerScore++;
                Debug.Log("Correct decision");
            }
            else
            {
                Debug.Log($"Incorrect decision. Correct decision: {_currentPost.CorrectTag}");
            }

            _postBehaviours.NextPost();

            #region UI Reset
            if (_currentlySelectedButton != null)
            {
                ResetButtonVisual(_currentlySelectedButton);
            }
            if (_currentlySelectedTagButton != null)
            {
                ResetButtonVisual(_currentlySelectedTagButton);
            }

            _currentSelection = Selection.None;
            _takeDownTag = null;
            SendAnalysisButton.interactable = false;
            SetTagButtonsInteractable(false);
            EventSystem.current.SetSelectedGameObject(null);

            _currentlySelectedButton = null;
            _currentlySelectedTagButton = null;
            #endregion
        }

        void SetTagButtonsInteractable(bool _interactable)
        {
            CrimeButton.interactable = _interactable;
            HatredButton.interactable = _interactable;
            NSFWButton.interactable = _interactable;
        }

        void SetButtonSelectedVisual(Button _button)
        {
            var colors = _button.colors;
            var selectedColor = colors.selectedColor;

            var _cb = new ColorBlock
            {
                colorMultiplier = colors.colorMultiplier,
                disabledColor = colors.disabledColor,
                fadeDuration = colors.fadeDuration,
                highlightedColor = colors.highlightedColor,
                normalColor = selectedColor,
                pressedColor = colors.pressedColor,
                selectedColor = selectedColor
            };

            _button.colors = _cb;
        }

        void ResetButtonVisual(Button button)
        {
            var colors = button.colors;
            var defaultColor = Color.white;

            var cb = new ColorBlock
            {
                colorMultiplier = colors.colorMultiplier,
                disabledColor = colors.disabledColor,
                fadeDuration = colors.fadeDuration,
                highlightedColor = colors.highlightedColor,
                normalColor = defaultColor,
                pressedColor = colors.pressedColor,
                selectedColor = colors.selectedColor
            };

            button.colors = cb;
        }

        public void SwitchCanvas()
        {
            StartCoroutine(SwitchCanvasCoroutine(Delay));
        }

        IEnumerator SwitchCanvasCoroutine(float _delayInSeconds)
        {
            yield return new WaitForSeconds(_delayInSeconds);
            if (EagleEyeCanvas != null && InitialCanvas != null)
            {
                bool _isCanvasAActive = EagleEyeCanvas.gameObject.activeSelf;
                bool _isCanvasBActive = InitialCanvas.gameObject.activeSelf;

                EagleEyeCanvas.gameObject.SetActive(!_isCanvasAActive);
                InitialCanvas.gameObject.SetActive(!_isCanvasBActive);
            }
        }
    }
}

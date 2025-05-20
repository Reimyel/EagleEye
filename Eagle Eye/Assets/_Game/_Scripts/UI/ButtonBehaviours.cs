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
        #region Members
        [Header("Canvas")]
        public Canvas EagleEyeCanvas;
        public Canvas InitialCanvas;
        public float Delay = 0.2f;

        [Header("Button")]
        public Button NoViolationButton;
        public Button TakeDownButton;
        public Button SendAnalysisButton;
        public Button CrimeButton;
        public Button HatredButton;
        public Button NSFWButton;

        [Header("Tutorial")]
        [SerializeField] Image _blackFilter;
        [SerializeField] float _filterTransparency;
        [SerializeField] float _changeDuration;
        [SerializeField] Button _firstTutorialText;

        public enum Selection { None, NoViolation, TakeDown, Crime, Hatred, NSFW }
        Selection _currentSelection = Selection.None;
        ScriptablePostsData.Tag? _takeDownTag = null;
        Button _currentlySelectedButton;
        Button _currentlySelectedTagButton = null;
        PostBehaviours _postBehaviours;
        #endregion

        #region Mono
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
        #endregion

        #region Custom
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

            if (_selection == Selection.NoViolation) //enables send analysis button if no violation is selected
            {
                SendAnalysisButton.interactable = true;
                SetTagButtonsInteractable(false);
            }
            else if (_selection == Selection.TakeDown)
            {
                SendAnalysisButton.interactable = false;
                SetTagButtonsInteractable(true); //enables tag buttons as well ONLY if its take down
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
            //Debug.Log("Selected Tag: " + _tag);

            //only enables the Player to select the tags IF he selected take down before
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

            //references the current post
            ScriptablePostsData _currentPost = _postBehaviours.Posts[_postBehaviours.CurrentPostIndex];
            //Player decision
            ScriptablePostsData.Tag _playerTag;

            if (_currentSelection == Selection.NoViolation)
            {
                _playerTag = ScriptablePostsData.Tag.NoViolation;
                //Debug.Log("Analysis: No Violation");
            }
            else
            {
                _playerTag = _takeDownTag.Value;
                //Debug.Log($"Analysis: Take Down/Tag: {_takeDownTag}");
            }

            //verifies if the decision is correct or not
            if (_playerTag == _currentPost.CorrectTag)
            {
                _postBehaviours.PlayerScore++;
                Debug.Log("Correct decision");
            }
            else
            {
                Debug.Log($"Incorrect decision. Correct decision: {_currentPost.CorrectTag}");
            }

            _postBehaviours.NextPost(); //advances to the next post in the index order

            #region UI Reset
            //resets ALL buttons to their default state
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

        public void SetTagButtonsInteractable(bool _interactable)
        {
            //makes the tag buttons interactable
            //(for when the Player makes his decision after choosing take down)
            CrimeButton.interactable = _interactable;
            HatredButton.interactable = _interactable;
            NSFWButton.interactable = _interactable;
        }

        public void SetButtonsInteractable(bool _interactable)
        {
            //makes buttons interactable
            NoViolationButton.interactable = _interactable;
            TakeDownButton.interactable = _interactable;
            SendAnalysisButton.interactable = _interactable;
        }

        public void SwitchCanvas()
        {
            StartCoroutine(SwitchCanvasCoroutine(Delay));
        }

        IEnumerator SwitchCanvasCoroutine(float _delayInSeconds)
        {
            //adds a delay before changing from one canvas to another
            yield return new WaitForSeconds(_delayInSeconds);
            if (EagleEyeCanvas != null && InitialCanvas != null)
            {
                bool _isCanvasAActive = EagleEyeCanvas.gameObject.activeSelf;
                bool _isCanvasBActive = InitialCanvas.gameObject.activeSelf;

                EagleEyeCanvas.gameObject.SetActive(!_isCanvasAActive);
                InitialCanvas.gameObject.SetActive(!_isCanvasBActive);
            }
        }

        public void StartTutorialSequence()
        {
            StartCoroutine(ImageFade(0f, _filterTransparency, _changeDuration));
        }

        IEnumerator ImageFade(float beginningValue, float endValue, float duration)
        {
            float elapsedTime = 0;
            while (elapsedTime < duration)
            {
                elapsedTime += Time.deltaTime;
                float newAlpha = Mathf.Lerp(beginningValue, endValue, elapsedTime / duration);
                _blackFilter.color = new Color(_blackFilter.color.r, _blackFilter.color.g, _blackFilter.color.b, newAlpha);
                yield return null;
                
                if (elapsedTime >= endValue)
                {
                    //show first tutorial text
                    _firstTutorialText.gameObject.SetActive(true);
                }
            }
        }

        #region Button Visuals
        void SetButtonSelectedVisual(Button _button)
        {
            //changes the normal button color to the selected button color
            //(I couldn't find out how to maintain selected color through the inspector)
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
            //resets button colors to normal
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
        #endregion
        #endregion
    }
}

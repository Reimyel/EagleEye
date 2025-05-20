using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Rendering;

namespace FourZeroFourStudios
{
    [System.Serializable]
    public class PostLayout
    {
        public GameObject LayoutRoot;
        public Image UserAvatar;
        public TextMeshProUGUI Userinfo;
        public TextMeshProUGUI Content;
        public Image PostImage; //only for layout_image
    }

    public class PostBehaviours : MonoBehaviour
    {
        [Header("Posts")]
        public int CurrentPostIndex = 0;
        public int PlayerScore; //making correct decisions earns the Player points
        public ScriptablePostsData[] Posts;

        [Header("Layouts")]
        public PostLayout LayoutDefault;
        public PostLayout LayoutSmall;
        public PostLayout LayoutBig;
        public PostLayout LayoutImage;

        [Header("Canvas")]
        public GameObject LoadingScreen;
        public GameObject PostPanel;
        public float Delay = 1f;

        [Header("Disable / Active UI")]
        [SerializeField] private GameObject _go_player;
        [SerializeField] private CameraHolder _cameraHolder;
        [SerializeField] private Volume _volume;
        [SerializeField] private VolumeProfile _vprofile_default;
        [SerializeField] private Animator _anim_cameraHolder;
        [SerializeField] GameObject[] _go_canvas_minigames;
        [SerializeField] GameObject _go_canvas_hud;

        [Header("Max")]
        [SerializeField] MaxBehaviour _maxBehaviour;
        bool _maxSequence = false;
        HeroPropDoorOffice _heroPropDoorOffice;

        private void Start()
        {
            _heroPropDoorOffice = FindObjectOfType<HeroPropDoorOffice>();
            DisplayCurrentPost();
        }

        public void ReturnToPosts() => StartCoroutine(SwitchBackToPostCoroutine(Delay));

        public void NextPost()
        {
            if (Posts.Length == 0) return;
            CurrentPostIndex = (CurrentPostIndex + 1) % Posts.Length;
            DisplayCurrentPost();
        }

        private void DisplayCurrentPost()
        {
            ScriptablePostsData _post = Posts[CurrentPostIndex];

            //reset all layouts
            LayoutDefault.LayoutRoot.SetActive(false);
            LayoutSmall.LayoutRoot.SetActive(false);
            LayoutBig.LayoutRoot.SetActive(false);
            LayoutImage.LayoutRoot.SetActive(false);

            //define which layout opens, based on what layout the post scriptable orders
            PostLayout _activeLayout = null;

            switch (_post.Layout)
            {
                case ScriptablePostsData.LayoutType.Default:
                    LayoutDefault.LayoutRoot.SetActive(true);
                    _activeLayout = LayoutDefault;
                    break;
                case ScriptablePostsData.LayoutType.Small:
                    LayoutSmall.LayoutRoot.SetActive(true);
                    _activeLayout = LayoutSmall;
                    break;
                case ScriptablePostsData.LayoutType.Big:
                    LayoutBig.LayoutRoot.SetActive(true);
                    _activeLayout = LayoutBig;
                    break;
                case ScriptablePostsData.LayoutType.Image:
                    LayoutImage.LayoutRoot.SetActive(true);
                    _activeLayout = LayoutImage;
                    break;
            }

            //updates the post with its info
            //if the layout is layoutimage, it assigns the image in the scriptable
            if (_post.Layout == ScriptablePostsData.LayoutType.Image && _activeLayout.PostImage != null)
            {
                _activeLayout.PostImage.sprite = _post.PostImage;
            }

            _activeLayout.UserAvatar.sprite = _post.UserImage;
            _activeLayout.Userinfo.text = _post.UserInfo;
            _activeLayout.Content.text = _post.Content;

            //go to loading screen after a certain amount of posts
            switch (CurrentPostIndex)
            {
                case 5:
                    SwitchDisplay();
                    //dialogue with Max
                    _maxSequence = true;
                    _maxBehaviour.StartSequence();
                    break;
                case 12:
                    SwitchDisplay();
                    _heroPropDoorOffice.EnableCanOpen(HeroPropDoorOffice.DisableDoor.OUT);
                    break;
                case 20:
                    SwitchDisplay();
                    break;
                case 29:
                    SwitchDisplay();
                    break;
            }
        }

        public void SwitchDisplay()
        {
            if (Posts[CurrentPostIndex] != null && LoadingScreen != null)
            {
                bool _isPostPanelActive = PostPanel.activeSelf;
                bool _isLoadingScreenActive = LoadingScreen.activeSelf;

                PostPanel.SetActive(!_isPostPanelActive);
                LoadingScreen.SetActive(!_isLoadingScreenActive);
            }
            StartCoroutine(SwitchToLoadingCoroutine(Delay));
        }

        private IEnumerator SwitchToLoadingCoroutine(float _delayInSeconds)
        {
            //adds a delay so Player can see its loading, before being "ejected" from the laptop
            yield return new WaitForSeconds(_delayInSeconds);

            _go_canvas_hud.SetActive(true);

            for (int i = 0;  i < _go_canvas_minigames.Length; i++)
                _go_canvas_minigames[i].SetActive(false);

            _anim_cameraHolder.Play("Anim_CameraHolder_ZoomOut");
            _volume.profile = _vprofile_default;

            if (!_maxSequence)
            {
                StartCoroutine(EnablePlayer(1f));
            }

            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        private IEnumerator EnablePlayer(float _delayInSeconds) 
        {
            yield return new WaitForSeconds(_delayInSeconds);
            _go_player.SetActive(true);
            _cameraHolder.IsPlayerSeated = false;
        }

        private IEnumerator SwitchBackToPostCoroutine(float _delayInSeconds)
        {
            yield return new WaitForSeconds(_delayInSeconds);
            SwitchDisplay();
            NextPost();
        }
    }
}

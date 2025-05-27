using System.Collections;
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
        [SerializeField] GameObject _loadingScreen;
        [SerializeField] GameObject _postPanel;
        ButtonBehaviours _buttonBehaviours;
        public float Delay = 1f;

        [Header("Disable / Active UI")]
        [SerializeField] private GameObject _go_player;
        [SerializeField] private CameraHolder _cameraHolder;
        [SerializeField] private Volume _volume;
        [SerializeField] private VolumeProfile _vprofile_default;
        [SerializeField] private Animator _anim_cameraHolder;
        [SerializeField] GameObject[] _go_canvas_minigames;
        [SerializeField] GameObject _go_canvas_hud;
        [SerializeField] CameraMove _cameraMove;

        [Header("Props")]
        [SerializeField] HeroPropChairOffice _heroPropChairOffice;
        [SerializeField] HeroPropVendingMachine _heroPropVendingMachine;
        [SerializeField] HeroPropDoorOffice _heroPropDoorOffice;
        [SerializeField] HeroPropToilet[] _heroPropToilet;

        [Header("Max")]
        [SerializeField] MaxBehaviour _maxBehaviour;

        [Header("Triggers")]
        [SerializeField] GameObject _triggerModerationPause1;
        [SerializeField] GameObject _triggerModerationPause2;
        [SerializeField] GameObject _triggerModerationPause3;

        [Header("Cultists")]
        [SerializeField] GameObject _cultistMoveTrigger;

        private void Start()
        {
            _buttonBehaviours = FindObjectOfType<ButtonBehaviours>();
            DisplayCurrentPost();
        }

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
                //dialogue with Max
                case 5:
                    EndModeration();
                    _maxBehaviour.StartSequence();
                    break;
                //Connor hears cultists
                case 12:
                    EndModeration();
                    StartCoroutine(EnablePauseTrigger());
                    _heroPropDoorOffice.EnableCanOpenOUT();
                    break;
                //Connor goes out for coffee
                case 20:
                    EndModeration();
                    StartCoroutine(EnablePauseTrigger());
                    _heroPropDoorOffice.EnableCanOpenOUT();

                    _heroPropVendingMachine.SetCanTakeCoffee();
                    break;
                //Connor goes to the bathroom
                case 29:
                    EndModeration();
                    StartCoroutine(EnablePauseTrigger());
                    _heroPropDoorOffice.EnableCanOpenOUT();

                    foreach (HeroPropToilet _script in _heroPropToilet)
                    {
                        _script.enabled = true;
                    }
                    break;
            }
        }

        public void EndModeration()
        {
            if (Posts[CurrentPostIndex] != null && _loadingScreen != null)
            {
                _postPanel.SetActive(false);
                _loadingScreen.SetActive(true);
                _buttonBehaviours.SetTagButtonsInteractable(false);
                _buttonBehaviours.SetButtonsInteractable(false);
            }
            StartCoroutine(SwitchToLoading(Delay));
        }

        public void ReturnToModeration()
        {
            if (Posts[CurrentPostIndex] != null && _loadingScreen != null)
            {
                _postPanel.SetActive(true);
                _loadingScreen.SetActive(false);
                _buttonBehaviours.SetTagButtonsInteractable(true);
                _buttonBehaviours.SetButtonsInteractable(true);
            }
        }

        private IEnumerator SwitchToLoading(float _delayInSeconds)
        {
            //adds a delay so Player can see its loading, before being "ejected" from the laptop
            yield return new WaitForSeconds(_delayInSeconds);

            _go_canvas_hud.SetActive(true);

            for (int i = 0;  i < _go_canvas_minigames.Length; i++)
                _go_canvas_minigames[i].SetActive(false);

            _anim_cameraHolder.Play("Anim_CameraHolder_ZoomOut");
            _volume.profile = _vprofile_default;

            _cameraMove.MouseCanMoveScreen = true;
            _cameraMove.HideCursor();
        }

        public IEnumerator EnablePlayer(float _delayInSeconds) 
        {
            yield return new WaitForSeconds(_delayInSeconds);
            _heroPropChairOffice.GetUp();
        }

        public IEnumerator ReturnToPosts(float _delayInSeconds)
        {
            yield return new WaitForSeconds(_delayInSeconds);
            ReturnToModeration();
            NextPost();
        }

        IEnumerator EnablePauseTrigger()
        {
            yield return new WaitForSeconds(Delay);
            switch (CurrentPostIndex)
            {
                case 12:
                    _triggerModerationPause1.SetActive(true);
                    _cultistMoveTrigger.SetActive(true);
                    break;
                case 20:
                    _triggerModerationPause2.SetActive(true);
                    break;
                case 29:
                    _triggerModerationPause3.SetActive(true);
                    break;
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

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

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                StartCoroutine(SwitchBackToPostCoroutine(Delay));
            }
        }

        private void Start()
        {
            DisplayCurrentPost();
        }

        public void NextPost()
        {
            if (Posts.Length == 0) return;
            CurrentPostIndex = (CurrentPostIndex + 1) % Posts.Length;
            DisplayCurrentPost();
        }

        void DisplayCurrentPost()
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
                    break;
                case 12:
                    SwitchDisplay();
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

        IEnumerator SwitchToLoadingCoroutine(float _delayInSeconds)
        {
            //adds a delay so Player can see its loading, before being "ejected" from the laptop
            yield return new WaitForSeconds(_delayInSeconds);
            Debug.Log("nn tem lógica pra sair ainda");
        }

        IEnumerator SwitchBackToPostCoroutine(float _delayInSeconds)
        {
            yield return new WaitForSeconds(_delayInSeconds);
            Debug.Log("nn tem lógica pra voltar ainda");
            SwitchDisplay();
            NextPost();
        }
    }
}

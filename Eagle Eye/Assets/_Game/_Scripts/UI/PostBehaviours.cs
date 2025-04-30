using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace FourZeroFourStudios
{
    public class PostBehaviours : MonoBehaviour
    {
        public ScriptablePostsData[] Posts;
        public int CurrentPostIndex = 0;

        public Image UserImageUI;
        public TextMeshProUGUI UserInfoUI;
        public TextMeshProUGUI ContentUI;

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

            UserImageUI.sprite = _post.UserImage;
            UserInfoUI.text = _post.UserInfo;
            ContentUI.text = _post.Content;
        }
    }
}

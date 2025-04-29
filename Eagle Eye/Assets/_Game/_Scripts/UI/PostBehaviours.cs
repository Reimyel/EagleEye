using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace FourZeroFourStudios
{
    public class PostBehaviours : MonoBehaviour
    {
        public ScriptablePostsData[] Posts;
        public int CurrentPostIndex = 0;

        public Image UserImageUI;
        public Text UsernameUI;
        public Text UsertagUI;
        public Text DateUI;
        public Text ContentUI;

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
            UsernameUI.text = _post.Username;
            UsertagUI.text = _post.Usertag;
            DateUI.text = _post.Date;
            ContentUI.text = _post.Content;
    }
    }
}

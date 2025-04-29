using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace FourZeroFourStudios
{
    [CreateAssetMenu(fileName = "New Post", menuName = "Post System/Post Data")]
    public class ScriptablePostsData : MonoBehaviour
    {
        public Sprite UserImage;
        public string Username;
        public string Usertag;
        public string Date;
        [TextArea(3,10)] public string Content;
    }
}

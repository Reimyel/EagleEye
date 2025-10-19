using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace FourZeroFourStudios
{
    [CreateAssetMenu(fileName = "NewPost_", menuName = "Scriptables/NewPost")]
    public class ScriptablePostsData : ScriptableObject
    {
        public Sprite UserImage;
        [TextArea(5,5)] public string UserInfo;
        [TextArea(10,10)] public string Content;
        public Sprite PostImage;
        public Tag CorrectTag;
        public LayoutType Layout;
        public enum Tag { NoViolation, Crime, Hatred, NSFW }
        public enum LayoutType { Default, Small, Big, Image }
    }
}

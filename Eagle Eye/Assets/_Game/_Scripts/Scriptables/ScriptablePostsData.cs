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
        [TextArea(2,2)] public string UserInfo;
        [TextArea(10,10)] public string Content;
    }
}

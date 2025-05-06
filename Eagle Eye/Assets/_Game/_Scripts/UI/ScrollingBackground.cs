using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace FourZeroFourStudios
{
    public class ScrollingBackground : MonoBehaviour
    {
        [SerializeField] RawImage _image;
        [SerializeField] float _x, _y;

        void Update()
        {
            _image.uvRect = new Rect(_image.uvRect.position + new Vector2(_x, _y) * Time.deltaTime, _image.uvRect.size);
        }
    }
}

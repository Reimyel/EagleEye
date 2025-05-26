using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FourZeroFourStudios
{
    public class CameraZooming : MonoBehaviour
    {
        [SerializeField] Animator _anim_camHolder;
        
        bool _canZooming = true;

        void Update()
        {
            CheckZoomIn();
            CheckZoomOut();
        }

        void CheckZoomIn() 
        {
            if (Input.GetMouseButtonDown(1) && _canZooming)
                _anim_camHolder.Play("Anim_CameraHolder_ZoomIn");
        }

        void CheckZoomOut() 
        {
            if (Input.GetMouseButtonUp(1))
                _anim_camHolder.Play("Anim_CameraHolder_ZoomOut");
        }

        public void Disactivate() 
        {
            _canZooming = false;
            _anim_camHolder.Play("Anim_CameraHolder_Default");
        }

        public void Activate() => _canZooming = true;
    }
}

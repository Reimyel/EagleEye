using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FourZeroFourStudios
{
    public class CameraZooming : MonoBehaviour
    {
        #region Members
        
        [Header("References:")]
        [SerializeField] Animator _anim_camHolder;

        [Header("Parameters:")]
        [SerializeField] float _speedIn;
        [SerializeField] float _speedOut;
        
        bool _canZooming = true;
        float _defaultAnimSpeed;
        #endregion

        #region Mono
        void Start() => _defaultAnimSpeed = _anim_camHolder.speed;

        void Update()
        {
            CheckZoomIn();
            CheckZoomOut();
        }
        #endregion

        #region Custom

        #region Inputs
        void CheckZoomIn() 
        {
            if (Input.GetButtonDown("Zoom") && _canZooming) 
            {
                StopAllCoroutines();
                _anim_camHolder.speed = _speedIn;
                _anim_camHolder.Play("Anim_CameraHolder_ZoomIn");
            }
        }

        void CheckZoomOut() 
        {
            if (Input.GetButtonUp("Zoom")) 
            {
                _anim_camHolder.speed = _speedOut;
                _anim_camHolder.Play("Anim_CameraHolder_ZoomOut");
                StartCoroutine(ResetAnimatorSpeed(3.0f));
            }
        }

        IEnumerator ResetAnimatorSpeed(float value) 
        {
            yield return new WaitForSeconds(value);
            _anim_camHolder.speed = _defaultAnimSpeed;
        }
        #endregion

        #region Managing Behaviour
        public void Deactivate() 
        {
            _canZooming = false;
            _anim_camHolder.Play("Anim_CameraHolder_Default");
        }

        public void Activate() => _canZooming = true;
        #endregion

        #endregion
    }
}

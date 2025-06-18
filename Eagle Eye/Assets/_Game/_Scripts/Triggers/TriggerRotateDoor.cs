using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace FourZeroFourStudios
{
    public class TriggerRotateDoor : MonoBehaviour
    {
        #region Members
        // Inspector
        [Header("Settings:")]
        [Space]

        [Header("Parameters:")]
        [SerializeField] float _sfxInterval;
        [Space]

        [Header("References:")]
        [SerializeField] Transform _transf_door;
        [SerializeField] EntitySFXController _sfxController;
        [Space]

        [Header("Parameters:")]
        [SerializeField] float _rotateSpeed;
        [Space]

        // SFX Control
        bool _isPlayingSFX = false;
        #endregion

        #region Unity
        void OnEnable() => Raycaster.OnRaycast += CheckRotate;

        void OnDisable() => Raycaster.OnRaycast -= CheckRotate;
        #endregion

        #region Custom
        void CheckRotate(GameObject gameObjectValue, TextMeshProUGUI tmpValue) 
        {
            if (gameObjectValue != gameObject)
            {
                if (_isPlayingSFX) 
                {
                    _isPlayingSFX = false;
                    StopAllCoroutines();
                }
                return;
            }

            if (Input.GetKey(KeyCode.W)) 
            {
                if (!_isPlayingSFX) 
                {
                    StartCoroutine(PlayRotating());
                    _isPlayingSFX = true;
                }

                Rotate();
            }
        }

        void Rotate() => _transf_door.rotation *= Quaternion.Euler(0f, -_rotateSpeed * Time.deltaTime, 0f);
        
        IEnumerator PlayRotating() 
        {
            _sfxController.Play("Rotate");
            yield return new WaitForSeconds(_sfxInterval);
            StartCoroutine(PlayRotating()); 
        }
        #endregion
    }
}

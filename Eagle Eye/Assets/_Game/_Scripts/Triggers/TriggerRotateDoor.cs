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
        [SerializeField] float _minDistance;

        [Header("References:")]
        [SerializeField] Transform _playerTransform;
        [SerializeField] Transform _transf_door;
        [Space]

        [Header("Parameters:")]
        [SerializeField] float _rotateSpeed;
        #endregion

        #region Unity
        void OnEnable() => Raycaster.OnRaycast += CheckRotate;

        void OnDisable() => Raycaster.OnRaycast -= CheckRotate;
        #endregion

        #region Custom
        void CheckRotate(GameObject gameObjectValue, TextMeshProUGUI tmpValue) 
        {
            if (gameObjectValue != gameObject) return;

            float distance = Vector3.Distance(gameObject.transform.position, _playerTransform.position);

            if (Input.GetKey(KeyCode.W) && distance <= _minDistance)
                Rotate();
        }

        void Rotate() => _transf_door.rotation *= Quaternion.Euler(0f, -_rotateSpeed * Time.deltaTime, 0f);
        #endregion
    }
}

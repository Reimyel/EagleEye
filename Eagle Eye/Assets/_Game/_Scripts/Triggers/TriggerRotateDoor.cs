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

        [Header("References:")]
        [SerializeField] Transform _transf_door;

        [Header("Parameters:")]
        [SerializeField] float _rotateSpeed;

        Transform _transf_player;
        #endregion

        #region Unity
        void OnEnable() => Raycaster.OnRaycast += CheckRotate;

        void OnDisable() => Raycaster.OnRaycast -= CheckRotate;
        #endregion

        #region Custom
        void CheckRotate(GameObject gameObjectValue, TextMeshProUGUI tmpValue) 
        {
            if (gameObjectValue != gameObject) return;

            if (Input.GetKey(KeyCode.W))
                Rotate();
        }

        void Rotate() => _transf_door.rotation *= Quaternion.Euler(0f, -_rotateSpeed * Time.deltaTime, 0f);
        #endregion
    }
}

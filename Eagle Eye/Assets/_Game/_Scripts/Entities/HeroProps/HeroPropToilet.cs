using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FourZeroFourStudios
{
    public class HeroPropToilet : BaseHeroProp
    {
        [Header("Settings:")]
        [Space]

        [Header("References:")]

        [Header("Hierarchy:")]
        [SerializeField] GameObject _go_player;
        [SerializeField] CameraHolder _cameraHolder;
        [SerializeField] CameraHeadBob _cameraHeadBob;
        [SerializeField] CameraZooming _cameraZooming;

        private void Update()
        {
            if (Input.GetKey(KeyCode.Space))
            {
                GetUp();
            }
        }

        public override void Interact()
        {
            base.Interact();

            Sit();

            this.enabled = false;
        }

        void Sit()
        {
            FadeManager.Instance.StartFade();
            _go_player.SetActive(false);
            _cameraZooming.Deactivate();
            _cameraHolder.IsPlayerSeatedToilet = true;
            _cameraHeadBob.enabled = false;
        }

        public void GetUp()
        {
            FadeManager.Instance.StartFade();
            _go_player.SetActive(true);
            _cameraZooming.Activate();
            _cameraHolder.IsPlayerSeatedToilet = false;
            _cameraHeadBob.enabled = true;
        }
    }
}

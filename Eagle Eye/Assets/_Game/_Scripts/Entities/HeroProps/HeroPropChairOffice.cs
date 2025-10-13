using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;

namespace FourZeroFourStudios
{
    public class HeroPropChairOffice : BaseHeroProp
    {
        [Header("Settings:")]
        [Space]

        [Header("References:")]

        [Header("Audio:")]
        [SerializeField] EntitySFXController _sfxController;

        [Header("Hierarchy:")]
        [SerializeField] GameObject _go_player;
        [SerializeField] GameObject _go_cameraPosition;
        [SerializeField] CameraHolder _cameraHolder;
        [SerializeField] CameraHeadBob _cameraHeadBob;
        [SerializeField] HeroPropLaptop _heroPropLaptop;
        [SerializeField] CameraZooming _cameraZooming;

        public override void Interact()
        {
            base.Interact();

            Sit();

            this.enabled = false;
            _heroPropLaptop.EnableLaptop();
        }

        void Sit()
        {
            _sfxController.Play("Sit");
            FadeManager.Instance.StartFade();
            _go_player.SetActive(false);
            _cameraZooming.Deactivate();
            
            _cameraHolder.UpdateTransform(_go_cameraPosition.transform.position, _go_cameraPosition.transform.rotation);

            _cameraHolder.IsSeating = true;

            _cameraHeadBob.enabled = false;
        }

        public void GetUp()
        {
            _cameraHolder.IsSeating = false;

            FadeManager.Instance.StartFade();
            _go_player.SetActive(true);
            _cameraZooming.Activate();
            _cameraHeadBob.enabled = true;
            _heroPropLaptop.DisableLaptop();
        }
    }
}

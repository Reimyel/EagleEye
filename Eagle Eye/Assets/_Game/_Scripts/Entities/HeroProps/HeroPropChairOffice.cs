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

        [Header("Hierarchy:")]
        [SerializeField] GameObject _go_player;
        [SerializeField] CameraHolder _cameraHolder;
        [SerializeField] CameraHeadBob _cameraHeadBob;
        [SerializeField] HeroPropLaptop _heroPropLaptop;

        public override void Interact()
        {
            base.Interact();

            Sit();

            this.enabled = false;
            _heroPropLaptop.EnableLaptop();
        }

        void Sit()
        {
            FadeManager.Instance.StartFade();
            _go_player.SetActive(false);

            _cameraHolder.IsPlayerSeated = true;
            _cameraHeadBob.enabled = false;
        }

        public void GetUp()
        {
            FadeManager.Instance.StartFade();
            _go_player.SetActive(true);
            _cameraHolder.IsPlayerSeated = false;

            _heroPropLaptop.DisableLaptop();
        }
    }
}

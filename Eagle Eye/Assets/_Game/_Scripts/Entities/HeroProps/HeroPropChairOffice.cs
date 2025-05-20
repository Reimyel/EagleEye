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

        public override void Interact()
        {
            base.Interact();
            _go_player.SetActive(false);

            _cameraHolder.IsPlayerSeated = true;
            _cameraHeadBob.enabled = false;
        }
    }
}

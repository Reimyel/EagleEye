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
        [SerializeField] Transform _transf_cameraPosition;
        [SerializeField] GameObject _go_desktop_canvas;
        [SerializeField] GameObject _go_initial_canvas;
        [SerializeField] GameObject _go_canvas_hud;
        [SerializeField] PostBehaviours _postBehaviours;

        [Header("Rendering:")]
        [SerializeField] Volume _volume;
        [SerializeField] VolumeProfile _vprofile_crt;
        [SerializeField] float _changeProfileDelay;

        bool _isFirstTime = true;

        public override void Interact()
        {
            base.Interact();
            _go_player.SetActive(false);

            _cameraHolder.enabled = false;

            _cameraHolder.gameObject.transform.position = _transf_cameraPosition.position;
            _cameraHolder.gameObject.transform.rotation = _transf_cameraPosition.rotation;

            _go_canvas_hud.SetActive(false);

            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            ApplyCrt();
        }

        void ApplyCrt() 
        {
            _volume.profile = _vprofile_crt;

            _go_desktop_canvas.SetActive(true);
            _go_initial_canvas.SetActive(true);

            if (_isFirstTime)
                _isFirstTime = false;
            else
                _postBehaviours.ReturnToPosts();

            this.enabled = false;
        }
    }
}

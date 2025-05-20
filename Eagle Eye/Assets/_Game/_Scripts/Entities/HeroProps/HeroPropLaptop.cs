using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

namespace FourZeroFourStudios
{
    public class HeroPropLaptop : BaseHeroProp
    {
        [Header("Settings:")]
        [Space]

        [Header("References:")]

        [Header("Hierarchy:")]
        [SerializeField] CameraHolder _cameraHolder;
        [SerializeField] GameObject _go_desktop_canvas;
        [SerializeField] GameObject _go_initial_canvas;
        [SerializeField] GameObject _go_canvas_hud;
        [SerializeField] PostBehaviours _postBehaviours;
        [SerializeField] CameraMove _cameraMove;


        [Header("Rendering:")]
        [SerializeField] Volume _volume;
        [SerializeField] VolumeProfile _vprofile_crt;
        [SerializeField] float _changeProfileDelay;

        Animator _anim_cameraHolder;
        bool _isFirstTime = true;

        void Start() => _anim_cameraHolder = _cameraHolder.gameObject.GetComponent<Animator>();

        public override void Interact()
        {
            base.Interact();

            _anim_cameraHolder.Play("Anim_CameraHolder_ZoomIn");
            _go_canvas_hud.SetActive(false);
            StartCoroutine(StartModeration());
        }

        IEnumerator StartModeration()
        {
            yield return new WaitForSeconds(_changeProfileDelay);

            //apply CRT effect
            _volume.profile = _vprofile_crt;

            _go_desktop_canvas.SetActive(true);
            _go_initial_canvas.SetActive(true);

            if (_isFirstTime)
            {
                _isFirstTime = false;
            }
            else
            {
                _postBehaviours.ReturnToPosts();
            }

            _cameraMove.ShowCursor();

            this.enabled = false;
        }
    }
}

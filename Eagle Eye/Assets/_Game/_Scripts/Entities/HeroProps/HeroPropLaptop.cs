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

        [Header("Audio:")]
        [SerializeField] EntitySFXController _sfxController;

        [Header("Hierarchy:")]
        [SerializeField] CameraHolder _cameraHolder;
        [SerializeField] GameObject _go_desktop_canvas;
        [SerializeField] GameObject _go_initial_canvas;
        [SerializeField] GameObject _go_eagleeye_canvas;
        [SerializeField] GameObject _go_canvas_hud;
        [SerializeField] PostBehaviours _postBehaviours;
        [SerializeField] CameraMove _cameraMove;
        [SerializeField] Collider _collider;


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
            _cameraMove.MouseCanMoveScreen = false;
            StartCoroutine(StartModeration());
        }

        IEnumerator StartModeration()
        {
            yield return new WaitForSeconds(_changeProfileDelay);

            //apply CRT effect
            _volume.profile = _vprofile_crt;
            _go_desktop_canvas.SetActive(true);

            _cameraMove.ShowCursor();

            if (_isFirstTime)
            {
                _isFirstTime = false;
                PlaySFXKeyboard();
                Invoke("PlaySFXStartup", 1f);
                
                _go_initial_canvas.SetActive(true);
            }
            else
            {
                PlaySFXKeyboard();
                StartCoroutine(_postBehaviours.ReturnToPosts(_postBehaviours.Delay));
                _go_eagleeye_canvas.SetActive(true);
                _go_initial_canvas.SetActive(false);
            }

            DisableLaptop();
        }

        public void EnableLaptop()
        {
            _collider.enabled = true;
            this.enabled = true;
        }

        public void DisableLaptop()
        {
            _collider.enabled = false;
            this.enabled = false;
        }

        void PlaySFXKeyboard() => _sfxController.Play("Keyboard");

        void PlaySFXStartup() => _sfxController.Play("Startup");
    }
}

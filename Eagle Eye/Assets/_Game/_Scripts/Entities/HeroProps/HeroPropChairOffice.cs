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

        Animator _anim_cameraHolder;

        bool _isFirstTime = true;

        void Start() => _anim_cameraHolder = _cameraHolder.gameObject.GetComponent<Animator>();

        public override void Interact()
        {
            base.Interact();
            _go_player.SetActive(false);

            _cameraHolder.gameObject.transform.position = _transf_cameraPosition.position;
            _cameraHolder.gameObject.transform.rotation = _transf_cameraPosition.rotation;

            _anim_cameraHolder.Play("Anim_CameraHolder_ZoomIn");

            _go_canvas_hud.SetActive(false);

            StartCoroutine(ApplyCrt());
        }

        IEnumerator ApplyCrt() 
        {
            yield return new WaitForSeconds(_changeProfileDelay);

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


            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            this.enabled = false;
        }
    }
}

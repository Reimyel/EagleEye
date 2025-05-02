using HauntedPSX.RenderPipelines.PSX.Runtime;
using Unity.VisualScripting;
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

        [Header("Rendering:")]
        [SerializeField] Volume _volume;
        [SerializeField] VolumeProfile _vprofile_crt;

        Animator _anim_cameraHolder;

        void Start() => _anim_cameraHolder = _cameraHolder.gameObject.GetComponent<Animator>();

        public override void Interact()
        {
            base.Interact();
            _go_player.SetActive(false);

            _cameraHolder.enabled = false;

            _cameraHolder.gameObject.transform.position = _transf_cameraPosition.position;
            _cameraHolder.gameObject.transform.rotation = _transf_cameraPosition.rotation;

            _anim_cameraHolder.Play("Anim_CameraHolder_ZoomIn");
            
            _volume.profile = _vprofile_crt;

            this.enabled = false;
        }
    }
}

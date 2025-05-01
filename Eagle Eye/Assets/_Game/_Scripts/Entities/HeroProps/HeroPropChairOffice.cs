using UnityEngine;

namespace FourZeroFourStudios
{
    public class HeroPropChairOffice : BaseHeroProp
    {
        [Header("Settings:")]
        [Space]

        [Header("References:")]
        [SerializeField] GameObject _go_player;
        [SerializeField] CameraHolder _cameraHolder;
        [SerializeField] Transform _transf_cameraPosition;

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

            this.enabled = false;
        }
    }
}

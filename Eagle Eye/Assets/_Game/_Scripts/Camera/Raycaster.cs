using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace FourZeroFourStudios 
{
    public class Raycaster : MonoBehaviour
    {
        #region Members
        [Header("Settings:")]
        [Space]

        [Header("Parameters:")]
        [SerializeField] float _distance;
        [SerializeField] LayerMask _layerMask;
        public bool IsInteracting;
        [Space]

        [Header("References")]
        [SerializeField] Transform _rayPoint;
        [SerializeField] TextMeshProUGUI _tmp_action;
        [SerializeField] HudDialogueManager _hudDialogueManager;
        [SerializeField] Image _crosshairIris;
        [SerializeField] Sprite _crosshairIrisEmpty;
        [SerializeField] Sprite _crosshairIrisFill;

        public static event System.Action<GameObject, TextMeshProUGUI> OnRaycast;
        #endregion

        #region Mono
        void Update() => Cast();
        #endregion

        #region Detecction
        void Cast()
        {
            Ray ray = new Ray(_rayPoint.position, transform.forward);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, _distance, _layerMask))
            {
                Debug.DrawLine(ray.origin, hit.point, Color.green);
                //Debug.Log("Ray collided: " + hit.collider.gameObject.name);

                IsInteracting = true;
                OnRaycast?.Invoke(hit.collider.gameObject, _tmp_action);
                _crosshairIris.sprite = _crosshairIrisFill;
            }
            else
            {
                Debug.DrawLine(ray.origin, ray.origin + ray.direction * _distance, Color.green);
                _tmp_action.text = string.Empty;

                IsInteracting = false;
                if (_hudDialogueManager.IsTrigger == false)
                     _hudDialogueManager.StopDialogue();
                _crosshairIris.sprite = _crosshairIrisEmpty;
            }
        }
        #endregion
    }
}


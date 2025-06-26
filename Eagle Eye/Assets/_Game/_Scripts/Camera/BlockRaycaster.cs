using TMPro;
using UnityEngine;

namespace FourZeroFourStudios 
{
    public class BlockRaycaster : MonoBehaviour
    {
        #region Members
        [Header("Settings:")]
        [Space]

        [Header("Parameters:")]
        [SerializeField] float _distance;
        [SerializeField] LayerMask _layerMask;
        [Space]

        [Header("References")]
        [SerializeField] Transform _rayPoint;
        [SerializeField] Raycaster _raycaster;
        #endregion

        #region Mono
        void Update() => Cast();
        #endregion

        #region Detecction
        void Cast()
        {
            Ray ray = new Ray(_rayPoint.position, transform.forward);
            RaycastHit hit;

            if (_raycaster.IsInteracting) return;

            if (Physics.Raycast(ray, out hit, _distance, _layerMask))
            {
                Debug.DrawLine(ray.origin, hit.point, Color.red);
                Debug.Log("Bloc ray collided: " + hit.collider.gameObject.name);

                SetRaycaster(false);
            }
            else
            {
                Debug.DrawLine(ray.origin, ray.origin + ray.direction * _distance, Color.red);

                SetRaycaster(true);
            }
        }

        void SetRaycaster(bool value)
        {
            _raycaster.enabled = value;
        }
        #endregion
    }
}


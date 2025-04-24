using UnityEngine;

public class Raycaster : MonoBehaviour
{
    #region Membros
    [Header("Parâmetros:")]
    [SerializeField] float _distance;
    [SerializeField] LayerMask _layerMask;

    [Header("Referências:")]
    [SerializeField] Transform _rayPoint;

    public static event System.Action<GameObject> OnRaycast;
    #endregion

    #region Unity
    void Update() => Cast();
    #endregion

    #region Detecção
    void Cast()
    {
        Ray ray = new Ray(_rayPoint.position, transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, _distance, _layerMask))
        {
            Debug.DrawLine(ray.origin, hit.point, Color.green);
            Debug.Log("Ray colidiu: " + hit.collider.gameObject.name);

            OnRaycast?.Invoke(hit.collider.gameObject);
        }
        else
        {
            Debug.DrawLine(ray.origin, ray.origin + ray.direction * _distance, Color.green);
        }
    }
    #endregion
}

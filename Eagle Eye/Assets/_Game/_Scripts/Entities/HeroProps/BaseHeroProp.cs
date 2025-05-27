using TMPro;
using UnityEngine;

namespace FourZeroFourStudios
{
    public abstract class BaseHeroProp : MonoBehaviour
    {
        [Header("Parent Settings:")]
        [SerializeField, TextArea] protected string _actionText;

        void OnEnable() => Raycaster.OnRaycast += CheckInteraction;

        void OnDisable() => Raycaster.OnRaycast -= CheckInteraction;

        void CheckInteraction(GameObject gameObjectValue, TextMeshProUGUI tmpValue) 
        {
            if (gameObjectValue != gameObject) return;

            tmpValue.text = _actionText;

            if (Input.GetButtonDown("Interact"))
                Interact();
        }

        public virtual void Interact() { }
    }
}

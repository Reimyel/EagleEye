using TMPro;
using UnityEngine;

namespace FourZeroFourStudios
{
    public class TriggerDisableRotateDoor : MonoBehaviour
    {
        HeroPropDoorOffice _heroPropDoorOffice;

        void Start() => _heroPropDoorOffice = FindObjectOfType<HeroPropDoorOffice>();

        void OnEnable() => Raycaster.OnRaycast += CheckTrigger;

        void OnDisable() => Raycaster.OnRaycast -= CheckTrigger;

        void CheckTrigger(GameObject gameObjectValue, TextMeshProUGUI tmpValue) 
        {
            if (gameObjectValue != gameObject) return;

            _heroPropDoorOffice.DisableCanOpen();
            gameObject.SetActive(false);
        }
    }
}

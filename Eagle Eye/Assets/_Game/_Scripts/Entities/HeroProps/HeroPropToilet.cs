using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FourZeroFourStudios
{
    public class HeroPropToilet : BaseHeroProp
    {
        [Header("Settings:")]
        [Space]

        [Header("References:")]

        [Header("Hierarchy:")]
        [SerializeField] GameObject _go_player;
        [SerializeField] CameraHolder _cameraHolder;
        [SerializeField] CameraHeadBob _cameraHeadBob;
        //[SerializeField] CameraZooming _cameraZooming;

        [Header("Objects:")]
        [SerializeField] Collider _toiletCollider;
        [SerializeField] GameObject _toiletLid;
        [SerializeField] HeroPropBathroomDoor _heroPropBathroomDoor;

        [Header("Cultist")]
        [SerializeField] HeroPropBathroomDoor _cultistBathroomDoor;
        [SerializeField] CultistBehaviour _cultistBehaviour;

        public override void Interact()
        {
            base.Interact();

            Sit();

            this.enabled = false;
            _toiletCollider.enabled = false;
        }

        void Sit()
        {
            FadeManager.Instance.StartFade();
            _go_player.SetActive(false);
            //_cameraZooming.Deactivate();
            _cameraHolder.IsPlayerSeatedToilet = true;
            _cameraHeadBob.enabled = false;

            _toiletLid.transform.rotation = Quaternion.Euler(0f, 0f, 90f);

            _heroPropBathroomDoor.gameObject.GetComponent<Collider>().enabled = false;
            _heroPropBathroomDoor.DoorLocked = true;
            _heroPropBathroomDoor.CloseBathroomDoor();

            StartCoroutine(CultistSequence(5f));
            StartCoroutine(PlayerGetUp(15f));
        }

        public void GetUp()
        {
            FadeManager.Instance.StartFade();
            _go_player.SetActive(true);
            //_cameraZooming.Activate();
            _cameraHolder.IsPlayerSeatedToilet = false;
            _cameraHeadBob.enabled = true;

            _toiletLid.transform.rotation = Quaternion.Euler(0f, 0f, 0f);

            _heroPropBathroomDoor.gameObject.GetComponent<Collider>().enabled = true;
            _heroPropBathroomDoor.DoorLocked = false;
        }

        IEnumerator CultistSequence(float _delay)
        {
            yield return new WaitForSeconds(_delay);
            _cultistBathroomDoor.OpenBathroomDoor();
            _cultistBehaviour.MoveBathroom();
        }

        IEnumerator PlayerGetUp(float _delay)
        {
            yield return new WaitForSeconds(_delay);
            GetUp();
        }
    }
}

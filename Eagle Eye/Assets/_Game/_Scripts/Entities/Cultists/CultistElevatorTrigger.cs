using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FourZeroFourStudios
{
    public class CultistElevatorTrigger : MonoBehaviour
    {
        [SerializeField] CultistBehaviour _cultistBehaviour;
        [SerializeField] GameObject _cultist;
        [SerializeField] HeroPropElevatorButton _heroPropElevatorButton;
        [SerializeField] HeroPropElevatorButton _heroPropElevatorButton2;
        [SerializeField] GameObject _elevatorCultistTrigger;
        [SerializeField] PropElevatorDoors _propElevatorDoors;
        [SerializeField] float _openElevatorDelay;
        [SerializeField] float _closeElevatorDelay;

        private void OnTriggerEnter(Collider other)
        {
            _elevatorCultistTrigger.SetActive(true);
            _cultistBehaviour.SetCanMove();
            _cultist.SetActive(true);

            _heroPropElevatorButton.ElevatorButtonTrigger = 2;
            _heroPropElevatorButton2.ElevatorButtonTrigger = 2;

            //NOT WORKING
            //gives the illusion that the cultist is entering and closing the elevator
            //StartCoroutine(DelayedElevatorOpen(_openElevatorDelay));
            //StartCoroutine(DelayedElevatorClose(_closeElevatorDelay));

            Destroy(gameObject);
        }

        IEnumerator DelayedElevatorOpen(float delay)
        {
            yield return new WaitForSeconds(delay);
            _propElevatorDoors.OpenElevatorDoors();
        }

        IEnumerator DelayedElevatorClose(float delay)
        {
            yield return new WaitForSeconds(delay);
            _propElevatorDoors.CloseElevatorDoors();
        }
    }
}

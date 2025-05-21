using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FourZeroFourStudios
{
    public class CultistElevatorTrigger : MonoBehaviour
    {
        [SerializeField] CultistBehaviour _cultistBehaviour;
        [SerializeField] HeroPropElevatorButton _heroPropElevatorButton;
        [SerializeField] HeroPropElevatorButton _heroPropElevatorButton2;
        [SerializeField] GameObject _elevatorCultistTrigger;
        [SerializeField] PropElevatorDoors _propElevatorDoors; 

        private void OnTriggerEnter(Collider other)
        {
            _elevatorCultistTrigger.SetActive(true);
            _cultistBehaviour.SetCanMove();

            _heroPropElevatorButton.ElevatorButtonTrigger = 2;
            _heroPropElevatorButton2.ElevatorButtonTrigger = 2;

            //gives the illusion that the cultist is entering and closing the elevator
            _propElevatorDoors.CloseElevatorDoors();

            Destroy(gameObject);
        }
    }
}

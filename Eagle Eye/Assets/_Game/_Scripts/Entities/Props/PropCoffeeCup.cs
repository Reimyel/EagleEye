using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FourZeroFourStudios
{
    public class PropCoffeeCup : MonoBehaviour
    {
        [Header("References:")]
        [SerializeField] Animator _anim_coffeeCup;
        [SerializeField] HeroPropTrashCan _heroPropTrashCan;
        [SerializeField] EntitySFXController _sfxController;

        [Header("Control:")]
        public int TimesDrank = 0;
 
        bool _canDrink = true;

        void Update()
        {
            if (Input.GetMouseButtonDown(0) && _canDrink)
            {
                _anim_coffeeCup.SetBool("_isDrinking", true);
                _sfxController.Play("Drink");
                _canDrink = false;
                StartCoroutine(AnimDefault(1.5f));
            }

            //discard coffee cup
            if (TimesDrank >= 3)
            {
                _canDrink = false;
                _heroPropTrashCan.enabled = true;
            }
        }

        IEnumerator AnimDefault(float _delayInSeconds)
        {
            yield return new WaitForSeconds(_delayInSeconds);
            _anim_coffeeCup.SetBool("_isDrinking", false);
            TimesDrank++;
            _canDrink = true;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FourZeroFourStudios
{
    public class PropCoffeeCup : MonoBehaviour
    {
        [SerializeField] Animator _anim_coffeeCup;
        bool _canDrink = true;
        int _timesDrank = 0;

        void Update()
        {
            if (Input.GetMouseButtonDown(0) && _canDrink)
            {
                _anim_coffeeCup.SetBool("_isDrinking", true);
                _canDrink = false;
                StartCoroutine(AnimDefault(2));
                
            }

            //discard coffee cup
            if (_timesDrank >= 3)
            {
                Destroy(gameObject);
            }
        }

        IEnumerator AnimDefault(float _delayInSeconds)
        {
            yield return new WaitForSeconds(_delayInSeconds);
            _anim_coffeeCup.SetBool("_isDrinking", false);
            _timesDrank++;
            _canDrink = true;
        }
    }
}

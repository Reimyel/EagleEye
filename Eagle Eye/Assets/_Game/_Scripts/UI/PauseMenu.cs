using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace FourZeroFourStudios
{
    public class PauseMenu : MonoBehaviour
    {
        public static bool IsPaused = false;
        public GameObject Go_PauseMenuUI;

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyUp(KeyCode.P))
            {
                if (IsPaused)
                {
                    Resume();
                }
                else 
                {
                    PauseGame();
                }
            }
        }
        void Resume()
        {
            Go_PauseMenuUI.SetActive(false);
            Time.timeScale = 1f;
            IsPaused = false;
            //Enable player movement
            //Desativar cursor

        }

        void PauseGame()
        {
            Go_PauseMenuUI.SetActive(true);
            Time.timeScale = 0f;
            IsPaused = true;
            //Disable player movement
            //Ativar cursor
        }

    }
}

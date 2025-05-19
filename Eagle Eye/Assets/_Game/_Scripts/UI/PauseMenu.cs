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
            Cursor.visible = false; // Esconde o cursor
            Cursor.lockState = CursorLockMode.Locked; // Trava o cursor no centro da tela
            //Enable player movement
            //Desativar cursor

        }

        void PauseGame()
        {
            Go_PauseMenuUI.SetActive(true);
            Time.timeScale = 0f;
            IsPaused = true;
            Cursor.visible = true; // Mostra o cursor
            Cursor.lockState = CursorLockMode.None; // Libera o cursor
            //Disable player movement
            //Ativar cursor
        }

    }
}

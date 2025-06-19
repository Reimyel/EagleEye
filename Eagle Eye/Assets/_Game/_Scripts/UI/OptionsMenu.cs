using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace FourZeroFourStudios
{
    public class OptionsMenu : MonoBehaviour
    {
        public TMP_Dropdown ResolutionDropdown;

        Resolution[] Resolutions;

        private void Start()
        {
            //Adiciona as resoluções disponíveis no array
            Resolutions = Screen.resolutions;

            //Limpa as opções do Dropdown
            ResolutionDropdown.ClearOptions();

            //Dropdown precisa de strigns para colocar como opções
            //Cria uma lista de strings que serão adicionadas no dropdown
            List<string> optionsLocal = new List<string>();

            int currentResolutionIndex = 0;
            for (int i = 0; i < Resolutions.Length; i++)
            {
                string optionLocal = Resolutions[i].width + " x " + Resolutions[i].height;
                optionsLocal.Add(optionLocal);

                if (Resolutions[i].width == Screen.currentResolution.width &&
                    Resolutions[i].height == Screen.currentResolution.height)
                {
                    currentResolutionIndex = i;
                }
            }

            ResolutionDropdown.AddOptions(optionsLocal);
            //Começa o jogo já com a definição certa do monitor
            ResolutionDropdown.value = currentResolutionIndex;
            ResolutionDropdown.RefreshShownValue();
        }

        public void SetResolution (int resolutionIndexValue)
        {
            Resolution resolutionLocal = Resolutions[resolutionIndexValue];
            Screen.SetResolution(resolutionLocal.width, resolutionLocal.height, Screen.fullScreen);
        }

        public void SetFullscreen(bool isFullscreen)
        {
            Screen.fullScreen = isFullscreen;
        }





    }

    
}

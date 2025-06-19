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
            //Adiciona as resolu��es dispon�veis no array
            Resolutions = Screen.resolutions;

            //Limpa as op��es do Dropdown
            ResolutionDropdown.ClearOptions();

            //Dropdown precisa de strigns para colocar como op��es
            //Cria uma lista de strings que ser�o adicionadas no dropdown
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
            //Come�a o jogo j� com a defini��o certa do monitor
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

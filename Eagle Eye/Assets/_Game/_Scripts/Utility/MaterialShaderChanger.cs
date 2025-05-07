using UnityEngine;

namespace FourZeroFourStudios
{
    public class MaterialShaderChanger : MonoBehaviour
    {
        [Header("Settings:")]

        [Header("References:")]
        [SerializeField] Shader _newShader;
        [SerializeField] Material[] _materials;

        void Start() => Change();

        void Change() 
        {
            for (int i = 0; i < _materials.Length; i++)
                _materials[i].shader = _newShader;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FourZeroFourStudios
{
    [CreateAssetMenu(fileName = "DialogueSequence_", menuName = "Scriptables/DialogueSequence")]
    public class ScriptableDialogueSequence : ScriptableObject 
    {
        [Header("Parameters:")]
        [SerializeField, TextArea(2,10)] string[] _lines;
        [SerializeField, Range(0f, 5f)] float _typeSpeed;
        [SerializeField] Color _color = Color.white;
        [SerializeField] bool _stopMove = false;
        // TODO: Audio

        // Interfaces
        [HideInInspector] public string[] Lines { get { return _lines; } private set { _lines = value; } }
        [HideInInspector] public float TypeSpeed { get { return _typeSpeed; } private set { _typeSpeed = value; } }
        [HideInInspector] public Color Color { get { return _color; } private set { _color = value; } }
        [HideInInspector] public bool StopMove { get { return _stopMove; } private set { _stopMove = value; } }
    }
}

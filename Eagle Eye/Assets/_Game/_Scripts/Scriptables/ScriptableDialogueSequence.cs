using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FourZeroFourStudios
{
    [System.Serializable]
    public class DialogueLine 
    {
        [TextArea(2, 10)] public string Text;
        public Color Color;
    }

    [CreateAssetMenu(fileName = "DialogueSequence_", menuName = "Scriptables/DialogueSequence")]
    public class ScriptableDialogueSequence : ScriptableObject 
    {
        [Header("Parameters:")]
        [SerializeField] DialogueLine[] _dialogueLines;
        [SerializeField, Range(0f, 5f)] float _typeSpeed;
        [SerializeField] bool _stopMove = false;
        // TODO: Audio

        // Interfaces
        [HideInInspector] public DialogueLine[] DialogueLines { get {  return _dialogueLines; } private set { _dialogueLines = value; } }
        [HideInInspector] public float TypeSpeed { get { return _typeSpeed; } private set { _typeSpeed = value; } }
        [HideInInspector] public bool StopMove { get { return _stopMove; } private set { _stopMove = value; } }
    }
}

using UnityEngine;

namespace FourZeroFourStudios
{
    [System.Serializable]
    public class NarrativeTriggers
    {
        public GameObject[] _go_triggers;
    }

    public class NarrativeManager : MonoBehaviour
    {
        public static NarrativeManager Instance;

        [Header("Triggers:")]
        [SerializeField] NarrativeTriggers[] _triggers;

        int _curIndex = 0;
    
        public void Progress() 
        {
            _curIndex++;
            ActivateTriggers();
        }

        void ActivateTriggers() 
        {
            GameObject[] curTriggersLocal = _triggers[_curIndex]._go_triggers;

            for (int i = 0; i < curTriggersLocal.Length; i++)
                curTriggersLocal[i].SetActive(true);
        }
    }
}

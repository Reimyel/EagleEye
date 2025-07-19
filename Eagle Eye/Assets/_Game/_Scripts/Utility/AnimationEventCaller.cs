using UnityEngine;
using UnityEngine.Events;

namespace FourZeroFourStudios
{
    public class AnimationEventCaller : MonoBehaviour
    {
        [SerializeField] UnityEvent[] _events;

        public void CallEvent(int index) 
        {
            _events[index].Invoke();
        }
    }
}

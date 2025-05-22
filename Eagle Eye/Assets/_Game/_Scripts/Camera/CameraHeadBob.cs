using UnityEngine;

namespace FourZeroFourStudios
{
    public class CameraHeadBob : MonoBehaviour
    {
        #region Members

        #region Inspector
        [Header("Settings:")]
        [Space]

        [Header("Parameters:")]
        [SerializeField, Range(0.001f, 0.01f)] private float amount;
        [SerializeField] private float frequency;
        [SerializeField, Range(0f, 100f)] private float smooth;
        #endregion

        private Vector3 _startPos;

        private bool _canMove = true;
        #endregion

        #region Mono
        private void Start() => _startPos = transform.localPosition;

        private void Update()
        {
            if (_canMove)
            {
                CheckForHeadbobTrigger();
                StopHeadbob();
            }
        }

        private void OnDisable()
        {
            Debug.Log("Teste");
        }
        #endregion

        #region Custom
        private void CheckForHeadbobTrigger()
        {
            var inputMagnitude = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")).magnitude;

            if (inputMagnitude > 0)
                StartHeadBob();
        }

        private Vector3 StartHeadBob()
        {
            var pos = Vector3.zero;
            pos.y += Mathf.Lerp(pos.y, Mathf.Sin(Time.time * frequency) * amount * 1.4f, smooth * Time.deltaTime);
            pos.x += Mathf.Lerp(pos.x, Mathf.Cos(Time.time * frequency / 2f) * amount * 1.6f, smooth * Time.deltaTime);
            transform.localPosition += pos;
            return pos;
        }

        private void StopHeadbob()
        {
            if (transform.localPosition == _startPos) return;
            transform.localPosition = Vector3.Lerp(transform.localPosition, _startPos, 1 * Time.deltaTime);
        }

        public void Stop()
        {
            _canMove = false;
            transform.localPosition = _startPos;
        }

        public void Enable() => _canMove = true;

        public bool IsActive()
        {
            return _canMove;
        }

        public void ChangeStartPos(Vector3 newPos) => _startPos = newPos;
        #endregion
    }
}

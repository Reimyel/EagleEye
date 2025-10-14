using UnityEngine;
using System.Collections;

namespace FourZeroFourStudios
{
    public class HeroPropMap : BaseHeroProp
    {
        [Header("Parameters")]
        [SerializeField] float _resetInspectionTime;
        [SerializeField] float _moveSpeed;
        [SerializeField] float _rotateSpeed;
        [SerializeField] float _lookUpAngle;

        [Header("References")]
        [SerializeField] GameObject _go_player;
        [SerializeField] Transform _inspectionPoint;
        [SerializeField] CanvasGroup _cg_crossHair;

         // Não serializadas
        bool _isShowing = false;
        bool _isAlreadyHiding = false;
        Vector3 _initialPos;
        Quaternion _initialRotation;
        Coroutine _moveRoutine;
        Camera _mainCamera;
        CameraZooming _cameraZooming;
        CameraMove _cameraMove;
        CameraHeadBob _cameraHeadBob;
        Raycaster _rayCaster;


        void Awake()
        {
            _initialPos = transform.position;
            _initialRotation = transform.rotation;

            _mainCamera = Camera.main;

            _cameraZooming = _mainCamera.GetComponent<CameraZooming>();
            _cameraHeadBob = _mainCamera.GetComponent<CameraHeadBob>();
            _cameraMove = _mainCamera.GetComponent<CameraMove>();
            _rayCaster = _mainCamera.GetComponent<Raycaster>();
        }

        void Update()
        {
            if (!_isShowing) return;

            if (Input.GetButtonDown("Interact") && !_isAlreadyHiding)
            {
                HideMap();
                _isAlreadyHiding = true;
            }
        }

        public override void Interact()
        {
            base.Interact();
            ShowMap();
        }

        void ShowMap()
        {
            _rayCaster.enabled = false;
            _go_player.SetActive(false);
            _cameraMove.MouseCanMoveScreen = false;
            _cameraHeadBob.enabled = false;
            _cameraZooming.Deactivate();
            _cg_crossHair.alpha = 0f;

            if (_moveRoutine != null)
                StopCoroutine(_moveRoutine);

            _moveRoutine = StartCoroutine(MoveToInspectionPoint_Coroutine());

            _isShowing = true;
        }

        void HideMap()
        {
            _go_player.SetActive(true);
            _cameraMove.MouseCanMoveScreen = true;
            _cameraHeadBob.enabled = true;
            _cameraZooming.Activate();
            _cg_crossHair.alpha = 1f;

            if (_moveRoutine != null)
                StopCoroutine(_moveRoutine);

            _moveRoutine = StartCoroutine(ReturnToInitialPosition_Coroutine());

            Invoke(nameof(ResetInspection), _resetInspectionTime);
        }

        void ResetInspection()
        {
            _isShowing = false;
            _isAlreadyHiding = false;
            _rayCaster.enabled = true;
        }

        IEnumerator MoveToInspectionPoint_Coroutine()
        {
            Vector3 startPos = transform.position;
            Quaternion startRot = transform.rotation;
            float curTime = 0f;

            while (curTime < 1f)
            {
                curTime += Time.deltaTime * _moveSpeed;

                transform.position = Vector3.Lerp(startPos, _inspectionPoint.position, curTime);

                Vector3 lookDir = (_mainCamera.transform.position - transform.position).normalized;
                Quaternion lookRot = Quaternion.LookRotation(lookDir);
                lookRot *= Quaternion.Euler(-_lookUpAngle, 0f, 0f);

                transform.rotation = Quaternion.Slerp(startRot, lookRot, curTime * _rotateSpeed * Time.deltaTime);

                yield return null;
            }

            transform.position = _inspectionPoint.position;
            Vector3 finalDir = (_mainCamera.transform.position - transform.position).normalized;
            transform.rotation = Quaternion.LookRotation(finalDir) * Quaternion.Euler(-_lookUpAngle, 0f, 0f);
        }

        IEnumerator ReturnToInitialPosition_Coroutine()
        {
            Vector3 startPos = transform.position;
            Quaternion startRot = transform.rotation;
            float curTime = 0f;

            while (curTime < 1f)
            {
                curTime += Time.deltaTime * _moveSpeed;
                transform.position = Vector3.Lerp(startPos, _initialPos, curTime);
                transform.rotation = Quaternion.Slerp(startRot, _initialRotation, curTime * _rotateSpeed * Time.deltaTime);
                yield return null;
            }

            transform.position = _initialPos;
            transform.rotation = _initialRotation;
        }
    }
}

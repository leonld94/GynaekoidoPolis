using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;
using UnityEngine.UIElements;
using UnityEngine.Windows;

/// <summary>
/// InGameScene에서 카메라를 조작하는 Controller. WASD로 이동, QE로 회전, 마우스 휠로 높이 조절.
/// </summary>
public class CameraController : MonoBehaviour
{
    [Header("Input")]
    [SerializeField] private InputActionReference cameraMoveAction;
    [SerializeField] private InputActionReference cameraRotateAction;
    [SerializeField] private InputActionReference cameraHeightAction;

    [Header("Movement")]
    [SerializeField] private float _moveSpeed = 10f;
    [SerializeField] private float _moveSpeedStep = 1f;
    [SerializeField] private float _minMoveSpeed = 10f;
    [SerializeField] private float _maxMoveSpeed = 100f;
    [SerializeField] private float _rotationSpeed = 90f;

    [Header("Height")]
    [SerializeField] private float _heightStep = 1f;
    [SerializeField] private float _minHeight = 10f;
    [SerializeField] private float _maxHeight = 100f;

    Vector2 _cameraMovementInput;
    float _cameraRotationInput;
    float _scrollInput;
    Vector3 _cameraPosition;
    float _cameraZoomDelta;

    private void OnEnable()
    {
        cameraMoveAction.action.Enable();
        cameraRotateAction.action.Enable();
        cameraHeightAction.action.Enable();
    }

    private void OnDisable()
    {
        cameraMoveAction.action.Disable();
        cameraRotateAction.action.Disable();
        cameraHeightAction.action.Disable();
    }

    void Update()
    {
        Move();
        Rotate();
        ChangeHeight();
    }


    // 움직이는건 local로
    void Move()
    {
        _cameraMovementInput = cameraMoveAction.action.ReadValue<Vector2>();

        // transform.forward는 local forward의 기저벡터가 Global의 기저벡터의 계수로 표현된 벡터임. 
        // Vector3.up를 Normal Vector로 넣어 저 평면에서 transform.forward 방향으로 이동하겠다는 뜻.
        
        Vector3 forward =
        Vector3.ProjectOnPlane(transform.forward, Vector3.up).normalized;

        Vector3 right =
            Vector3.ProjectOnPlane(transform.right, Vector3.up).normalized;

        Vector3 movement =
            right * _cameraMovementInput.x +
            forward * _cameraMovementInput.y;

        transform.position +=
            movement * _moveSpeed * Time.unscaledDeltaTime;

    }


    // 회전은 Global의 Y축을 기준으로
    void Rotate()
    {
        _cameraRotationInput = cameraRotateAction.action.ReadValue<float>();


        // Space.World를 넣어주면 Global 좌표계 기준으로 회전함.
        // Space.Self를 넣어주면 Local 좌표계 기준으로 회전함.
        gameObject.transform.Rotate(
            Vector3.up,
            _cameraRotationInput * _rotationSpeed * Time.unscaledDeltaTime,
            Space.World);

    }

    void ChangeHeight()
    {
        _scrollInput = cameraHeightAction.action.ReadValue<float>();

        if (Mathf.Approximately(_scrollInput, 0f))
        {
            return;
        }

        //Debug.Log("_scrollInput: " + _scrollInput);

        _cameraPosition = transform.position;


        // 카메라 줌인 상황에 따라 카메라 이동 속도도 변경해야함. 
        // 그러니까 유효한 마우스휠인지 먼저 검사해야함: 유효하다면 줌인과 이동속도를 같이 바꾸고, 아니라면 바꾸지 않고

        _cameraZoomDelta = _scrollInput * _heightStep;

        if (_cameraZoomDelta + _cameraPosition.y > _maxHeight)
        {
            _cameraPosition.y = _maxHeight;
            _moveSpeed = _maxMoveSpeed;
        }
        else if (_cameraZoomDelta + _cameraPosition.y < _minHeight)
        {
            _cameraPosition.y = _minHeight;
            _moveSpeed = _minMoveSpeed;
        }
        else
        {
            _cameraPosition.y += _cameraZoomDelta;
            if (_cameraZoomDelta < 0)
            {
                _moveSpeed -= _moveSpeedStep;
            }
            else
            {
                _moveSpeed += _moveSpeedStep;
            }
        }

        //_cameraPosition.y -= Mathf.Sign(_scrollInput) * _heightStep;

        //_cameraPosition.y = Mathf.Clamp(
        //    _cameraPosition.y,
        //    _minHeight,
        //    _maxHeight);
        transform.position = _cameraPosition;

    }
}

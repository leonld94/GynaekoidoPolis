using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;
using UnityEngine.Windows;

public class CameraController : MonoBehaviour
{
    [Header("Input")]
    [SerializeField] private InputActionReference cameraMoveAction;
    [SerializeField] private InputActionReference cameraRotateAction;

    [Header("Movement")]
    [SerializeField] private float moveSpeed = 10f;
    [SerializeField] private float rotationSpeed = 90f;


    Vector2 CameraMovementInput;
    float CameraRotationInput;

    private void OnEnable()
    {
        cameraMoveAction.action.Enable();
        cameraRotateAction.action.Enable();
    }

    private void OnDisable()
    {
        cameraMoveAction.action.Disable();
        cameraRotateAction.action.Disable();
    }

    void Update()
    {
        Move();
        Rotate();
    }


    // 움직이는건 local로
    void Move()
    {
        CameraMovementInput = cameraMoveAction.action.ReadValue<Vector2>();

        // transform.forward는 local forward의 기저벡터가 Global의 기저벡터의 계수로 표현된 벡터임. 
        // Vector3.up를 Normal Vector로 넣어 저 평면에서 transform.forward 방향으로 이동하겠다는 뜻.
        
        Vector3 forward =
        Vector3.ProjectOnPlane(transform.forward, Vector3.up).normalized;

        Vector3 right =
            Vector3.ProjectOnPlane(transform.right, Vector3.up).normalized;

        Vector3 movement =
            right * CameraMovementInput.x +
            forward * CameraMovementInput.y;

        transform.position +=
            movement * moveSpeed * Time.unscaledDeltaTime;

    }


    // 회전은 Global의 Y축을 기준으로
    void Rotate()
    {
        CameraRotationInput = cameraRotateAction.action.ReadValue<float>();


        // Space.World를 넣어주면 Global 좌표계 기준으로 회전함.
        // Space.Self를 넣어주면 Local 좌표계 기준으로 회전함.
        gameObject.transform.Rotate(
            Vector3.up,
            CameraRotationInput * rotationSpeed * Time.unscaledDeltaTime,
            Space.World);

    }
}

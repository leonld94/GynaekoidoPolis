using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;


public class BuildingSelectionControllerScript : MonoBehaviour
{
    [Header("Camera")]
    [SerializeField] private Camera _userCamera;
    [Header("Input")]
    [SerializeField] public InputActionReference SelectAction;
    [Header("UI")]
    [SerializeField] private BuildingInfoUIPanelScript BuildingInfoUIPanel;

    private RaycastHit _hit;
    private Vector2 _mousePosition;
    private Ray _ray;
    private ISelectableBuilding _nowHoveredObject;
    private ISelectableBuilding _prevHoveredObject;
    private ISelectableBuilding _nowSelectedObject;
    private ISelectableBuilding _prevSelectedObject;
    private bool mustOffUI;

    private void OnEnable()
    {
        SelectAction.action.performed += OnSelect;

        SelectAction.action.Enable();
    }

    private void OnDisable()
    {
        SelectAction.action.performed -= OnSelect;

        SelectAction.action.Disable();
    }

    private void OnSelect(InputAction.CallbackContext context)
    {
        //Debug.Log("OnSelet called");

        //대상을 지정한다: 이상해보이겠지만 Update 구조상 previous Hovered를 가져와야함
        _nowSelectedObject = _prevHoveredObject;

        // UI 버튼을 누른 클릭이 월드까지 전달되는 것을 방지
        if (EventSystem.current != null &&
            EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }

        //Debug.Log("Object is" + _nowSelectedObject);
        //Debug.Log("BuildingState is " + _nowSelectedObject?.SelectableBuildingState);
        mustOffUI = BuildingInfoUIPanel.ShowUIPanel(_nowSelectedObject?.SelectableBuildingState);

        if (mustOffUI)
        {
            BuildingInfoUIPanel.OffUIPanel();
        }
        // 이전 UI를 끈다
        //_prevSelectedObject?.HideUIPanel();
        // 현재 UI를 킨다
        //_nowSelectedObject?.ShowUIPanel();
        // 현재를 이전으로 옮긴다.
        _prevSelectedObject = _nowSelectedObject;
    }

    void Update()
    {
        if (_userCamera == null || Pointer.current == null)
        {
            return;
        }

        _mousePosition = Pointer.current.position.ReadValue();
        _ray = _userCamera.ScreenPointToRay(_mousePosition);

        //Debug.Log(_mousePosition.ToString());

        // 1. 마우스 포인터의 Ray를 쬐여 맨 먼저 만나는 대상을 받음
        if (Physics.Raycast(_ray, out _hit, Mathf.Infinity))

        {
            //Debug.DrawRay(_ray.origin, _ray.direction * _hit.distance, Color.yellow);
            //Debug.Log("Did Hit");
            _nowHoveredObject = _hit.collider.gameObject.GetComponent<ISelectableBuilding>();

            // 2. 해당 Object가 Selectable이면 다음 활동들이 활성화됨
            _nowHoveredObject?.HighlightOn();

            if (_nowHoveredObject != _prevHoveredObject)
            {
                _prevHoveredObject?.HighlightOff();
                _prevHoveredObject = _nowHoveredObject;
            }
            
        }
        else
        {
            //Debug.DrawRay(_ray.origin, _ray.direction * 1000, Color.white);
            //Debug.Log("Did not Hit");
            _prevHoveredObject = _nowHoveredObject;
            _nowHoveredObject = null;
        }

    }
}

using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Simulation을 사람이 조작할 수 있도록 하는 Controller
/// </summary>
public class SimulationController : MonoBehaviour
{

    [Header("Input")]
    [SerializeField] public InputActionReference pauseAction;
    [SerializeField] public InputActionReference speed1Action;
    [SerializeField] public InputActionReference speed2Action;
    [SerializeField] public InputActionReference speed3Action;
    [SerializeField] public InputActionReference speed6Action;


    // 바깥에서 참조할 수 있는 Simulation. SimulationController가 Simulation을 가지고 있어야 함.
    public Simulation Simulation { get; private set; }
    [Header("Map")]
    public MapRenderer MapRenderer;

    [Header("Time")]
    public GameObject RightDownUI;

    // 틱 실행 연산을 위해 흐른 시간을 계산하는 변수
    private double _timeAccumulator = 0d;
    // 1배속 기준 1초에 10틱
    private const double TickInterval = 0.1d;


    // 일시정지 상태와 게임속도 상태를 나타내는 변수들과 그의 enum
    public enum TimeSpeed { One = 1, Two = 2, Three = 3, Six = 6 };
    public bool IsPaused { get; private set; } = true;
    public TimeSpeed GameSpeed { get; private set; } = TimeSpeed.One;

    private void Awake()
    {

        Simulation = new Simulation(GameDataTransfer.GetGameData());

    }

    void Start()
    {
        // UI Event 구독시키기
        //pauseAction.action.performed += 

        if (MapRenderer == null)
        {
            Debug.Log("Prototype 감지 from SimulationController.Start()");
        }
        else
        {
            MapRenderer.RenderMap(Simulation.Map);
        }
    }

    private void OnEnable()
    {
        pauseAction.action.performed += OnPause;
        speed1Action.action.performed += OnSpeed1;
        speed2Action.action.performed += OnSpeed2;
        speed3Action.action.performed += OnSpeed3;
        speed6Action.action.performed += OnSpeed6;

        pauseAction.action.Enable();
        speed1Action.action.Enable();
        speed2Action.action.Enable();
        speed3Action.action.Enable();
        speed6Action.action.Enable();
    }

    private void OnDisable()
    {
        pauseAction.action.performed -= OnPause;
        speed1Action.action.performed -= OnSpeed1;
        speed2Action.action.performed -= OnSpeed2;
        speed3Action.action.performed -= OnSpeed3;
        speed6Action.action.performed -= OnSpeed6;

        pauseAction.action.Disable();
        speed1Action.action.Disable();
        speed2Action.action.Disable();
        speed3Action.action.Disable();
        speed6Action.action.Disable();
    }

    private void OnPause(InputAction.CallbackContext context)
    {
        SwitchPause();
        Debug.Log(IsPaused
            ? $"Game Paused in {Simulation.Clock.ToString()} Tick"
            : $"Game Resumed at {(int)GameSpeed}x");
    }

    private void OnSpeed1(InputAction.CallbackContext context)
    {
        GameSpeed = TimeSpeed.One;
        Debug.Log("Game Speed Set to 1x");
    }

    private void OnSpeed2(InputAction.CallbackContext context)
    {
        GameSpeed = TimeSpeed.Two;
        Debug.Log("Game Speed Set to 2x");
    }

    private void OnSpeed3(InputAction.CallbackContext context)
    {
        GameSpeed = TimeSpeed.Three;
        Debug.Log("Game Speed Set to 3x");
    }

    private void OnSpeed6(InputAction.CallbackContext context)
    {
        GameSpeed = TimeSpeed.Six;
        Debug.Log("Game Speed Set to 6x");
    }


    // 게임 진행할지 말지 바꾸는 스위치. 스페이스바 누를 때마다 바뀜.
    public void SwitchPause()
    {
        if (IsPaused)
        {
            RightDownUI.GetComponent<RightDownUIPanelScript>().PausedDisable(GameSpeed);
        }
        else
        {
            RightDownUI.GetComponent<RightDownUIPanelScript>().PausedEnable();
        }
        IsPaused = !IsPaused;
    }

    private void Update()
    {
        if (IsPaused)
        {
            return;
        }

        //Time.DeltaTime은 TimeScale에 영향을 받으니 만약 나중에 TimeScale을 건든다면 문제가 생기므로 선제적으로 Time.unscaledDeltaTime로 변경
        _timeAccumulator += (double)Time.unscaledDeltaTime * (int)GameSpeed;
        
        while (_timeAccumulator >= TickInterval)
        {
            _timeAccumulator -= TickInterval;
            Simulation.Tick();
        }
    }
}

using UnityEngine;

/// <summary>
/// 전체 시뮬레이션 자체를 표상하는 Class. 즉 Simulation 그 자체.
/// </summary>
public class Simulation
{
    public int FoodStorageCapacity = 1000;                    // 창고 하나 지을 때마다 1000씩 증가
    public int MaterialsStorageCapacity = 2000;               // 창고 하나 지을 때마다 1000씩 증가
    public int InactiveGynaikoeideisStorageCapacity = 0;      // 창고 하나 지을 때마다 10씩 증가

    // 초기 저장값
    public int StoredFood = 1000;
    public int StoredMaterials = 2000;
    public int LivingAnthropoiCount = 20;
    public int WorkingAnthropoiCount = 0;
    public int ActiveGynaikoeideisCount = 100;
    public int WorkingGynaikoeideisCount = 0;
    public int InactiveGynaikoeideisCount = 0;

    // 위기값. 해당 값만큼 Food와 Materials 요구량이 배가 됨.
    public int ConsumeMultiplier = 1;

    // 연산용 값. 유지비

    public const int FoodConsumedPerAnthroposPerHour = 10;
    public const int MaterialsConsumedPerAnthroposPerHour = 1;
    public const int MaterialsConsumedPerGynaikoeidesPerHour = 10;

    // 승리 조건
    public const int RescueArrivalDay = 30;

    // 패배 조건
    public const int AnthropoiDeathLimit = 10;

    public SimulationClock Clock { get; private set; }

    // 몸뚱아리 있는 것들. 필멸자, 불멸자, 가축 등
    //public List<Soma> Somata;
    // 건물들. 주거지, 상업지, 공장 등
    //public List<Oikodomema> Oikodomemata;
    // 맵 정보
    public Map Map { get; private set; }

    public Simulation(GameData gameData)
    {
        // Todo: 생성자 제대로 만들기. 지금은 임시로 채워만 놓음.
        // List<Soma> Somata = new List<Soma>();
        // List<Oikodomema> Oikodomemata = new List<Oikodomema>();

        if (gameData == null)
        {
            Debug.Log("Prototype 감지 from Simulation Constructor");
            Clock = new SimulationClock();
            Clock.Reset();
        }
        else
        {
            Map = new Map(gameData.mapData);
            Clock = new SimulationClock(gameData.simulationClockData);
        }
    }

    /// <summary>
    /// 시뮬레이션 틱 진행. Clock은 시간값을 진행시킬 뿐이고 실제론 뭐든 여기서 연산.
    /// </summary>
    public void Tick()
    {
        // Todo: 시뮬레이션 연산 구현
        TimeChange timeChange = Clock.AdvanceTick();

        // 내부에서 하는 활동들:
        // 1. 승리 계산
        // 2. 자원 계산(생산 => 소비 계산)
        // 3. 사망 계산
        // 4. 건설 계산
        // 5. UI 갱신
            // 가능하다면 날짜 변경할 때 D-Day Text 색깔도 변경. 눈에 잘띄도록.
    }
}

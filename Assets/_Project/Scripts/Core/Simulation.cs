using UnityEngine;

/// <summary>
/// 전체 시뮬레이션 자체를 표상하는 Class. 즉 Simulation 그 자체.
/// </summary>
public class Simulation
{

    public SimulationClock Clock { get; private set; }

    // 몸뚱아리 있는 것들. 필멸자, 불멸자, 가축 등
    //public List<Soma> Somata;
    // 건물들. 주거지, 상업지, 공장 등
    //public List<Oikodomema> Oikodomemata;
    // 맵 정보
    // public Map Map;

    public Simulation()
    {
        // Todo: 생성자 제대로 만들기. 지금은 임시로 채워만 놓음.
        // List<Soma> Somata = new List<Soma>();
        // List<Oikodomema> Oikodomemata = new List<Oikodomema>();
        // Map = new Map();
    }

    /// <summary>
    /// SimulationClock을 초기설정하는 함수. 저장된 Sim이 있으면 Clock을 받아오고, 없으면 새로 생성함.
    /// </summary>
    /// <param name="clock">받아올 clock</param>
    public void SetSimulationClock(SimulationClock clock)
    {
        if(Clock != null)
        {
            Debug.LogError("SimulationClock이 이미 설정되어 있어 덮어 쓸 수 없습니다.");
            return;
        }

        //// 가능한 두 가지 경우의 수
        // 1. 시뮬레이션을 새로 만드는 경우(게임을 새로 파는 경우): SimulationClock을 새로 만듦
        // 2. 세이브파일을 받아오는 경우(게임을 이어서 하는 경우): SimulationClock을 세이브파일에서 받아옴
        if (clock == null)
        {
            Clock = new SimulationClock();
            Clock.Reset();
        }
        else
        {
            Clock = clock;
        }
    }

    public void Tick()
    {
        // Todo:
        Clock.AdvanceTick();

        // 내부에서 하는 활동들:
        // 1. Obeject들의 상태를 업데이트 등


    }
}

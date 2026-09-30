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
    public Map Map { get; private set; }

    public Simulation(GameData gameData)
    {
        // Todo: 생성자 제대로 만들기. 지금은 임시로 채워만 놓음.
        // List<Soma> Somata = new List<Soma>();
        // List<Oikodomema> Oikodomemata = new List<Oikodomema>();

        if (gameData == null)
        {
            throw new System.ArgumentNullException(nameof(gameData));
        }


        Map = new Map(gameData.mapData);
        Clock = new SimulationClock(gameData.simulationClockData);
    }

    public void Tick()
    {
        // Todo:
        Clock.AdvanceTick();

        // 내부에서 하는 활동들:
        // 1. Obeject들의 상태를 업데이트 등


    }
}

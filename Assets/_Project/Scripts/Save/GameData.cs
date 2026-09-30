using System;

/// <summary>
/// Simulation 정보를 담고 있는 Class. Simulation을 구성하는 정보들을 담고 있음.
/// </summary>
[Serializable]
public class GameData
{
    public MapData mapData;
    public SimulationClockData simulationClockData;


    public GameData()
    {

    }

    public GameData(MapData mapData, SimulationClockData simulationClockData)
    {
        this.mapData = mapData;
        this.simulationClockData = simulationClockData;
    }
}

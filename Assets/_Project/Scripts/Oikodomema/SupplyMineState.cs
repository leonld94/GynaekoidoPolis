using UnityEngine;

public class SupplyMineState : BuildingState, IWorkable
{
    public const int WorkerLimit = 15;

    // 현재 노동자 수
    public int AnthroposWorkerCount { get; protected set; }
    public int GynaikoeidesWorkerCount { get; protected set; }

    public const int ResourceProduction = 50;

    public SupplyMineState(string siteId, bool isConstructed, 
        GameObject buildingModel, GameObject siteModel) 
        : base(siteId, isConstructed, buildingModel, siteModel)
    {
        AnthroposWorkerCount = 0;
        GynaikoeidesWorkerCount = 0;
        RequiredMaterials = 600;
    }

    public int Work()
    {
        return AnthroposWorkerCount + GynaikoeidesWorkerCount;
    }

    public bool AddAnthroposWorker()
    {
        if (AnthroposWorkerCount + GynaikoeidesWorkerCount < WorkerLimit)
        {
            AnthroposWorkerCount++;
            return true;
        }
        return false;
    }

    public bool SubAnthroposWorker()
    {
        if (AnthroposWorkerCount > 0)
        {
            AnthroposWorkerCount--;
            return true;
        }
        return false;
    }

    public bool AddGynaikoeidesWorker()
    {
        if (AnthroposWorkerCount + GynaikoeidesWorkerCount < WorkerLimit)
        {
            GynaikoeidesWorkerCount++;
            return true;
        }
        return false;
    }

    public bool SubGynaikoeidesWorker()
    {
        if (GynaikoeidesWorkerCount > 0)
        {
            GynaikoeidesWorkerCount--;
            return true;
        }
        return false;
    }
}

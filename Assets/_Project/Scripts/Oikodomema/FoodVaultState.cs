using UnityEngine;

public class FoodVaultState : BuildingState, IWorkable
{
    // 최대 노동자 수
    public const int WorkerLimit = 15;

    // 현재 노동자 수
    public int AnthroposWorkerCount { get; protected set; }
    public int GynaikoeidesWorkerCount { get; protected set; }

    public const int ResourceProduction = 50;

    public FoodVaultState(string siteId, bool isConstructed, 
        GameObject buildingModel, GameObject siteModel) 
        : base(siteId, isConstructed, buildingModel, siteModel)
    {
        AnthroposWorkerCount = 0;
        GynaikoeidesWorkerCount = 0;
        RequiredMaterials = 500;
    }


    public int Work()
    {
        return AnthroposWorkerCount + GynaikoeidesWorkerCount;
    }
    public bool AssignAnthroposWorker()
    {
        if(AnthroposWorkerCount + GynaikoeidesWorkerCount < WorkerLimit)
        {
            AnthroposWorkerCount++;
            return true;
        }
        return false;
    }

    public bool FireAnthroposWorker()
    {
        if(AnthroposWorkerCount > 0)
        {
            AnthroposWorkerCount--;
            return true;
        }
        return false;
    }

    public bool AssignGynaikoeidesWorker()
    {
        if (AnthroposWorkerCount + GynaikoeidesWorkerCount < WorkerLimit)
        {
            GynaikoeidesWorkerCount++;
            return true;
        }
        return false;
    }

    public bool FireGynaikoeidesWorker()
    {
        if (GynaikoeidesWorkerCount > 0)
        {
            GynaikoeidesWorkerCount--;
            return true;
        }
        return false;
    }
}

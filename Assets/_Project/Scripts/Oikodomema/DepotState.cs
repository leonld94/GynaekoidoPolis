using UnityEngine;

public class DepotState : BuildingState
{
    public StoreType StoreType { get; protected set; }

    public DepotState(string siteId, bool isConstructed, 
        GameObject buildingModel, GameObject siteModel) 
        : base(siteId, isConstructed, buildingModel, siteModel)
    {

    }

    /// <summary>
    /// 저장요소를 변경하는 Method
    /// </summary>
    /// <param name="newStoreType"></param>
    /// <returns>저장요소 변경되면 True, 변경되지 않으면 False.</returns>
    public bool ChangeStoreType(StoreType newStoreType)
    {
        if (StoreType != newStoreType)
        {
            StoreType = newStoreType;
            return true;
        }
        return false;
    }
}

public enum StoreType
{
    Food,
    Materials,
    Gynaikoeideis
}
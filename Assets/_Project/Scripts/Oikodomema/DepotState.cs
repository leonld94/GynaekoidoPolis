using System;
using Unity.VisualScripting;
using UnityEngine;

public class DepotState : BuildingState
{
    private StoreType _storeType;
    public StoreType StoreType { get => _storeType; private set => _storeType = value; }

    public DepotState(string siteId, bool isConstructed, 
        GameObject buildingModel, GameObject siteModel,
        StoreType storeType = StoreType.Food) 
        : base(siteId, isConstructed, buildingModel, siteModel)
    {
        RequiredMaterials = 1000;
        StoreType = storeType; // 기본이 Food 
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
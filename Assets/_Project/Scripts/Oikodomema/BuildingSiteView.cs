using System;
using UnityEngine;

public class BuildingSiteView : MonoBehaviour
{
    [SerializeField] private string siteId;
    [SerializeField] private BuildingType buildingTypeId;
    [SerializeField] private bool IsConstructed;
    [SerializeField] private GameObject buildingModel;
    [SerializeField] private GameObject siteModel;

    public string SiteId => siteId;
    public BuildingType BuildingTypeId => buildingTypeId;

    private BuildingState _buildingState;

    public BuildingState CreateInitialState()
    {
        if(buildingTypeId == BuildingType.Depot)
        {
            _buildingState = new DepotState(siteId, IsConstructed, buildingModel, siteModel);
            return _buildingState;
        }
        else if(buildingTypeId == BuildingType.FoodVault)
        {
            _buildingState = new FoodVaultState(siteId, IsConstructed, buildingModel, siteModel);
            return _buildingState;
        }
        else if (buildingTypeId == BuildingType.SupplyMine)
        {
            _buildingState = new SupplyMineState(siteId, IsConstructed, buildingModel, siteModel);
            return _buildingState;
        }

        // else로 정리할 수 있긴 한데 이 친구는 비정상작동이니까 따로 때놓는게 나을거같아서
        _buildingState = new BuildingState(siteId, IsConstructed, buildingModel, siteModel);
        return _buildingState;
    }

    public void Refresh(BuildingState state)
    {
        // state.IsConstructed에 따라 건물 모델 표시
        // 노동자 수, 선택 표시 등의 화면 갱신
    }
}
using System;
using UnityEngine;

public class BuildingSiteView : MonoBehaviour, ISelectableBuilding
{
    [SerializeField] private string siteId;
    [SerializeField] private BuildingType buildingTypeId;
    [SerializeField] private bool IsConstructed;
    [SerializeField] private GameObject buildingModel;
    [SerializeField] private GameObject siteModel;
    [SerializeField] private GameObject EdgeObject;
    public string SiteId => siteId;
    public BuildingType BuildingTypeId => buildingTypeId;

    public BuildingState BuildingState { get; private set; }
    public BuildingState SelectableBuildingState => BuildingState;

    [Header("Depot일 때 특수 선택지")]
    [SerializeField]private StoreType _initialStoreType = StoreType.Food;


    public BuildingState CreateInitialState()
    {
        if(buildingTypeId == BuildingType.Depot)
        {
            BuildingState = new DepotState(siteId, IsConstructed, _initialStoreType);
            RefreshModel();
            return BuildingState;
        }
        else if(buildingTypeId == BuildingType.FoodVault)
        {
            BuildingState = new FoodVaultState(siteId, IsConstructed);
            RefreshModel();
            return BuildingState;
        }
        else if (buildingTypeId == BuildingType.SupplyMine)
        {
            BuildingState = new SupplyMineState(siteId, IsConstructed);
            RefreshModel();
            return BuildingState;
        }

        // else로 정리할 수 있긴 한데 이 친구는 비정상작동이니까 따로 때놓는게 나을거같아서
        BuildingState = new BuildingState(siteId, IsConstructed);
        RefreshModel();
        return BuildingState;
    }

    public void RefreshModel()
    {
        // state.IsConstructed에 따라 건물 모델 표시
        if (BuildingState.IsConstructed)
        {
            buildingModel.SetActive(true);
            siteModel.SetActive(false);
        }
        else
        {
            buildingModel.SetActive(false);
            siteModel.SetActive(true);
        }
    }


    /// <summary>
    /// 선택 가능임을 표지하는 외곽 모서리 발광  함수
    /// </summary>
    public void HighlightOn()
    {
        if(EdgeObject == null)
        {
            return;
        }

        EdgeObject.SetActive(true);
    }
    public void HighlightOff()
    {
        if (EdgeObject == null)
        {
            return;
        }

        EdgeObject.SetActive(false);
    }

    /// <summary>
    /// 선택됨을 표지하는 외곽 모서리 발광 함수 (위랑 두께가 다르던 할듯)
    /// </summary>
    public void SelectedHighlight()
    {

    }

    /// <summary>
    /// 선택 시 소유하고 있는 패널을 띄워줌
    /// </summary>
    public void ShowUIPanel()
    {
        Debug.Log("Show UI from " + gameObject.ToString());

        // 어딘가(어디로?)로 넘기기 _buildingState => 알아서 처리해줌
        //BuildingInfoUIPanel.ShowUIPanel(_buildingState);

    }

    public void HideUIPanel()
    {
        Debug.Log("Hide UI from " + gameObject.ToString());
    }
}
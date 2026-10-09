using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BuildingRegistry : MonoBehaviour
{
    [SerializeField]
    private BuildingSiteView[] buildingSites;

    public IReadOnlyList<BuildingSiteView> BuildingSites => buildingSites;

    private Dictionary<string, BuildingSiteView> _buildingSiteDict;

    /// <summary>
    /// BuildingRegistry가 가진 List<BuildingSiteView>를 기반으로 다른 Member 초기화; dict
    /// </summary>
    /// <returns>생성한 List<BuildingState>를 반환</returns>
    public List<BuildingState> CreateInitialStates()
    {
        // site마다의 BuildingState를 여기서 만들기에 그걸 연결하는 dict도 여기서 만들어야할듯
        _buildingSiteDict = new Dictionary<string, BuildingSiteView>();

        List<BuildingState> states = new();

        foreach (BuildingSiteView site in buildingSites)
        {
            states.Add(site.CreateInitialState());
            _buildingSiteDict.Add(site.BuildingState.SiteId, site);
        }

        return states;
    }

    public void RefreshBySiteId(BuildingState buildingState)
    {
        BuildingSiteView buildingSiteView = _buildingSiteDict[buildingState.SiteId];
        buildingSiteView.RefreshModel();
    }

#if UNITY_EDITOR
    [ContextMenu("Collect Building Sites")]
    private void CollectBuildingSites()
    {
        buildingSites =
            GetComponentsInChildren<BuildingSiteView>(includeInactive: true);
    }
#endif
}

public enum BuildingType
{
    Depot = 0,
    FoodVault = 1,
    SupplyMine = 2
}
using System.Collections.Generic;
using UnityEngine;

public class BuildingRegistry : MonoBehaviour
{
    [SerializeField]
    private BuildingSiteView[] buildingSites;

    public IReadOnlyList<BuildingSiteView> BuildingSites => buildingSites;

    public List<BuildingState> CreateInitialStates()
    {
        List<BuildingState> states = new();

        foreach (BuildingSiteView site in buildingSites)
        {
            states.Add(site.CreateInitialState());
        }

        return states;
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
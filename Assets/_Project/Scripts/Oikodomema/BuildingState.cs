using Unity;
using UnityEngine;

public class BuildingState
{


    public string SiteId { get; }
    public bool IsConstructed { get; protected set; }
    public float ConstructionProgress { get; protected set; } // 0.0f ~ 1.0f
    public float ConstructionGrowth { get; protected set; }
    public int RequiredMaterials { get; protected set; }
    public int AssignedMaterials { get; protected set; }
    public int AnthropoiBuilderCount { get; set; } // 현재 건설 노동자 수
    public int GynaikoeideisBuilderCount { get; set; } // 현재 건설 노동자 수
    public int BuilderLimit { get; }

    private GameObject _buildingModel;
    private GameObject _siteModel;


    public BuildingState(string siteId, bool isConstructed, GameObject buildingModel, GameObject siteModel)
    {
        SiteId = siteId;
        IsConstructed = isConstructed;
        ConstructionProgress = isConstructed ? 1.0f : 0.0f;
        ConstructionGrowth = 1.0f / (10 * 24 * 25);      // 10명이서 하루 일하면 되는 값
        RequiredMaterials = 0;      // 자식 Class에서 재할당
        AssignedMaterials = 0;
        AnthropoiBuilderCount = 0;
        GynaikoeideisBuilderCount = 0;
        BuilderLimit = 15;
        _buildingModel = buildingModel;
        _siteModel = siteModel;

        if(IsConstructed)
        {
            _buildingModel.SetActive(true);
            _siteModel.SetActive(false);
        }
        else
        {
            _buildingModel.SetActive(false);
            _siteModel.SetActive(true);
        }
    }

    /// <summary>
    /// 건설 진행도를 업데이트하는 Method. BuilderCount에 따라 건설 진행도가 증가함.
    /// </summary>
    public void Construct()
    {
        if(AssignedMaterials < RequiredMaterials)
        {
            return;
        }
        ConstructionProgress += ConstructionGrowth 
            * (AnthropoiBuilderCount + GynaikoeideisBuilderCount);
    }

    /// <summary>
    /// 자재
    /// </summary>
    /// <param name="materialsNum">할당 시도한 자재량</param>
    /// <returns>반환된 자재량</returns>
    public int AssignMaterials(int materialsNum)
    {
        AssignedMaterials += materialsNum;

        int returnValue = 0;
        if (RequiredMaterials < AssignedMaterials)
        {
            returnValue = AssignedMaterials - RequiredMaterials;
            AssignedMaterials -= returnValue;
        }
        return returnValue;
    }

    public bool AssignAnthroposBuilder()
    {
        if(AnthropoiBuilderCount + GynaikoeideisBuilderCount < BuilderLimit)
        {
            AnthropoiBuilderCount++;
            return true;
        }

        return false;
    }

    public bool FireAnthroposBuilder()
    {
        if(0 < AnthropoiBuilderCount)
        {
            AnthropoiBuilderCount--;
            return true;
        }

        return false;
    }

    public bool AssignGynaikoeidesBuilder()
    {
        if (AnthropoiBuilderCount + GynaikoeideisBuilderCount < BuilderLimit)
        {
            GynaikoeideisBuilderCount++;
            return true;
        }

        return false;
    }

    public bool FireGynaikoeidesBuilder()
    {
        if (0 < GynaikoeideisBuilderCount)
        {
            GynaikoeideisBuilderCount--;
            return true;
        }

        return false;
    }

    public bool TryConstructionComplete()
    {
        if(ConstructionProgress >= 1.0f)
        {
            IsConstructed = true;
            _buildingModel.SetActive(true);
            _siteModel.SetActive(false);
            return true;
        }
        return false;
    }

    public int ReturnAnthropoiBuilderNum()
    {
        int returnValue = AnthropoiBuilderCount;
        AnthropoiBuilderCount = 0;
        return returnValue;
    }

    public int ReturnGynaikoeideisBuilderNum()
    {
        int returnValue = GynaikoeideisBuilderCount;
        GynaikoeideisBuilderCount = 0;
        return returnValue;
    }
}
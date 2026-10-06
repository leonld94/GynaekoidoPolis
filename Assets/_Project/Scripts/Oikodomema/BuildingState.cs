using Unity;
using UnityEngine;

public class BuildingState
{
    public string SiteId { get; }
    public bool IsConstructed { get; protected set; }

    public float ConstructionProgress { get; protected set; } // 0.0f ~ 1.0f

    public int BuilderCount { get; set; } // 현재 건설 노동자 수

    private GameObject _buildingModel;
    private GameObject _siteModel;


    public BuildingState(string siteId, bool isConstructed, GameObject buildingModel, GameObject siteModel)
    {
        SiteId = siteId;
        IsConstructed = isConstructed;
        ConstructionProgress = isConstructed ? 1.0f : 0.0f;
        BuilderCount = 0;
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
    /// <param name="constructionProgressPerBuilder">건설자 당 틱에 얼마나 진행되는지</param>
    public void Construct(float constructionProgressPerBuilder)
    {
        ConstructionProgress += constructionProgressPerBuilder * BuilderCount;
    }

    public bool isConstructionComplete()
    {
        if(ConstructionProgress >= 1.0f)
        {
            IsConstructed = true;
            BuilderCount = 0;
            _buildingModel.SetActive(true);
            _siteModel.SetActive(false);
            return true;
        }
        return false;
    }
}
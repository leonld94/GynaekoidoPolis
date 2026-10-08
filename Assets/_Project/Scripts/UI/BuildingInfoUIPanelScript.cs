using UnityEngine;
using TMPro;

public class BuildingInfoUIPanelScript : MonoBehaviour
{

    public TextMeshProUGUI BuildingNameText;

    [Header("Under Construction UI")]
    public GameObject UnderConstructionUI;
    [Header("BuilderTap")]
    public TextMeshProUGUI AssignedBuilderValueText;
    public TextMeshProUGUI BuilderLimitValueText;

    [Header("GynaikoeideisTap")]
    public TextMeshProUGUI AssignedGynaikoeideisValueText;
    public TextMeshProUGUI BuilderLimitValueText2;
    [Header("AnthropoiTap")]
    public TextMeshProUGUI AssignedAnthropoiValueText;
    public TextMeshProUGUI BuilderLimitValueText3;
    [Header("MaterialsTap")]
    public TextMeshProUGUI AssignedMaterialsValueText;
    public TextMeshProUGUI RequiredMaterialsValueText;
    [Header("ProgressTap")]
    public TextMeshProUGUI ProgressValueText;


    [Header("CompleteBuilding UI")]
    public GameObject CompleteBuildingUI;
    public GameObject DepotUI;
    public GameObject WorkableUI;
    [Header("Depot UI")]
    public GameObject FoodChoicedButton;
    public GameObject FoodSelectButton;
    public GameObject GynaikoeideisChoicedButton;
    public GameObject GynaikoeideisSelectButton;
    public GameObject MaterialsChoicedButton;
    public GameObject MaterialsSelectButton;

    [Header("Workable UI")]
    public TextMeshProUGUI AssignedWorkerValueText;
    public TextMeshProUGUI WorkerLimitValueText;
    public TextMeshProUGUI AssignedGynaikoeideisWorkerValueText;
    public TextMeshProUGUI WorkerLimitValueText2;
    public TextMeshProUGUI AssignedAnthropoiWorkerValueText;
    public TextMeshProUGUI WorkerLimitValueText3;
    public TextMeshProUGUI ResourceNameText;
    public TextMeshProUGUI ResourceValueText;


    private BuildingState _nowBuilding;

    public bool ShowUIPanel(BuildingState buildingState)
    {
        bool mustOffUI = true;

        //Debug.Log("ShowUIPanel called");
        //Debug.Log($"Now we use {buildingState}");

        if(buildingState == null)
        {
            return mustOffUI;
        }

        _nowBuilding = buildingState;
        gameObject.SetActive(true);
        mustOffUI = false;

        if (buildingState is DepotState depotState)
        {
            BuildingNameText.text = "창고";
            if (!buildingState.IsConstructed)
            {
                ShowUnderConstructionUI();
            }
            else
            {
                //Debug.Log("This Depot is availible");

                UnderConstructionUI.SetActive(false);
                CompleteBuildingUI.SetActive(true);

                DepotUI.SetActive(true);
                WorkableUI.SetActive(false);

                if (depotState.StoreType == StoreType.Food)
                {
                    FoodChoicedButton.SetActive(true);
                    FoodSelectButton.SetActive(false);
                    GynaikoeideisChoicedButton.SetActive(false);
                    GynaikoeideisSelectButton.SetActive(true);
                    MaterialsChoicedButton.SetActive(false);
                    MaterialsSelectButton.SetActive(true);
                }
                else if (depotState.StoreType == StoreType.Gynaikoeideis)
                {

                    FoodChoicedButton.SetActive(false);
                    FoodSelectButton.SetActive(true);
                    GynaikoeideisChoicedButton.SetActive(true);
                    GynaikoeideisSelectButton.SetActive(false);
                    MaterialsChoicedButton.SetActive(false);
                    MaterialsSelectButton.SetActive(true);
                }
                else if (depotState.StoreType == StoreType.Materials)
                {

                    FoodChoicedButton.SetActive(false);
                    FoodSelectButton.SetActive(true);
                    GynaikoeideisChoicedButton.SetActive(false);
                    GynaikoeideisSelectButton.SetActive(true);
                    MaterialsChoicedButton.SetActive(true);
                    MaterialsSelectButton.SetActive(false);
                }
            }
        }
        else
        {
            if (!buildingState.IsConstructed)
            {
                ShowUnderConstructionUI();
            }
            else
            {
                UnderConstructionUI.SetActive(false);
                CompleteBuildingUI.SetActive(true);

                DepotUI.SetActive(false);
                WorkableUI.SetActive(true);
            }

            if (buildingState is FoodVaultState foodBuilding)
            {
                BuildingNameText.text = "식량영구저장소";

                // 노동자
                int workerNum = foodBuilding.GynaikoeidesWorkerCount
                    + foodBuilding.AnthroposWorkerCount;
                AssignedWorkerValueText.text = workerNum.ToString();
                WorkerLimitValueText.text = FoodVaultState.WorkerLimit.ToString();

                // 인형, 인간
                AssignedGynaikoeideisWorkerValueText.text = foodBuilding.GynaikoeidesWorkerCount.ToString();
                WorkerLimitValueText2.text = FoodVaultState.WorkerLimit.ToString();
                AssignedAnthropoiWorkerValueText.text = foodBuilding.AnthroposWorkerCount.ToString();
                WorkerLimitValueText3.text = FoodVaultState.WorkerLimit.ToString();

                // 자재로 바꾸기
                ResourceNameText.text = "<color=#7FFF7F>식량</color>";

                // 숫자 바꾸기
                ResourceValueText.text = $"<color=#7FFF7F>{FoodVaultState.ResourceProduction * workerNum}</color>";
            }
            else if (buildingState is SupplyMineState materialsBuilding)
            {
                BuildingNameText.text = "자재채굴소";

                // 노동자
                int workerNum = materialsBuilding.GynaikoeidesWorkerCount
                    + materialsBuilding.AnthroposWorkerCount;
                AssignedWorkerValueText.text = workerNum.ToString();
                WorkerLimitValueText.text = SupplyMineState.WorkerLimit.ToString();

                // 인형, 인간
                AssignedGynaikoeideisWorkerValueText.text = materialsBuilding.GynaikoeidesWorkerCount.ToString();
                AssignedAnthropoiWorkerValueText.text = materialsBuilding.AnthroposWorkerCount.ToString();

                // 자재로 바꾸기
                ResourceNameText.text = "<color=#FF7F7F>자재</color>";

                // 숫자 바꾸기
                ResourceValueText.text = $"<color=#FF7F7F>{SupplyMineState.ResourceProduction * workerNum}</color>";
            }
            else
            {
                BuildingNameText.text = "Unknown";
            }
        }

        return mustOffUI;
    }

    private void ShowUnderConstructionUI()
    {

        Debug.Log("ShowUnderConstructionUI called");

        UnderConstructionUI.SetActive(true);
        CompleteBuildingUI.SetActive(false);

        //builder
        AssignedBuilderValueText.text = (_nowBuilding.AnthropoiBuilderCount
                + _nowBuilding.GynaikoeideisBuilderCount).ToString();

        BuilderLimitValueText.text = _nowBuilding.BuilderLimit.ToString();
        // gynaik
        AssignedGynaikoeideisValueText.text = _nowBuilding.GynaikoeideisBuilderCount.ToString(); ;
        BuilderLimitValueText2.text = _nowBuilding.BuilderLimit.ToString();
        // anthrop
        AssignedAnthropoiValueText.text = _nowBuilding.AnthropoiBuilderCount.ToString();
        BuilderLimitValueText3.text = _nowBuilding.BuilderLimit.ToString();
        // materials
        AssignedMaterialsValueText.text = _nowBuilding.AssignedMaterials.ToString();
        RequiredMaterialsValueText.text = _nowBuilding.RequiredMaterials.ToString();
        // progress
        ProgressValueText.text = (_nowBuilding.ConstructionProgress * 100f).ToString();
    }

    public void OffUIPanel()
    {
        gameObject.SetActive(false);
    }

    // 버튼도 만들기
}

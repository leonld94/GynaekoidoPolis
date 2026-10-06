using System.Collections.Generic;
using System.Linq;

public class ResourceSystem
{
    public GameState GameState { get; private set; }

    public List<BuildingState> ActiveBuildings { get; private set; }


    public ResourceSystem(GameState gameState, List<BuildingState> ActiveB)
    {
        GameState = gameState;
        ActiveBuildings = ActiveB;
    }


    public void CalculateResourceProduction()
    {
        foreach(BuildingState building in ActiveBuildings)
        {
            if (building is FoodVaultState FoodBuilding)
            {
                int production = FoodBuilding.Work() * GameState.FoodProduced;
                GameState.StoredFood += production;
            }
            else if (building is SupplyMineState MaterialBuilding)
            {
                int production = MaterialBuilding.Work() * GameState.MaterialsProduced;
                GameState.StoredMaterials += production;
            }
        }
    }

    public void CalculateResourceConsumption()
    {
        // 식량 소모 공식: 인간 수 * 시간당 소모량 * 위기값
        // 자재 소모 공식: (인간 수 * 인간 시간당 소모량 + 인형 수 인형 시간당 소모량)
                        // * 위기값 + 노동자 수 * 노동 시간당 소모량

        // 기본적인 소모량
        GameState.FoodConsumedPerHour = 
            GameState.LivingAnthropoiCount * GameState.FoodConsumedPerAnthroposPerHour;
        GameState.MaterialsConsumedPerHour = 
            (GameState.LivingAnthropoiCount * GameState.MaterialsConsumedPerAnthroposPerHour) 
            + (GameState.ActiveGynaikoeideisCount * GameState.MaterialsConsumedPerGynaikoeidesPerHour);
        GameState.RequiredWorkingMaterials = (GameState.WorkingAnthropoiCount + GameState.WorkingGynaikoeideisCount)
            * GameState.MaterialsConsumedPerLabourer;


        // 위기값 적용 소모량
        GameState.FoodConsumedPerHourWithMultiplier = 
            GameState.FoodConsumedPerHour * GameState.ConsumeMultiplier;
        GameState.MaterialsConsumedPerHourWithMultiplier =
            GameState.MaterialsConsumedPerHour * GameState.ConsumeMultiplier
            + GameState.RequiredWorkingMaterials;

        // 실제 소비량 적용
        GameState.StoredFood -= GameState.FoodConsumedPerHourWithMultiplier;
        GameState.StoredMaterials -= GameState.MaterialsConsumedPerHourWithMultiplier;
    }


    public void CalculateDiesPersons()
    {
        IEnumerator<BuildingState> enumerator = ActiveBuildings.GetEnumerator();

        bool hasCurrent = enumerator.MoveNext();

        // 1. 앞부터 순회하며 인간 노동자 전부 빼기
        // 2. 수가 부족하면 그냥 종료하기
        // 어차피 인간이 다 죽으면 겜오버임
        while (GameState.StoredFood < GameState.FoodThreshold)
        {
            if(!hasCurrent)
            {
                break;
            }

            if(enumerator.Current is DepotState)
            {
                hasCurrent = enumerator.MoveNext();
            }
            else
            {
                if(enumerator.Current is FoodVaultState foodBuilding)
                {
                    if(0 < foodBuilding.AnthroposWorkerCount)
                    {
                        foodBuilding.SubAnthroposWorker();
                        GameState.StoredFood -= GameState.FoodThreshold;
                    }
                    else
                    {
                        hasCurrent = enumerator.MoveNext();
                    }
                }
                else if(enumerator.Current is SupplyMineState materialsBuilding)
                {
                    if (0 < materialsBuilding.AnthroposWorkerCount)
                    {
                        materialsBuilding.SubAnthroposWorker();
                        GameState.StoredFood -= GameState.FoodThreshold;
                    }
                    else
                    {
                        hasCurrent = enumerator.MoveNext();
                    }
                }
                else
                {
                    hasCurrent = enumerator.MoveNext();
                }
            }
        }
    }

    public void CalculateDismantle()
    {
        IEnumerator<BuildingState> enumerator = ActiveBuildings.GetEnumerator();

        bool hasCurrent = enumerator.MoveNext();

        // 1. 앞부터 순회하며 인형 노동자 전부 빼기
        // 2. 수가 부족하면 그냥 종료하기.
        while (GameState.StoredMaterials < GameState.MaterialsThreshold)
        {

            if (!hasCurrent)
            {
                break;
            }

            if (enumerator.Current is DepotState)
            {
                hasCurrent = enumerator.MoveNext();
            }
            else
            {
                if (enumerator.Current is FoodVaultState foodBuilding)
                {
                    if (0 < foodBuilding.GynaikoeidesWorkerCount)
                    {
                        foodBuilding.SubGynaikoeidesWorker();
                        GameState.StoredMaterials -= GameState.GynaikoeidesToMaterials;
                    }
                    else
                    {
                        hasCurrent = enumerator.MoveNext();
                    }
                }
                else if (enumerator.Current is SupplyMineState materialsBuilding)
                {
                    if (0 < materialsBuilding.AnthroposWorkerCount)
                    {
                        materialsBuilding.SubGynaikoeidesWorker();
                        GameState.StoredMaterials -= GameState.GynaikoeidesToMaterials;
                    }
                    else
                    {
                        hasCurrent = enumerator.MoveNext();
                    }
                }
                else
                {
                    hasCurrent = enumerator.MoveNext();
                }
            }
        }
    }
}

using Unity.VisualScripting;
using UnityEngine;

public class GameState
{
    // 기본정보
    public int GameFlow = 1; // 0: 끝, 1: 기본.
                             
    // 승리 조건
    public const int RescueArrivalDay = 30;

    // 패배 조건
    public const int AnthropoiDeathLimit = 10;


    /// Level1      0일 ~ 9일
    /// Level2      10일 ~ 17일
    /// Level3      18일 ~ 23일
    /// Level4      24일 ~ 26일
    /// Level5      27일 ~ 29일
    public enum HazardLevelDay
    {
        Level1 = 9,
        Level2 = 17,
        Level3 = 23,
        Level4 = 26,
        Level5 = 29
    }

    /// 식량
    public int FoodStorageCapacity = 1000;                  // 최대 저장량; 창고 하나당 1000
    public int StoredFood = 1000;                           // 저장량
    public int FoodConsumedPerHour = 0;                     // 시간당 소비량
    public int FoodConsumedPerHourWithMultiplier = 0;       // 시간당 소비량(위기값 적용)

    // 자재
    public int MaterialsStorageCapacity = 2000;             // 최대 저장량; 창고 하나당 1000
    public int StoredMaterials = 2000;                      // 저장량
    public int MaterialsConsumedPerHour = 0;                // 시간당 소비량
    public int MaterialsConsumedPerHourWithMultiplier = 0;  // 시간당 소비량(위기값 적용)
    public int RequiredWorkingMaterials = 0;

    // 인형
    public int InactiveGynaikoeideisStorageCapacity = 10;    // 최대 비활성 가능 개수; 창고 하나당 10
    public int InactiveGynaikoeideisCount = 0;              // 비활성화 개수
    public int ActiveGynaikoeideisCount = 100;              // 활성화 개수

    public int WorkingGynaikoeideisCount = 0;               // 노동 개수

    // 인간
    public int LivingAnthropoiCount = 20;                   // 생존자 수
    public int FreeAnthropoiCount = 20;                     // 비노동자 수
    public int WorkingAnthropoiCount = 0;                   // 노동자 수

    // 위기값. 해당 값만큼 Food와 Materials 요구량이 배가 됨.
    public int ConsumeMultiplier = 1;
    

    // 연산용 값.
    // 유지비
    public const int FoodConsumedPerAnthroposPerHour = 1;
    public const int MaterialsConsumedPerAnthroposPerHour = 1;
    public const int MaterialsConsumedPerGynaikoeidesPerHour = 1;
    public const int MaterialsConsumedPerLabourer = 1;
    public const int FoodThreshold = -100; // 인간이 죽는 식량부족임계값
    public const int MaterialsThreshold = 0; // 인간이 인형을 해체해 땔깜으로 쓰는 임계값
    public const int GynaikoeidesToMaterials = 100; // 해체시 돌려받는 비용
    // 생산비
    public const int FoodProduced = 50;
    public const int MaterialsProduced = 50;

    /// <summary>
    /// 식량 부족시 인간을 사망시키는 Method
    /// </summary>
    /// <returns>사망자가 있는 지를 반환</returns>
    public bool CalculateDies()
    {
        bool isSomeoneDying = false;

        while (StoredFood < FoodThreshold)
        {
            isSomeoneDying = true;
            LivingAnthropoiCount--;
            StoredFood -= FoodThreshold;

            // 1. 가능하다면 자유노동력 감소
            if (FreeAnthropoiCount > 0)
            {
                FreeAnthropoiCount--;
            }
            // 2. 안된다면 노동자 감소
            // 이 작업은 다른 class에게 작업 이전: ResourceSystem
            else
            {
                break;
            }
        }
        return isSomeoneDying;
    }

    /// <summary>
    /// 자재 부족시 인형을 분해하는 Method
    /// </summary>
    /// <returns>노동자인 인형도 분해해야하는지를 반환</returns>
    public bool CalculateDismantle()
    {
        bool isWorkerDismantled = false;

        while (StoredMaterials < MaterialsThreshold)
        {
            // 1. 가능하다면 꺼진 것부터 쓰기
            if (0 < InactiveGynaikoeideisCount)
            {
                InactiveGynaikoeideisCount--;
                StoredMaterials += GynaikoeidesToMaterials;
            }
            // 2. 불가능하면 일 안하는 켜져있는 것부터 쓰기
            else if (WorkingGynaikoeideisCount < ActiveGynaikoeideisCount)
            {
                ActiveGynaikoeideisCount--;
                StoredMaterials += GynaikoeidesToMaterials;
            }
            // 3. 안되면 노동자 쓰기
            else
            {
                isWorkerDismantled = true;
                break;  // 인형이 더이상 없는데 어쩔 수 없지...
            }
        }

        return isWorkerDismantled;
    }
}

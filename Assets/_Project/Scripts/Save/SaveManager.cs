using System;
using System.IO;
using System.Text;
using Unity.VisualScripting;
using UnityEngine;

public class SaveManager
{

    private const string _saveDirectoryName = "saves";
    private const string _saveFileExtension = ".json";
    public static string SaveDirectoryPath => Path.Combine(Application.persistentDataPath, _saveDirectoryName);
    // e.g. "saves/slot_10_game.json", /saves/slot_11_meta.json"

    /// <summary>
    /// 게임 저장을 담당하는 Method
    /// </summary>
    /// <param name="slotNum">저장할 파일 슬롯</param>
    /// <param name="gameData">저장할 게임 데이터</param>
    /// <param name="metaData">저장할 게임 메타데이터</param>
    /// <returns>성공 여부를 true/false로 반환</returns>
    public static bool Save(int slotNum, GameData gameData, MetaData metaData)
    {
        //// slot이 유효한지 확인. 유효하지 않으면 false 반환.
        //// slot이 비었는지 확인. 비었으면 진행 isThisSlotEmpty(int slot);
        //// 안 비었으면 돌아가서 정말 덮어씌울건지 질의. 허가받으면 진행
        //// 허가 안받으면 false 반환
        string filePath = GetGameDataPath(slotNum);
        //// GameData 직렬화
        string gameDataJson = JsonUtility.ToJson(gameData);
        //// GameDataPath에 저장
        File.WriteAllText(filePath, gameDataJson, Encoding.UTF8);

        //// MetaData 주소 받아오기
        filePath = GetMetaDataPath(slotNum);
        //// MetaData 갱신
        if(!UpdateMetaData(metaData, gameData))
        {
            throw new System.Exception("MetaData doesn't updated");
        }
        //// MetaData 직렬화
        string metaDataJson = JsonUtility.ToJson(metaData);
        //// MetaDataPath에 저장
        File.WriteAllText(filePath, metaDataJson, Encoding.UTF8);

        return true;
    }

    public static bool UpdateMetaData(MetaData metaData, GameData gameData)
    {

        metaData.gameVersion = Application.version;
        metaData.savedAtUnixTime = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
        //// Todo: playTimeSeconds 구현한 이후에 이거 채워넣기
        //metaData.playTimeSeconds = 

        metaData.mapWidth = gameData.mapData.width;
        metaData.mapHeight = gameData.mapData.height;
        metaData.currentTick = gameData.simulationClockData.currentTick;

        return true;
    }

    public static string GetGameDataPath(int slotNum)
    {
        if(slotNum < 0)
        {
            throw new System.Exception("Slot could not be negetive integer.");
        }

        // slot이 중복일때? 그냥 Pass. 그냥 경고없이 덮어씌우도록 할거임

        // 목표: "~~~\~~~\saves\slot_n_game.json" 뱉기

        return Path.Combine(SaveDirectoryPath, $"slot_{slotNum}_game" + _saveFileExtension);
    }

    public static string GetMetaDataPath(int slotNum)
    {
        if (slotNum < 0)
        {
            throw new System.Exception("Slot could not be negetive integer.");
        }

        // slot이 중복일때? 그냥 Pass. 그냥 경고없이 덮어씌우도록 할거임

        // 목표: "~~~\~~~\saves\slot_n_meta.json" 뱉기

        return Path.Combine(SaveDirectoryPath, $"slot_{slotNum}_meta" + _saveFileExtension);
    }

    /// <summary>
    /// 게임 로딩을 담당하는 Method
    /// </summary>
    /// <param name="slotNum">가져올 파일 슬롯</param>
    /// <returns>잘 수행되었으면 GameData를 반환, 안되었으면 null로 반환</returns>
    public static GameData LoadGameData(int slotNum)
    {
        // 1. filePath를 가져오고
        string gameFilePath = GetGameDataPath(slotNum);

        if (!File.Exists(gameFilePath))
        {
            throw new Exception("게임 데이터 파일이 없습니다. 위치: " + gameFilePath);
        }

        // 2. 파일을 가져오고
        string jsonText = File.ReadAllText(gameFilePath);

        // 3. Json => Object 전환하고
        GameData gameData = JsonUtility.FromJson<GameData>(jsonText);

        // 4. 정상적이면 반환. 근데 검증 코드는 어떻게 짜지
        return gameData;
    }

    public static MetaData LoadMetaData(int slotNum)
    {
        // 1. filePath를 가져오고
        string metaFilePath = GetMetaDataPath(slotNum);

        if (!File.Exists(metaFilePath))
        {
            throw new Exception("메타 데이터 파일이 없습니다. 위치: " + metaFilePath);
        }

        // 2. 파일을 가져오고
        string jsonText = File.ReadAllText(metaFilePath);

        // 3. Json => Object 전환하고
        MetaData metaData = JsonUtility.FromJson<MetaData>(jsonText);

        // 4. 정상적이면 반환. 근데 검증 코드는 어떻게 짜지
        return metaData;
    }
}

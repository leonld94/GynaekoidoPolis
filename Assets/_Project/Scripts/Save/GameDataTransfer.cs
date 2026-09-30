using Unity.VisualScripting;
using UnityEngine;

public class GameDataTransfer
{
    private static GameData _gameData;
    private static MetaData _metaData;

    public static bool SetGameData(GameData gameData)
    {
        if(gameData == null)
        {
            return false;
        }
        _gameData = gameData;
        return true;
    }

    public static bool SetMetaData(MetaData metaData)
    {
        if(metaData == null)
        {
            return false;
        }
        _metaData = metaData;
        return true;
    }

    public static GameData GetGameData()
    {
        return _gameData;
    }

    public static MetaData GetMetaData()
    {
        return _metaData;
    }
}

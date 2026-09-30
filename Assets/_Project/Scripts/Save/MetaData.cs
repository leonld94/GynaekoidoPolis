using System;

/// <summary>
/// Save/Load 화면에서 보여질 각 SaveFile들의 정보를 담은 Class
/// </summary>
[Serializable]
public class MetaData
{
    public string displayName;

    public string saveVersion;
    public string gameVersion;

    public long savedAtUnixTime;
    public long playTimeSeconds;

    public int mapWidth;
    public int mapHeight;
    public long currentTick;
}

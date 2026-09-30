using System;
using UnityEngine;

public enum GeoSchema { plain, hillRange, river, mountainRange, sea, shore }
public enum GeoMorphomaType { plain, hill, mountain, water }

public class Chunk
{

    private readonly int _x;
    private readonly int _y;
    private ChunkData _data;
    public int X { get => _x; }
    public int Y { get => _y; }
    public ChunkData Data { get => _data; }

    public Chunk(int x, int y, ChunkData data)
    {
        _x = x;
        _y = y;
        _data = data;
    }

}

[Serializable]
public class ChunkData
{
    // 청크 전체가 어떤 지형인지
    public GeoSchema geoSchema;
    public bool isUnlocked;
    // 128 * 128 = 16384개의 타일 데이터.
    // index = tileY * 128 + tileX
    public TileData[] tiles;
}

[Serializable]
public struct TileData
{
    // 타일이 어떤 요소인지. 물인지 평지인지 언덕인지
    public GeoMorphomaType geoMorphomaType;
    public bool canMove;
    public bool canBuild;
}
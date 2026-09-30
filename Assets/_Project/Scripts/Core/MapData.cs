using UnityEngine;
using System;

[Serializable]
public class MapData
{
    public int seed;
    // 청크 단위로 나눈 맵의 가로 길이. 예를 들어 10이면 맵은 10x10개의 청크로 나뉘어 있음.
    public int width;
    // 청크 단위로 나눈 맵의 세로 길이. 예를 들어 10이면 맵은 10x10개의 청크로 나뉘어 있음.
    public int height;

    // index = chunkY * width + chunkX
    public ChunkData[] chunks;
}
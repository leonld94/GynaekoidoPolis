using UnityEngine;
using System;

[Serializable]
public class MapData
{
    public enum GeoSchema { plain, hillRange, river, mountainRange, sea, shore }
    public enum GeoMorphomaType { plain, hill, mountain, water }

    public int seed;
    // 청크 단위로 나눈 맵의 가로 길이. 예를 들어 10이면 맵은 10x10개의 청크로 나뉘어 있음.
    public int width;
    // 청크 단위로 나눈 맵의 세로 길이. 예를 들어 10이면 맵은 10x10개의 청크로 나뉘어 있음.
    public int height;

    // index = chunkY * width + chunkX
    public ChunkData[] chunks;

    [Serializable]
    public class ChunkData
    {
        // 청크 전체가 어떤 지형인지
        public GeoSchema geoSchema;
        public bool isUnlocked;
        // 128 * 128 = 16384개의 타일 데이터.
        // index = tileY * 128 + tileX
        public TileData[] tiles;

        [Serializable]
        public class TileData
        {
            // 타일이 어떤 요소인지. 물인지 평지인지 언덕인지
            public GeoMorphomaType geoMorphomaType;
            public bool canMove;
            public bool canBuild;
        }
    }
}



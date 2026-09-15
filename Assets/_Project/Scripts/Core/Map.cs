using UnityEngine;

public class Map
{
    public const int ChunkSize = 128;

    
    //// Codex가 제안해준 MapData 제어. 읽어보니 괜찮은듯?
    //private MapData _data;

    //public Map(MapData data)
    //{
    //    Validate(data);
    //    _data = data;
    //}

    //public MapData.ChunkData GetChunk(int chunkX, int chunkY)
    //{
    //    int index = chunkY * _data.width + chunkX;
    //    return _data.chunks[index];
    //}

    //public MapData.ChunkData.TileData GetTile(
    //    int chunkX,
    //    int chunkY,
    //    int tileX,
    //    int tileY)
    //{
    //    MapData.ChunkData chunk = GetChunk(chunkX, chunkY);

    //    int index = tileY * ChunkSize + tileX;
    //    return chunk.tiles[index];
    //}

    //private static void Validate(MapData data)
    //{
    //    if (data.chunks.Length != data.width * data.height)
    //    {
    //        throw new System.ArgumentException(
    //            "청크 배열 길이가 width * height와 일치하지 않습니다.");
    //    }

    //    foreach (MapData.ChunkData chunk in data.chunks)
    //    {
    //        if (chunk.tiles.Length != ChunkSize * ChunkSize)
    //        {
    //            throw new System.ArgumentException(
    //                $"모든 청크는 {ChunkSize * ChunkSize}개의 타일을 가져야 합니다.");
    //        }
    //    }
    //}

    // Todo: 절차적 생성 class 만들기
    //private ProceduralGenerator proceduralGenerator;

}

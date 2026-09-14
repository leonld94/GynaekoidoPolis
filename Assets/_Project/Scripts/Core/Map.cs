using UnityEngine;

public class Map
{
    public int Seed { get; private set; }
    public int Width { get; private set; }
    public int Height { get; private set; }
    public bool[] isThisChunkLoaded { get; private set; }

    // Todo: 절차적 생성 class 만들기
    //private ProceduralGenerator proceduralGenerator;


}

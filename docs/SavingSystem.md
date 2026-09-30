# 세이브 시스템

> 현황: 설계 중
> 관련코드 `MapData.cs`, `GameData.cs`, `SimulationClock.cs`, `GameDataTransfer.cs`, `Chunk.cs`

## 목적
게임을 저장하고 불러올 수 있도록 세이브 시스템을 구성한다.

## 핵심 구조

```text
GameData
├─ MapData
│  └─ ChunkData[]
│     └─ TileData[]
├─ SimulationClockData (계획)
│  └─ CurrentTick
├─ SomaData[] (계획)
└─ OikodomemaData[] (계획)

MetaData (계획)
├─ displayName
├─ gameVersion
├─ saveVersion
├─ savedAtUnixTime
└─ playTimeSeconds

Simulation
└─ Map
   └─ UnlockedChunks

MapRenderer
```

Save Game과 Load Game은 Save File을 읽어와 GameData Instance를 가져오거나 생성하는 방식으로 동작

저장은 일단 JSON으로 구현하고, 그럼 Instance <=> Json 변환해줄 친구가 있어야하니 담당 class가 있어야함.

직렬화될 개체는 GameData. GameData로 시뮬레이션을 전부 복원할 수 있어야함.
Load는 직렬화된 GameData를 역직렬화해서 메모리에 싣는 거고, Save는 메모리에 올라와있는 GameData를 직렬화해서 저장하는 것.

## 저장위치 및 작명
저장위치: Unity가 지원해주는 Application.persistentDataPath + "\saves"
저장형식: slot_1_game.json, slot_1_meta.json

## Save 방식
저장할 때에는 경고없이 slotNum이 같으면 그냥 무조건 덮어쓰기로. 다른 이름으로 저장은 다른 slotNum을 넣으면 됨.

### Slot 관리



### 튜토리얼 데이터 동작 방식
streamingAssetPath에는 튜토리얼 GameData가 있고 튜토리얼을 실행하면 해당 데이터를 가져와 새로운 SaveFile을 만들어 거기에 복사함.
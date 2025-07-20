using UnityEngine;

[CreateAssetMenu(fileName = "GameConfig", menuName = "Config/GameConfig")]
public class GameConfig : ScriptableObject
{
    [field: SerializeField] public ChunkConfig ChunkConfig { get; private set; }
    [field: SerializeField, Range(200, 300)] public float StartSpeed { get; private set; }
    [field: SerializeField, Range(400, 2000)] public float MaxSpeed { get; private set; }
    [field: SerializeField, Min(0.5f)] public float SpeedIncreaseTime { get; private set; }
    [field: SerializeField, Range(1, 25)] public int SpeedIncrementer { get; private set; }
}

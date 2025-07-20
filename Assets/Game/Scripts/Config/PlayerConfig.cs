using UnityEngine;

[CreateAssetMenu(fileName = "PlayerConfig", menuName = "Config/PlayerConfig")]
public class PlayerConfig : ScriptableObject
{
    [field: SerializeField] public float Fuel { get; private set; }
    [field: SerializeField] public float FuelConsumption { get; private set; }
    [field: SerializeField] public float SlideFactor { get; private set; }
    [field: SerializeField] public float CollisionForce { get; private set; }
}

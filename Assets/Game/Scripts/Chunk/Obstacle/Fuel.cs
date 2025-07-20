using UnityEngine;

public class Fuel : MonoBehaviour
{
    [SerializeField] private float m_fuel;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Player player))
            player.Fill(m_fuel);
    }
}

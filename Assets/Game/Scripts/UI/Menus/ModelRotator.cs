using UnityEngine;

public class ModelRotator : MonoBehaviour
{
    [SerializeField] private float m_rotationSpeed;

    private void Update()
    {
        transform.Rotate(Vector3.up, m_rotationSpeed * Time.deltaTime);
    }
}

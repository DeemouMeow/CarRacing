using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [SerializeField, Range(0f, 360f)] private float m_minRotation;
    [SerializeField, Range(0f, 360f)] private float m_maxRotation;

    public Vector3 Position => transform.position;

    private void OnValidate()
    {
        if (m_maxRotation < m_minRotation)
            m_minRotation = m_maxRotation - 0.001f;
    }

    public void Initialize(Vector3 position)
    {
        SetRandomRotation();
        transform.position = position;
    }

    private void SetRandomRotation()
    {
        float randomRotation = Random.Range(m_minRotation, m_maxRotation);

        transform.rotation = Quaternion.Euler(0f, randomRotation, 0f);
    }
}

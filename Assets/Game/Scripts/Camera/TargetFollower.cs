using UnityEngine;

public class TargetFollower : MonoBehaviour
{
    [SerializeField] private Vector3 m_offsetPosition;
    [SerializeField] private Vector3 m_offsetRotation;

    private Transform m_target;

    private Transform m_transform => gameObject.transform;

    private void Awake()
    {
        enabled = false;
    }

    private void LateUpdate()
    {
        m_transform.position = m_target.position + m_offsetPosition;
    }

    public void Initialize(Transform target)
    {
        m_target = target;
        m_transform.Rotate(m_offsetRotation);
        enabled = true;
    }
}

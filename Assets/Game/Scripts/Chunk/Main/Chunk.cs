using UnityEngine;

public class Chunk : MonoBehaviour
{
    [SerializeField] private Transform m_startPoint;
    [SerializeField] private Transform m_endPoint;
    [SerializeField] private Transform m_obstaclesParent;
    [SerializeField] private ChunkTrigger m_trigger;

    private event System.Action<Chunk> m_moved;

    public event System.Action<Chunk> Moved
    {
        add
        {
            m_moved -= value;
            m_moved += value;
        }

        remove 
        { 
            m_moved -= value; 
        }
    }
    public Transform StartPoint => m_startPoint;
    public Transform EndPoint => m_endPoint;
    public Transform ObstaclesParent => m_obstaclesParent;

    public void Initialize(Vector3 position) 
    {
        m_trigger.Initialize(this);
        transform.position = position;
    }

    public void MarkMoved()
    {
        m_moved?.Invoke(this);
    }
}

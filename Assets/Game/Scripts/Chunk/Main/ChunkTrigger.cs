using UnityEngine;

public class ChunkTrigger : MonoBehaviour
{
    private Chunk m_parentChunk;

    public void Initialize(Chunk chunk) 
    {
        m_parentChunk = chunk;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Player>() != null && m_parentChunk != null)
            m_parentChunk.MarkMoved();
    }
}

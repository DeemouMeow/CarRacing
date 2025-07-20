using System.Collections.Generic;
using UnityEngine;

public class ChunkSpawner
{
    private const int k_maxChuksCount = 2;

    private Chunk m_prefab;
    private Chunk m_lastSpawned;
    private ObstacleSpawner m_obstacleSpawner;
    private List<Chunk> m_spawnedChunks;

    public void Initialize(ChunkConfig config, ObstacleSpawner obstacleSpawner)
    {
        m_prefab = config.Template;

        m_spawnedChunks = new List<Chunk>();
        m_obstacleSpawner = obstacleSpawner;

        m_lastSpawned = Object.Instantiate(m_prefab);
        m_lastSpawned.Initialize(Vector3.zero);

        m_lastSpawned.Moved += OnChankMoved;
        m_obstacleSpawner.SpawnObstacles(m_lastSpawned, new Vector3(0f, 0f, 25f), Vector3.zero);

        m_spawnedChunks.Add(m_lastSpawned);
    }

    private void SpawnNext()
    {
        Chunk chunk = Object.Instantiate(m_prefab);

        float distanceFromCenter = Mathf.Abs(chunk.StartPoint.localPosition.z);
        float chunkZPosition = m_lastSpawned.EndPoint.position.z + distanceFromCenter;

        Vector3 chunkPosition = new Vector3(0, 0, chunkZPosition);

        chunk.Initialize(chunkPosition);
        chunk.Moved += OnChankMoved;
        m_obstacleSpawner.SpawnObstacles(chunk);

        m_lastSpawned = chunk;
        m_spawnedChunks.Add(chunk);
    }

    private void RemoveOdd()
    {
        if (m_spawnedChunks.Count > k_maxChuksCount)
        {
            Object.Destroy(m_spawnedChunks[0].gameObject);
            m_spawnedChunks.RemoveAt(0);
        }
    }

    private void OnChankMoved(Chunk chunk)
    {
        chunk.Moved -= OnChankMoved;

        SpawnNext();
        RemoveOdd();
    }
}

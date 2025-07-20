using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ObstacleSpawner
{
    private const float k_minDistanceForPass = 6f;
    private const float k_minDistanceBetweenTriades = 12f;

    private Chunk m_currentChunk;
    private List<float> m_linesXCoordinates;
    private List<Obstacle> m_obstacles;

    private int m_linesCount;

    public void Initialize(ChunkConfig config)
    {
        m_obstacles = config.Obstacles.ToList();

        m_linesCount = config.LinesCount;
        m_linesXCoordinates = config.LinesXCoordinates.ToList();
    }

    public void SpawnObstacles(Chunk chunk)
    {
        SpawnObstacles(chunk, Vector3.zero, Vector3.zero);
    }

    public void SpawnObstacles(Chunk chunk, Vector3 startOffset, Vector3 endOffset)
    {
        if (chunk == null)
            return;

        m_currentChunk = chunk;
        Vector3 startSpawnPoint = chunk.StartPoint.position + startOffset;
        Vector3 endSpawnPoint = chunk.EndPoint.position + endOffset;
        float obstaclesDistanceMaxOffset = 1.1f;

        Obstacle[] triade = GetRandomObstacles();

        while (endSpawnPoint.z - startSpawnPoint.z > k_minDistanceBetweenTriades + obstaclesDistanceMaxOffset)
        {
            float sign = 1;

            for (int i = 0; i < triade.Length; i++)
            {
                sign = Random.value > 0.5 ? 1 : -1;
                Vector3 spawnPosition = new Vector3(m_linesXCoordinates[i], 1f, startSpawnPoint.z + obstaclesDistanceMaxOffset * sign);
                triade[i] = Object.Instantiate(triade[i], m_currentChunk.ObstaclesParent);
                triade[i].Initialize(spawnPosition);
            }

            int randomIndex = Random.Range(0, triade.Length);

            Vector3 newPosition = triade[randomIndex].Position;
            newPosition.z = triade[randomIndex == 0 ? randomIndex + 1 : randomIndex - 1].Position.z + k_minDistanceForPass * sign;
            triade[randomIndex].transform.position = newPosition;

            startSpawnPoint.z = triade.Select(obstacle => obstacle.Position.z).Max() + k_minDistanceBetweenTriades;
            triade = GetRandomObstacles();
        }
    }

    private Obstacle[] GetRandomObstacles()
    {
        Obstacle[] obstacles = new Obstacle[m_linesCount];

        int randomIndex;

        for (int i = 0; i < obstacles.Length; i++)
        {
            randomIndex = Random.Range(0, m_obstacles.Count);
            obstacles[i] = m_obstacles[randomIndex];
        }

        return obstacles;
    }
}

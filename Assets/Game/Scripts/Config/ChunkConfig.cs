using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ChunkConfig", menuName = "Config/ChunkConfig")]
public class ChunkConfig : ScriptableObject
{
    [SerializeField] private List<Obstacle> m_obstacles;

    [field: SerializeField] public Chunk Template { get; private set; }
    [field: SerializeField] public int LinesCount { get; private set; }
    [field: SerializeField] public float LineWidth { get; private set; }

    private List<float> m_linesXCoordinates;

    public IReadOnlyCollection<Obstacle> Obstacles => m_obstacles;
    public IEnumerable<float> LinesXCoordinates => GetLinesXCoordinates();

    private IEnumerable<float> GetLinesXCoordinates()
    {
        m_linesXCoordinates = new List<float>();

        bool hasCenterLine = LinesCount % 2 != 0;
        int halfLinesCount = Mathf.FloorToInt(LinesCount / 2);

        for (int i = hasCenterLine ? 0 : 1; i <= halfLinesCount; i++)
        {
            float coordinate = i * LineWidth;
            m_linesXCoordinates.Add(coordinate);

            if (coordinate != 0)
                m_linesXCoordinates.Add(-coordinate);
        }

        return m_linesXCoordinates;
    }
}

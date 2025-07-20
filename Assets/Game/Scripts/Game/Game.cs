using System.Collections;
using UnityEngine;

public class Game : MonoBehaviour
{
    private float m_speed;
    private float m_maxSpeed;

    private float m_speedIncrementer;
    private float m_increaseTime;

    private static class GameEvents
    {
        private static event System.Action<float> m_speedUp;
        private static event System.Action<Player> m_gameOver;

        public static event System.Action<float> SpeedUp
        {
            add
            {
                m_speedUp -= value;
                m_speedUp += value;
            }
            remove
            {
                m_speedUp -= value;
            }
        }
        public static event System.Action<Player> GameOver
        {
            add
            {
                m_gameOver -= value;
                m_gameOver += value;
            }
            remove
            {
                m_gameOver -= value;
            }
        }

        public static void InitiateSpeedUp(float speed) 
        {
            if (speed <= 0)
                return;

            m_speedUp?.Invoke(speed);
        }

        public static void InitiateGameOver(Player player) => m_gameOver?.Invoke(player);
    }

    public static event System.Action<float> SpeedUp
    {
        add => GameEvents.SpeedUp += value;
        remove => GameEvents.SpeedUp -= value;
    }
    public static event System.Action<Player> GameOver
    {
        add => GameEvents.GameOver += value;
        remove => GameEvents.GameOver -= value;
    }

    public void Initialize(GameConfig config)
    {
        m_speed = config.StartSpeed;
        m_maxSpeed = config.MaxSpeed;

        m_speedIncrementer = config.SpeedIncrementer;
        m_increaseTime = config.SpeedIncreaseTime;

        Player.Wasted += OnPlayerWasted;
        UI.UI.RestartClicked += OnRestart;
        SpeedUp += OnSpeedUp;

        StartCoroutine(DecreaseTimer());
    }

    private IEnumerator DecreaseTimer()
    {
        yield return new WaitForSeconds(m_increaseTime);

        IncreaseSpeed();
    }

    private void IncreaseSpeed()
    {
        m_speed += m_speedIncrementer;

        if (m_speed >= m_maxSpeed)
            m_speed = m_maxSpeed;

        GameEvents.InitiateSpeedUp(m_speed);
    }

    private void OnSpeedUp(float currentSpeed)
    {
        if (m_speed >= m_maxSpeed)
            return;

        StartCoroutine(DecreaseTimer());
    }

    private void OnRestart()
    {
        SpeedUp -= OnSpeedUp;
        Player.Wasted -= OnPlayerWasted;
        UI.UI.RestartClicked -= OnRestart;
        StopAllCoroutines();
    }

    private void OnPlayerWasted(Player player)
    {
        if (player == null)
            return;

        GameEvents.InitiateGameOver(player);

        SpeedUp -= OnSpeedUp;
        Player.Wasted -= OnPlayerWasted;
        UI.UI.RestartClicked -= OnRestart;
        StopAllCoroutines();
    }
}

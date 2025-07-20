using Input;
using UnityEngine;

public class PlayerMovement
{
    private float m_speed;
    private Vector3 m_velocity;
    private Vector3 m_lastVelocity;

    private bool m_sliding;
    private float m_lerpFactor;
    private float m_slideStep;
    private float m_collisionForceStrength;
    private float m_movedDistance;
    private Vector3 m_lastPosition;

    private class Lerper
    {
        private float m_targetValue;
        private float m_currentValue;
        private float m_startT;
        private float m_currentT;

        public float Value => m_currentValue;
        public bool End => m_currentT == -1;

        public void Initialize(float a, float b, float t)
        {
            m_currentT = 0f;
            m_currentValue = a;
            m_targetValue = b;
            m_startT = t;
        }

        public float Lerp()
        {
            if (End || m_currentT >= 1)
            {
                m_currentT = -1;
                m_currentValue = m_targetValue;
                return m_currentValue;
            }

            m_currentT += m_startT;
            m_currentValue = Mathf.Lerp(m_currentValue, m_targetValue, m_currentT);
            

            return m_currentValue;
        }
    }

    private Player m_player;
    private Lerper m_slideLerper;
    private Rigidbody m_rigidbody;

    public void Initialize(GameConfig gameConfig, Player player)
    {
        if (player is null)
            Debug.LogError(nameof(player) + " is null!");

        m_player = player;

        m_speed = gameConfig.StartSpeed;

        m_lerpFactor = player.Config.SlideFactor;
        m_collisionForceStrength = player.Config.CollisionForce;

        m_slideStep = gameConfig.ChunkConfig.LineWidth;

        m_lastPosition = player.transform.position;
        m_rigidbody = player.GetComponent<Rigidbody>();
        m_rigidbody.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionY;

        m_slideLerper = new Lerper();

        m_rigidbody.position = Vector3.up + Vector3.forward * m_rigidbody.position.z;

        UI.UI.RestartClicked += OnRestart;
        Game.SpeedUp += OnSpeedUp;
        Game.GameOver += OnGameOver;
        GameInput.Instance.Pause += OnPause;
    }

    public void Shutdown()
    {
        UI.UI.RestartClicked -= OnRestart;
        Game.SpeedUp -= OnSpeedUp;
        Game.GameOver -= OnGameOver;
        GameInput.Instance.Pause -= OnPause;
    }

    public void Slide(float sign)
    {
        if (m_sliding)
            return;

        float currentX = m_rigidbody.position.x;
        float targetX = currentX + sign * m_slideStep;
        float t = Time.fixedDeltaTime * m_lerpFactor;

        m_slideLerper.Initialize(currentX, targetX, t);
        m_sliding = true;
    }

    public void InitiateExplode(Vector3 from)
    {
        float explosionRadius = 1f;
        from.y = m_rigidbody.position.y;

        m_rigidbody.constraints = RigidbodyConstraints.None;
        m_rigidbody.AddExplosionForce(m_collisionForceStrength, from, explosionRadius);
    }

    public float Move()
    {
        m_velocity = Vector3.forward * m_speed * Time.fixedDeltaTime;
        m_rigidbody.velocity = m_velocity;

        m_movedDistance += m_rigidbody.position.z - m_lastPosition.z;
        m_lastPosition = m_rigidbody.position;

        if (!m_sliding)
            return m_movedDistance;

        Vector3 newPosition = m_rigidbody.position;
        newPosition.x = m_slideLerper.Lerp();

        if (m_slideLerper.End)
        {
            newPosition.x = m_slideLerper.Value;
            m_sliding = false;
        }

        m_rigidbody.position = newPosition;

        return m_movedDistance;
    }

    private void OnSpeedUp(float speed)
    {
        m_speed = speed;
    }

    private void OnPause(bool status)
    {
        if (status)
        {
            m_lastVelocity = m_rigidbody.velocity;
            m_rigidbody.velocity = Vector3.zero;
        }
        else
            m_rigidbody.velocity = m_lastVelocity;
    }

    private void OnRestart()
    {
        OnGameOver(m_player);
    }

    private void OnGameOver(Player player)
    {
        m_rigidbody.velocity = m_rigidbody.velocity / 5;
        Game.GameOver -= OnGameOver;
        Game.SpeedUp -= OnSpeedUp;
        UI.UI.RestartClicked -= OnRestart;
    }
}
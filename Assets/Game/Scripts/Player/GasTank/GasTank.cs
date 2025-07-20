public class GasTank
{
    private float m_capacity;
    private float m_current;
    private float m_baseConsumption;

    private event System.Action m_fuelOver;

    public event System.Action FuelOver 
    {
        add
        {
            m_fuelOver -= value;
            m_fuelOver += value;
        }
        remove
        {
            m_fuelOver -= value;
        }
    }

    public GasTank(PlayerConfig config)
    {
        m_capacity = m_current = config.Fuel;
        m_baseConsumption = config.FuelConsumption;
    }

    public void Fill(float amount)
    {
        if (amount < 0)
            throw new System.ArgumentException();

        float available = m_capacity - m_current;

        m_current += available < amount ? available : amount;
    }

    public void Spend()
    {
        if (m_current <= 0)
            return;

        m_current -= m_baseConsumption;

        if (m_current <= 0)
            m_fuelOver?.Invoke();
    }
}

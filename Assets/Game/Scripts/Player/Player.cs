using UnityEngine;
using Input;
using UI;

public class Player : MonoBehaviour
{
    private static event System.Action<Player> m_wasted;

    private float m_movedDistance;
    private HUD m_hud;
    private PlayerData m_data;
    private PlayerMovement m_movement;
    private PlayerCollision m_collision;
    private CarModel m_carModel;
    private GasTank m_gasTank;

    public static event System.Action<Player> Wasted
    {
        add
        {
            m_wasted -= value;
            m_wasted += value;
        }
        remove
        {
            m_wasted -= value;
        }
    }

    public float MovedDistance => m_movedDistance;
    public PlayerConfig Config => m_carModel.CarConfiguration;
    public CarModel CarModel => m_carModel;

    private void FixedUpdate()
    {
        m_movedDistance = m_movement.Move();
        m_hud.UpdateScore(m_movedDistance);
        m_gasTank.Spend();
    }

    public void SetPlayerData(PlayerData data)
    {
        m_data = data;
        m_data.SkinSelected += OnSkinSelected;
    }

    public void Initialize(GameConfig gameConfig) 
    {
        m_movement = new PlayerMovement();

        if (m_collision == null && !TryGetComponent(out m_collision))
            m_collision = gameObject.AddComponent<PlayerCollision>();

        m_collision.Initialize();

        m_gasTank = new GasTank(m_carModel.CarConfiguration);
        m_movement.Initialize(gameConfig, this);

        if (m_hud is null)
            m_hud = FindAnyObjectByType<HUD>();

        m_hud.Initialize(m_data);

        GameInput.Instance.Slide += OnSlide;
        GameInput.Instance.Pause += OnPause;
        m_gasTank.FuelOver += OnFuelOver;
        m_collision.Collide += OnCollide;

        enabled = true;
    }

    public void Fill(float fuelAmount) =>
        m_gasTank.Fill(fuelAmount);

    private void OnSkinSelected(SkinItem skinItemSO)
    {
        if (m_carModel == skinItemSO.Model)
            return;

        if (m_carModel != null)
            Destroy(m_carModel.gameObject);

        m_carModel = Instantiate(skinItemSO.Model, transform);
        m_carModel.SetDefaultLayer();
    }

    private void OnSlide(float sign)
    {
        m_movement.Slide(sign);
    }

    private void OnPause(bool status)
    {
        enabled = !status;
    }

    private void OnCollide(Collision collision)
    {
        m_gasTank.FuelOver -= OnFuelOver;
        m_collision.Collide -= OnCollide;

        m_wasted?.Invoke(this);

        m_movement.InitiateExplode(collision.GetContact(0).point);
        m_collision.enabled = false;
        enabled = false;
    }

    private void OnFuelOver()
    {
        m_gasTank.FuelOver -= OnFuelOver;
        m_collision.Collide -= OnCollide;

        m_wasted?.Invoke(this);
        enabled = false;
    }
}
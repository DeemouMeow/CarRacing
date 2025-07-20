using UnityEngine;
using Cinemachine;
using Input;

public class Bootstrap : MonoBehaviour
{
    [SerializeField] private Player m_player;
    [SerializeField] private UI_Bootstrap m_uiBootstrap;
    [SerializeField] private Game m_game;
    [SerializeField] private GameConfig m_gameConfig;
    [SerializeField] private CinemachineVirtualCamera m_playerVirtualCamera;

    private static bool m_restarted;

    private ChunkSpawner m_chunkSpawner;
    private ObstacleSpawner m_obstacleSpawner;
    private GameInput m_gameInput;
    private DataProvider m_dataProvider;
    private PlayerData m_playerData;

    private void Awake()
    {
        DataInit();
        m_player.SetPlayerData(m_playerData);

        InputInit();
        m_uiBootstrap.Initialize(m_playerData, m_dataProvider);
        ChunkInit();

        UI.UI.RestartClicked += OnRestart;
        UI.UI.StartClicked += OnStart;
        UI.UI.ExitClicked += OnExit;

        if (m_restarted)
        {
            m_restarted = false;
            OnStart();
        }
    }

    private void OnApplicationQuit()
    {
        OnExit();
    }

    private void DataInit()
    {
        m_dataProvider = new DataProvider();

        m_playerData = m_dataProvider.TryLoad(PlayerData.CreateInstance);
    }

    private void InputInit()
    {
        m_gameInput = new GameInput();
        m_gameInput.Initialize();
    }

    private void ChunkInit()
    {
        m_chunkSpawner = new ChunkSpawner();
        m_obstacleSpawner = new ObstacleSpawner();
        m_obstacleSpawner.Initialize(m_gameConfig.ChunkConfig);
        m_chunkSpawner.Initialize(m_gameConfig.ChunkConfig, m_obstacleSpawner);
    }

    private void PlayerInit()
    {
        m_playerVirtualCamera.Follow = m_player.transform;
        m_player.Initialize(m_gameConfig);
    }

    private void OnStart()
    {
        m_game.Initialize(m_gameConfig);
        PlayerInit();
        m_playerVirtualCamera.gameObject.SetActive(true);

        UI.UI.StartClicked -= OnStart;
    }

    private void OnRestart()
    {
        m_restarted = true;

        SceneLoader.Restart();

        UI.UI.RestartClicked -= OnRestart;
    }

    private void OnExit()
    {
        m_dataProvider.Save(m_playerData);

        UI.UI.ExitClicked -= OnExit;
    }
}

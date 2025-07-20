using UnityEngine;
using UI;

public class UI_Bootstrap : MonoBehaviour
{
    [SerializeField] private PauseMenu m_pauseMenu;
    [SerializeField] private EndGameMenu m_endGameMenu;
    [SerializeField] private StartMenu m_startMenu;
    [SerializeField] private Shop m_shop;

    public void Initialize(PlayerData playerData, DataProvider dataProvider)
    {
        m_pauseMenu.Initialize();
        m_endGameMenu.Initialize();
        m_startMenu.Initialize();
        m_shop.Initialize(playerData, dataProvider);
    }
}

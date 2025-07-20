using TMPro;
using UnityEngine;

namespace UI
{
    public class HUD : UI
    {
        [SerializeField] private TMP_Text m_scoreText;

        private PlayerData m_playerData;
        private int m_hundredsCount;

        public int HundredsCount => int.Parse(m_scoreText.text) >= 100 ? m_hundredsCount - 1 : 0;

        private void Awake()
        {
            m_scoreText.gameObject.SetActive(false);
        }

        public void Initialize(PlayerData data)
        {
            m_scoreText.gameObject.SetActive(true);
            m_playerData = data;
            Game.GameOver += OnGameOver;

            m_hundredsCount = 1;
        }

        public void UpdateScore(float value)
        {
            if (value > 100 * m_hundredsCount)
                m_hundredsCount++;

            m_scoreText.text = ((int)value).ToString();
        }

        private void OnGameOver(Player player)
        {
            Game.GameOver -= OnGameOver;

            m_playerData.AddCoins(HundredsCount);
        }
    }
}
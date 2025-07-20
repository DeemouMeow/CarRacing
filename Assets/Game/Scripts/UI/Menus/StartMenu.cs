using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class StartMenu : UI
    {
        [SerializeField] private Button m_start;
        [SerializeField] private Button m_shop;
        [SerializeField] private Button m_exit;

        private static bool m_restarted;

        public void Initialize()
        {
            m_start.onClick.AddListener(InitiateStart);
            m_shop.onClick.AddListener(InitiateShop);
            m_exit.onClick.AddListener(InitiateExit);

            RestartClicked += OnRestart;
            StartClicked += OnStart;

            if (m_restarted)
            {
                m_restarted = false;
                OnStart();
            }
        }

        private void OnRestart()
        {
            m_restarted = true;
            gameObject.SetActive(false);

            RestartClicked -= OnRestart;
        }

        private void OnStart()
        {
            gameObject.SetActive(false);

            StartClicked -= OnStart;
        }
    }
}


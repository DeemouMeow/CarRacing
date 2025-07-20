using UnityEngine;

namespace UI
{
    using UnityEngine.UI;
    using Input;

    public class PauseMenu : UI
    {
        [SerializeField] private Button m_continue;
        [SerializeField] private Button m_restart;
        [SerializeField] private Button m_exit;

        private void OnDestroy()
        {
            m_continue.onClick.RemoveListener(InitiateContinue);
            m_restart.onClick.RemoveListener(InitiateRestart);
            m_exit.onClick.RemoveListener(InitiateExit);
        }

        public void Initialize()
        {
            gameObject.SetActive(false);

            m_continue.onClick.AddListener(InitiateContinue);
            m_restart.onClick.AddListener(InitiateRestart);
            m_exit.onClick.AddListener(InitiateExit);
            GameInput.Instance.Pause += (bool status) => gameObject.SetActive(status);
        }
    }
}
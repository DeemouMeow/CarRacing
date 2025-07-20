namespace UI
{
    using System;
    using UnityEngine;

    public abstract class UI : MonoBehaviour
    {
        private static event Action m_startClicked;
        private static event Action m_shopClicked;
        private static event Action m_shopClosed;
        private static event Action m_continueClicked;
        private static event Action m_restartClicked;
        private static event Action m_exitClicked;

        public static event Action StartClicked
        {
            add
            {
                m_startClicked -= value;
                m_startClicked += value;
            }
            remove
            {
                m_startClicked -= value;
            }
        }
        public static event Action ShopClicked
        {
            add
            {
                m_shopClicked -= value;
                m_shopClicked += value;
            }
            remove
            {
                m_shopClicked -= value;
            }
        }
        public static event Action ShopClosed
        {
            add
            {
                m_shopClosed -= value;
                m_shopClosed += value;
            }
            remove
            {
                m_shopClosed -= value;
            }
        }

        public static event Action ContinueClicked
        {
            add
            {
                m_continueClicked -= value;
                m_continueClicked += value;
            }
            remove
            {
                m_continueClicked -= value;
            }
        }
        public static event Action RestartClicked
        {
            add
            {
                m_restartClicked -= value;
                m_restartClicked += value;
            }
            remove
            {
                m_restartClicked -= value;
            }
        }
        public static event Action ExitClicked
        {
            add
            {
                m_exitClicked -= value;
                m_exitClicked += value;
            }
            remove
            {
                m_exitClicked -= value;
            }
        }

        protected virtual void InitiateStart() =>
            m_startClicked?.Invoke();

        protected virtual void InitiateShop() =>
            m_shopClicked?.Invoke();

        protected virtual void InitiateShopClosed() =>
            m_shopClosed?.Invoke();

        protected virtual void InitiateContinue() =>
            m_continueClicked?.Invoke();

        protected virtual void InitiateRestart() =>
            m_restartClicked?.Invoke();

        protected virtual void InitiateExit() =>
            m_exitClicked?.Invoke();
    }
}


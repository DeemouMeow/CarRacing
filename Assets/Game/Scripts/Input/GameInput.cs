namespace Input 
{
    using UnityEngine.InputSystem;
    using System.Collections.Generic;
    using UI;

    public class GameInput
    {
        private InputMaps m_inputMaps;
        private InputEvents m_inputEvents;

        private class InputEvents
        {
            private List<System.Action<float>> m_slideCallbacksCash;
            private List<System.Action<bool>> m_pauseCallbacksCash;

            private event System.Action<float> m_slide;
            private event System.Action<bool> m_pause;

            private bool m_isPause;

            public InputEvents()
            {
                m_slideCallbacksCash = new List<System.Action<float>>();
                m_pauseCallbacksCash = new List<System.Action<bool>>();
            }

            public event System.Action<float> Slide
            {
                add => SubscribeSlide(value);
                remove => UnsubscribeSlide(value);
            }
            public event System.Action<bool> Pause
            {
                add => SubscribePause(value);
                remove => UnsubscribePause(value);
            }

            public void TriggerMove(float direction) =>
                m_slide?.Invoke(direction);

            public void TriggerPause()
            {
                m_isPause = !m_isPause;
                m_pause?.Invoke(m_isPause);
            }

            public void UnsubscribeAll()
            {
                if (m_slide is not null)
                {
                    foreach (var method in m_slide?.GetInvocationList())
                        Slide -= (System.Action<float>)method;

                    m_slideCallbacksCash.Clear();
                }

                if (m_pause is not null)
                {
                    foreach (var method in m_pause?.GetInvocationList())
                        Pause -= (System.Action<bool>)method;

                    m_pauseCallbacksCash.Clear();
                }

            }

            private void SubscribeSlide(System.Action<float> callback) 
            {
                if (m_slideCallbacksCash.Contains(callback))
                    return;

                m_slideCallbacksCash.Add(callback);
                m_slide += callback;

            }

            private void UnsubscribeSlide(System.Action<float> callback)
            {
                m_slideCallbacksCash.Remove(callback);
                m_slide -= callback;
            }

            private void SubscribePause(System.Action<bool> callback)
            {
                if (m_pauseCallbacksCash.Contains(callback))
                    return;

                m_pauseCallbacksCash.Add(callback);
                m_pause += callback;

            }

            private void UnsubscribePause(System.Action<bool> callback)
            {
                m_pauseCallbacksCash.Remove(callback);
                m_pause -= callback;
            }
        }

        public static GameInput Instance { get; private set; }

        public event System.Action<float> Slide
        {
            add => m_inputEvents.Slide += value;
            remove => m_inputEvents.Slide -= value;
        }
        public event System.Action<bool> Pause
        {
            add => m_inputEvents.Pause += value;
            remove => m_inputEvents.Pause -= value;
        }

        public void Initialize()
        {
            if (Instance == null)
            {
                m_inputMaps = new InputMaps();
                m_inputEvents = new InputEvents();
                Instance = this;
                Enable();
            }
        }

        private void Enable()
        {
            m_inputMaps.Enable();
            m_inputMaps.Movement.Side.performed += OnMoveTriggered;
            m_inputMaps.State.Pause.performed += OnPauseTriggered;

            UI.ContinueClicked += UI_OnContinue;
            UI.RestartClicked += UI_Restart;
            UI.ExitClicked += UI_Exit;
        }

        private void Disable()
        {
            m_inputMaps.Disable();
            m_inputMaps.Movement.Side.performed -= OnMoveTriggered;
            m_inputMaps.State.Pause.performed -= OnPauseTriggered;

            UI.ContinueClicked -= UI_OnContinue;
            UI.RestartClicked -= UI_Restart;
            UI.ExitClicked -= UI_Exit;
        }

        private void UI_Restart()
        {
            Disable();
            m_inputEvents.UnsubscribeAll();

            Instance = null;
        }

        private void UI_OnContinue() =>
            m_inputEvents.TriggerPause();

        private void UI_Exit()
        {
            Disable();
            m_inputEvents.UnsubscribeAll();

            SceneLoader.Exit();
            Instance = null;
        }

        private void OnMoveTriggered(InputAction.CallbackContext callbackContext)
        {
            m_inputEvents.TriggerMove(callbackContext.ReadValue<UnityEngine.Vector2>().x);
        }

        private void OnPauseTriggered(InputAction.CallbackContext callbackContext)
        {
            m_inputEvents.TriggerPause();
        }
    }
}
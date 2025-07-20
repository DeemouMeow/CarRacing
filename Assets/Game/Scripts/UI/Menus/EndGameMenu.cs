using UnityEngine;

namespace UI
{
    using System.Collections;
    using TMPro;
    using UnityEngine.EventSystems;
    using UnityEngine.UI;

    public class EndGameMenu : UI, IPointerClickHandler
    {
        private static float k_showDelay = 1f;

        [SerializeField] private CarModelPodium m_carModelPodium;
        [SerializeField] private Button m_restart;
        [SerializeField] private Button m_shop;
        [SerializeField] private Button m_exit;
        [SerializeField] private TMP_Text m_scoreText;

        private bool m_shopDisplayed;
        private bool m_scoreDisplaying;
        private Player m_wastedPlayer;

        private void OnDestroy()
        {
            StopAllCoroutines();

            m_restart.onClick.RemoveListener(InitiateRestart);
            m_shop.onClick.RemoveListener(InitiateShop);
            m_exit.onClick.RemoveListener(InitiateExit);

            ShopClosed -= OnShopClosed;
            Game.GameOver -= OnGameOver;

            m_wastedPlayer = null;
        }

        public void Initialize()
        {
            m_restart.onClick.AddListener(InitiateRestart);
            m_shop.onClick.AddListener(InitiateShop);
            m_exit.onClick.AddListener(InitiateExit);

            Game.GameOver += OnGameOver;
        }

        private void Show()
        {
            m_carModelPodium.SetModel(m_wastedPlayer.CarModel);

            gameObject.SetActive(true);

            StartCoroutine(ScoreCoroutine(m_wastedPlayer.MovedDistance));
        }

        protected override void InitiateShop()
        {
            base.InitiateShop();

            ShopClosed += OnShopClosed;
            m_shopDisplayed = true;
            gameObject.SetActive(false);
        }

        private void OnShopClosed()
        {
            if (m_shopDisplayed)
            {
                m_carModelPodium.SetModel(m_wastedPlayer.CarModel);
                ShopClosed -= OnShopClosed;
                m_shopDisplayed = false;
                gameObject.SetActive(true);
            }
        }

        private void OnGameOver(Player player)
        {
            m_wastedPlayer = player;

            Invoke(nameof(Show), k_showDelay);

            Game.GameOver -= OnGameOver;
        }

        private IEnumerator ScoreCoroutine(float score)
        {
            if (m_wastedPlayer is null)
                StopCoroutine(ScoreCoroutine(score));

            int intScore = (int)score;
            int counter = 0;

            m_scoreText.text = counter.ToString();
            m_scoreDisplaying = true;

            yield return new WaitForSeconds(k_showDelay);

            while (counter != intScore && m_scoreDisplaying)
            {
                counter++;
                m_scoreText.text = counter.ToString();
                yield return null;
            }

            m_scoreText.text = ((int)m_wastedPlayer.MovedDistance).ToString();
            m_scoreDisplaying = false;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            m_scoreDisplaying = false;
        }
    }
}

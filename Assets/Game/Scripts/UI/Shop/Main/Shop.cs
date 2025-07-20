using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace UI
{
    public class Shop : UI
    {
        [SerializeField] private UnityEngine.UI.Button m_closeButton;
        [SerializeField] private ShopItemListView m_itemsListView;
        [SerializeField] private ShopItemsList m_itemsList;
        [SerializeField] private CarModelPodium m_carModelPodium;
        [SerializeField] private IntValueView m_coinsValueView;

        private OpenSkinsChecker m_openSkinsChecker;
        private SelectedSkinChecker m_selectedSkinChecker;
        private SkinSelector m_skinSelector;
        private SkinUnlocker m_skinUnlocker;

        private IEnumerable<SkinItem> Skins => m_itemsList.SkinsList;

        private DataProvider m_dataProvider;
        private PlayerData m_playerData;
        private bool m_initialized;

        public void Initialize(PlayerData playerData, DataProvider dataProvider)
        {
            if (m_initialized)
                return;

            m_openSkinsChecker = new OpenSkinsChecker(playerData);
            m_selectedSkinChecker = new SelectedSkinChecker(playerData);
            m_skinSelector = new SkinSelector(playerData);
            m_skinUnlocker = new SkinUnlocker(playerData);

            m_dataProvider = dataProvider;
            m_playerData = playerData;
            m_itemsListView.ShopItemViewClicked += OnShopItemViewClicked;
            m_itemsListView.Initialize(Skins, m_openSkinsChecker, m_selectedSkinChecker);

            ShopClicked += OnShopClicked;
            RestartClicked += OnRestartClicked;
            m_closeButton.onClick.AddListener(InitiateShopClosed);

            gameObject.SetActive(false);
            m_initialized = true;
        }

        protected override void InitiateShopClosed()
        {
            base.InitiateShopClosed();

            m_itemsListView.Hide();
            m_itemsListView.ShopItemViewClicked -= OnShopItemViewClicked;

            gameObject.SetActive(false);
        }

        private void OnShopClicked()
        {
            m_itemsListView.ShopItemViewClicked += OnShopItemViewClicked;
            m_itemsListView.Show();
            m_coinsValueView.Show(m_playerData.Coins);

            gameObject.SetActive(true);
        }

        private void OnRestartClicked()
        {
            m_dataProvider.Save(m_playerData);

            m_itemsListView.ShopItemViewClicked -= OnShopItemViewClicked;
            ShopClicked -= OnShopClicked;
            RestartClicked -= OnRestartClicked;
        }

        private void OnShopItemViewClicked(ShopItemView shopItemView)
        {
            if (shopItemView is null)
                return;

            m_selectedSkinChecker.Visit(shopItemView.ShopItemData);

            if (m_selectedSkinChecker.IsSelected)
            {
                m_skinSelector.Visit(shopItemView.ShopItemData);
                m_carModelPodium.SetModel((shopItemView.ShopItemData as SkinItem).Model);
                return;
            }

            m_openSkinsChecker.Visit(shopItemView.ShopItemData);

            if (!m_openSkinsChecker.IsOpen)
            {
                m_skinUnlocker.Visit(shopItemView.ShopItemData);
                m_openSkinsChecker.Visit(shopItemView.ShopItemData);
            }

            if (m_openSkinsChecker.IsOpen)
            {
                m_skinSelector.Visit(shopItemView.ShopItemData);
                m_carModelPodium.SetModel((shopItemView.ShopItemData as SkinItem).Model);
                m_itemsListView.SelectItem(shopItemView);
            }

            m_coinsValueView.Show(m_playerData.Coins);
            m_dataProvider.Save(m_playerData);
        }
    }
}
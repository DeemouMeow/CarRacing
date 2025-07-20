using System.Collections.Generic;
using UnityEngine;

public class ShopItemListView : MonoBehaviour
{
    [SerializeField] private ShopItemViewFactory m_shopItemViewFactory;
    [SerializeField] private RectTransform m_contentParent;

    private event System.Action<ShopItemView> m_shopItemViewClicked;
    private IEnumerable<SkinItem> m_skins;
    private List<ShopItemView> m_rendered;

    private OpenSkinsChecker m_openSkinsChecker;
    private SelectedSkinChecker m_selectedSkinChecker;

    public event System.Action<ShopItemView> ShopItemViewClicked
    {
        add
        {
            m_shopItemViewClicked -= value;
            m_shopItemViewClicked += value;
        }
        remove
        {
            m_shopItemViewClicked -= value;
        }
    }

    public void Initialize(IEnumerable<SkinItem> skinsList, OpenSkinsChecker openSkinsChecker, SelectedSkinChecker selectedSkinChecker)
    {
        m_skins = skinsList;
        m_openSkinsChecker = openSkinsChecker;
        m_selectedSkinChecker = selectedSkinChecker;
        m_shopItemViewFactory.Initialize();

        Render();
    }

    public void Show()
    {
        Render();

        gameObject.SetActive(true);
    }

    public void SelectItem(ShopItemView shopItem)
    {
        if (m_rendered != null && m_rendered.Count > 0)
        {
            foreach (var rendered in m_rendered)
                rendered.Unselect();

            shopItem.Select();
        }
    }

    public void Hide()
    {
        Clear();

        gameObject.SetActive(false);
    }

    private void Clear()
    {
        if (m_rendered == null || m_rendered.Count == 0)
            return;

        foreach (var item in m_rendered)
            Destroy(item.gameObject);

        m_rendered.Clear();
    }

    private void Render()
    {
        Clear();

        m_rendered = new List<ShopItemView>();

        foreach (ShopItem item in m_skins)
        {
            ShopItemView shopItem = m_shopItemViewFactory.Get(item, m_contentParent);
            shopItem.ShopItemClicked += OnShopItemClicked;

            shopItem.Lock();
            shopItem.Unselect();

            m_openSkinsChecker.Visit(item);

            if (m_openSkinsChecker.IsOpen)
            {
                m_selectedSkinChecker.Visit(item);

                if (m_selectedSkinChecker.IsSelected)
                {
                    shopItem.Select();
                    OnShopItemClicked(shopItem);
                }

                shopItem.Unlock();
            }

            m_rendered.Add(shopItem);
        }
    }

    private void OnShopItemClicked(ShopItemView shopItem)
    {
        if (shopItem is null)
            return;

        m_shopItemViewClicked?.Invoke(shopItem);
    }
}
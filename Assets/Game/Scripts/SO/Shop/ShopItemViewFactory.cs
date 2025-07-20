using UnityEngine;

[CreateAssetMenu(fileName = "Shop Item View Factory", menuName = "Shop/Factories/Shop Item View Factory")]
public class ShopItemViewFactory : ScriptableObject
{
    [SerializeField] private ShopItemView m_skinItemViewPrefab;

    private ShopItemVisitor m_shopItemVisitor;

    private class ShopItemVisitor
    {
        private ShopItemView m_skinItemView;

        public ShopItemVisitor(ShopItemView skinItemView)
        {
            m_skinItemView = skinItemView;
        }

        public ShopItemView Prefab { get; private set; }

        public void Visit(ShopItem shopItem)
        {
            SkinItem skinItemSO = shopItem as SkinItem;

            if (skinItemSO != null)
            {
                Visit(skinItemSO);
                return;
            }
        }

        public void Visit(SkinItem skinItem) =>
            Prefab = m_skinItemView;
    }

    public void Initialize()
    {
        m_shopItemVisitor = new ShopItemVisitor(m_skinItemViewPrefab);
    }

    public ShopItemView Get(ShopItem shopItem, Transform parent)
    {
        m_shopItemVisitor.Visit(shopItem);

        ShopItemView shopItemView = Instantiate(m_shopItemVisitor.Prefab, parent);
        shopItemView.Initialize(shopItem);

        return shopItemView;
    }
}
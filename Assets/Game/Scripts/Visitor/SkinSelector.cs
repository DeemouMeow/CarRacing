public class SkinSelector : IShopItemVisitor
{
    private PlayerData m_playerData;

    public SkinSelector(PlayerData playerData)
    {
        m_playerData = playerData;
    }

    public bool Success { get; private set; }

    public void Visit(ShopItem shopItemSO)
    {
        SkinItem skinItem = shopItemSO as SkinItem;

        if (skinItem != null)
            Visit(skinItem);
    }

    public void Visit(SkinItem skinItem) =>
        Success = m_playerData.SelectSkin(skinItem, true);
}

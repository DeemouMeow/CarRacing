public class OpenSkinsChecker : IShopItemVisitor
{
    private PlayerData m_playerData;

    public OpenSkinsChecker(PlayerData playerData)
    {
        m_playerData = playerData;
    }

    public bool IsOpen { get; private set; }

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
        IsOpen = m_playerData.IsSkinOpen(skinItem.SkinType);
}

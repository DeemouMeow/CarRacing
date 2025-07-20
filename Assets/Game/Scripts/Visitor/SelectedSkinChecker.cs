public class SelectedSkinChecker : IShopItemVisitor
{
    private PlayerData m_playerData;

    public SelectedSkinChecker(PlayerData playerData)
    {
        m_playerData = playerData;
    }

    public bool IsSelected { get; private set; }

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
        IsSelected = m_playerData.SelectedSkin == skinItem.SkinType;
}

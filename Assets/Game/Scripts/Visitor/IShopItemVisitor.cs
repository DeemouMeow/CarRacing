public interface IShopItemVisitor
{
    public void Visit(ShopItem shopItem)
    {
        SkinItem skinItem = shopItem as SkinItem;

        if (skinItem != null)
            Visit(skinItem);
    }

    public void Visit(SkinItem skinItem);
}

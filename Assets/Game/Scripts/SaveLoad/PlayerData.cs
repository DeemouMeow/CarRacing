using System.Collections.Generic;
using Newtonsoft.Json;

[System.Serializable]
public class PlayerData
{
    private int m_coins;
    private SkinType m_selectedSkin;
    private List<SkinType> m_ownedSkins;

    private event System.Action<SkinItem> m_skinSelected;

    public PlayerData()
    {
        m_coins = 0;
        m_selectedSkin = SkinType.Default;
        m_ownedSkins = new List<SkinType>() { 0 };
    }

    [JsonConstructor]
    public PlayerData(int coins, SkinType selectedSkin, List<SkinType> ownedSkins)
    {
        m_coins = coins;
        m_selectedSkin = selectedSkin;
        m_ownedSkins = ownedSkins;
    }

    public int Coins => m_coins;
    public SkinType SelectedSkin => m_selectedSkin;
    public IEnumerable<SkinType> OwnedSkins => m_ownedSkins;

    public event System.Action<SkinItem> SkinSelected
    {
        add
        {
            m_skinSelected -= value;
            m_skinSelected += value;
        }
        remove => m_skinSelected -= value;
    }

    public static PlayerData CreateInstance() =>
        new PlayerData();

    public bool SelectSkin(SkinItem skinItem, bool forecefully = false)
    {
        if (forecefully || SelectedSkin != skinItem.SkinType)
        {
            m_skinSelected?.Invoke(skinItem);
            m_selectedSkin = skinItem.SkinType;

            return true;
        }

        return false;
    }

    public bool IsSkinOpen(SkinType skin) =>
        m_ownedSkins.Contains(skin);

    public bool TryOpenSkin(SkinItem skinItem)
    {
        if (m_ownedSkins.Contains(skinItem.SkinType))
            return false;

        if (skinItem.Cost < 0 || Coins < skinItem.Cost)
            return false;

        m_coins -= skinItem.Cost;
        m_ownedSkins.Add(skinItem.SkinType);

        SelectSkin(skinItem, true);

        return true;
    }

    public void AddCoins(int value)
    {
        if (value < 0)
        {
            m_coins += 0;
            return;
        }

        m_coins += value;
    }

    public override string ToString()
    {
        return $"Coins: {Coins}; Selected Skin {SelectedSkin}, Opened Skins Count: {m_ownedSkins.Count}";
    }
}

using UnityEngine;

[CreateAssetMenu(fileName = "Shop Item", menuName = "Shop/Items Data/Shop Item")]
public class ShopItem : ScriptableObject
{
    [SerializeField] private string m_name;
    [SerializeField] private Sprite m_icon;
    [SerializeField] private int m_cost;

    public string Name => m_name;
    public int Cost => m_cost;
    public Sprite Icon => m_icon;
}

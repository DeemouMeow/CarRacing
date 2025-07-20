using UnityEngine;

[CreateAssetMenu(fileName = "Skin Item", menuName = "Shop/Items Data/Skin Item")]
public class SkinItem : ShopItem
{
    [SerializeField] private SkinType m_skinType;
    [SerializeField] private CarModel m_model;

    public SkinType SkinType => m_skinType;
    public CarModel Model => m_model;
}

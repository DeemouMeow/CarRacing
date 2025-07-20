using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Items List", menuName = "Shop/Lists/Items List")]
public class ShopItemsList : ScriptableObject
{
    [SerializeField] private List<SkinItem> m_skinsList;

    public IEnumerable<SkinItem> SkinsList => m_skinsList;
}
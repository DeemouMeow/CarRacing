using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class ShopItemView : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [SerializeField] private Image m_iconImage;
    [SerializeField] private TMP_Text m_nameText;
    [SerializeField] private LockedPanel m_lockedPanel;
    [SerializeField] private GameObject m_hoverObject;
    [SerializeField] private GameObject m_selectedPanel;

    private ShopItem m_shopItemSO;
    private bool m_locked;
    private bool m_selected;
    private event System.Action<ShopItemView> m_shopItemClicked;

    public ShopItem ShopItemData => m_shopItemSO;
    public event System.Action<ShopItemView> ShopItemClicked
    {
        add
        {
            m_shopItemClicked -= value;
            m_shopItemClicked += value;
        }
        remove
        {
            m_shopItemClicked -= value;
        }
    }

    public void Initialize(ShopItem shopItemSO)
    {
        m_shopItemSO = shopItemSO;

        m_iconImage.sprite = m_shopItemSO.Icon;
        m_nameText.text = m_shopItemSO.Name;
    }

    public void Select()
    {
        m_selected = true;
        m_lockedPanel.Hide();

        m_hoverObject.SetActive(false);
        m_selectedPanel.SetActive(true);
    }

    public void Unselect()
    {
        m_selected = false;
        m_selectedPanel.SetActive(false);
    }

    public void Lock()
    {
        m_locked = true;
        m_lockedPanel.Show(m_shopItemSO.Cost);
    }

    public void Unlock()
    {
        m_locked = false;
        m_lockedPanel.Hide();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        m_shopItemClicked?.Invoke(this);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (m_locked || m_selected)
            return;
        
        m_hoverObject.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        m_hoverObject.SetActive(false);
    }
}

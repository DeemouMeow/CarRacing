using TMPro;
using UnityEngine;

public class LockedPanel : MonoBehaviour
{
    private const string k_defaultText = "unset";

    [SerializeField] private TMP_Text m_costText;

    public void Show(int value)
    {
        m_costText.text = value.ToString();

        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);

        m_costText.text = k_defaultText;
    }
}

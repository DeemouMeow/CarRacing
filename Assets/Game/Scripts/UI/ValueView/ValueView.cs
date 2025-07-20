using TMPro;
using UnityEngine;

public abstract class ValueView<T> : MonoBehaviour
{
    [SerializeField] protected TMP_Text m_valueText;

    public abstract void Show(T value);
}

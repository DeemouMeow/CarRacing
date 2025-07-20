public class IntValueView : ValueView<int>
{
    public override void Show(int value)
    {
        m_valueText.text = value.ToString();
    }
}

using UnityEngine;

public class CarModelPodium : MonoBehaviour
{
    private CarModel m_currentModel;

    public void SetModel(CarModel model)
    {
        if (m_currentModel != null)
            Destroy(m_currentModel.gameObject);

        m_currentModel = Instantiate(model, transform);
        m_currentModel.SetUILayer();
    }
}

using UnityEngine;

public class CarModel : MonoBehaviour
{
    private const int k_defaultLayer = 0;
    private const int k_uiLayer = 5;

    [SerializeField] private PlayerConfig m_carConfiguration;

    public PlayerConfig CarConfiguration => m_carConfiguration;

    public void SetUILayer()
    {
        gameObject.layer = k_uiLayer;

        foreach (Transform transform in GetComponentsInChildren<Transform>())
            transform.gameObject.layer = k_uiLayer;
    }

    public void SetDefaultLayer()
    {
        gameObject.layer = k_defaultLayer;

        foreach (Transform transform in GetComponentsInChildren<Transform>())
            transform.gameObject.layer = k_defaultLayer;
    }
}

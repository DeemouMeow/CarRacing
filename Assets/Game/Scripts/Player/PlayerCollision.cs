using UnityEngine;

[RequireComponent(typeof(BoxCollider), typeof(Rigidbody))]
public class PlayerCollision : MonoBehaviour
{
    private const string k_groundLayer = "Ground";

    private event System.Action<Collision> m_collide;

    public event System.Action<Collision> Collide
    {
        add
        {
            m_collide -= value;
            m_collide += value;
        }
        remove 
        { 
            m_collide -= value;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer != LayerMask.NameToLayer(k_groundLayer))
            m_collide?.Invoke(collision);
    }

    public void Initialize()
    {
        BoxCollider collider = GetComponent<BoxCollider>();

        collider.size = new Vector3(0.7f, 0.4f, 1f);
    }

    public Collider SideCollision(float sign)
    {
        Vector3 rayDirection = new Vector3(sign, 0f, 0f);
        float trackWidth = 1.5f;

        Ray ray = new Ray(transform.position, rayDirection);

        Physics.Raycast(ray, out RaycastHit hit, trackWidth);

        return hit.collider;
    }
}

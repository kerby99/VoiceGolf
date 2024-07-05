using UnityEngine;

public class HoleController : MonoBehaviour
{
    [SerializeField]
    private float attractionForce = 10.0f;

    private Collider vortexCollider;

    private void Awake()
    {
        vortexCollider = GetComponent<Collider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Ball"))
        {
            Debug.Log("La balle est dans le trou!");
            other.gameObject.SetActive(false);
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Ball"))
        {
            Vector3 normal = other.transform.position - vortexCollider.bounds.center;
            other.attachedRigidbody.AddForce(normal * attractionForce);
        }
    }
}

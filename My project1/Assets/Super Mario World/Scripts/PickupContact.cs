using UnityEngine;

public class PickupContact : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Destroy(gameObject);

            Debug.Log("Collectible collected by player");
        }
    }
}

using UnityEngine;
using UnityEngine.Audio;

public class InfinitePlatform : MonoBehaviour
{
    public Transform player;     // Assign your player transform in the inspector
    public float platformLength = 100f;  // Not strictly needed anymore unless you use it for something else

    private bool canMove = false;

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            canMove = true;
        }
    }

    void Update()
    {
        if (canMove)
        {
            // Move platform center to player's current x position
            transform.position = new Vector3(player.position.x, transform.position.y, transform.position.z);
            canMove = false;
        }
    }
}

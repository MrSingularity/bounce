using UnityEngine;

public class InfinitePlanes : MonoBehaviour
{
    public Transform player;     // Assign your player transform in the inspector

    void Update()
    {
        transform.position = new Vector3(player.position.x, transform.position.y, transform.position.z);
    }
}
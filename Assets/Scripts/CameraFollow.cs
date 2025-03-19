using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;

    private float fixedY;
    private float fixedZ;

    void Start()
    {
        if (player == null)
        {
            Debug.LogError("Player Transform is not assigned!");
            return;
        }

        fixedY = transform.position.y;
        fixedZ = transform.position.z;
    }

    void LateUpdate()
    {
        if (player != null)
        {
            transform.position = new Vector3(player.position.x, fixedY, fixedZ);
        }        
    }
}

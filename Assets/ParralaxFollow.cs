using UnityEngine;

public class ParallaxFollow : MonoBehaviour
{
    public Transform player;
    public float parallaxFactor = 0.3f;
    private Vector3 startPos;

    void Start()
    {
        startPos = transform.position;
    }

    void LateUpdate()
    {
        Vector3 newPos = startPos;
        newPos.x += player.position.x * parallaxFactor;
        transform.position = newPos;
    }
}

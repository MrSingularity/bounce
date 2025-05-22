using UnityEngine;

public class InfiniteBackground : MonoBehaviour
{
    public Transform player;
    public GameObject planePrefab;
    public float planeLength = 10f;
    public int numInitialPlanes = 3;

    private float nextSpawnX = 0f;

    void Awake()
    {
        if (player == null)
        {
            Debug.LogError("InfiniteBackground: Player not assigned!", gameObject);
            enabled = false;
        }
    }

    void Start()
    {
        for (int i = 0; i < numInitialPlanes; i++)
        {
            SpawnPlane();
        }
    }

    void Update()
    {
        if (player.position.x + (planeLength * 2) > nextSpawnX)
        {
            SpawnPlane();
        }
    }

    void SpawnPlane()
    {
        // Z bleibt konstant, X wächst
        Vector3 spawnPosition = new Vector3(nextSpawnX, 16.7f, 34.2f); // Z-Wert aus deinem Screenshot!
        Instantiate(planePrefab, spawnPosition, planePrefab.transform.rotation);
        nextSpawnX += planeLength;
    }
}

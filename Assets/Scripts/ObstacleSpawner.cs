using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    public GameObject obstaclePrefab;
    public Transform player;

    public float firstX = 10f;
    public float spawnAheadX = 15f;
    public float spawnIntervalX = 5f;
    public float minY = -1f;
    public float maxY = 1f;
    public float obstacleSpawnChance = 0.9f;

    private float nextSpawnX;

    void Start()
    {
        nextSpawnX = player.position.x + firstX;
    }

    void Update()
    {
        if (player.position.x + spawnAheadX >= nextSpawnX)
        {
            if (Random.value < obstacleSpawnChance)
            {
                SpawnObstacle(nextSpawnX);
            }
            nextSpawnX += spawnIntervalX;
        }
    }

    void SpawnObstacle(float xPos)
    {
        float yPos = Random.Range(minY, maxY);
        Vector3 spawnPos = new Vector3(xPos, yPos, 0f);
        
        GameObject newObstacle = Instantiate(obstaclePrefab, spawnPos, Quaternion.identity);
        ObstaclePassChecker checker = newObstacle.GetComponent<ObstaclePassChecker>();
        if (checker != null)
        {
            checker.player = player;
        }
    }
}

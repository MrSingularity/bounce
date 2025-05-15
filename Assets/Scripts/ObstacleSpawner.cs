using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    public GameObject obstaclePrefab;
    public Transform player;

    public float firstX = 10f;
    public float spawnAheadX = 15f;

    public float startIntervalX = 8f;
    public float minIntervalX = 3f;

    public float startSpawnChance = 0.5f;
    public float maxSpawnChance = 0.95f;

    public float difficultyRampDistance = 250f; // How much distance it takes to reach max difficulty

    public float minY = -1f;
    public float maxY = 1f;

    private float nextSpawnX;

    void Start()
    {
        nextSpawnX = player.position.x + firstX;
    }

    void Update()
    {
        float playerX = player.position.x;
        float progress = Mathf.Clamp01(playerX / difficultyRampDistance);

        // Dynamically adjust interval and chance based on progress
        float currentIntervalX = Mathf.Lerp(startIntervalX, minIntervalX, progress);
        float currentSpawnChance = Mathf.Lerp(startSpawnChance, maxSpawnChance, progress);

        while (player.position.x + spawnAheadX >= nextSpawnX)
        {
            if (Random.value < currentSpawnChance)
            {
                SpawnObstacle(nextSpawnX);
            }
            nextSpawnX += currentIntervalX;
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

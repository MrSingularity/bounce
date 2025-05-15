using UnityEngine;

public class SpawnCollectibles : MonoBehaviour
{
    public GameObject collectiblePrefab;
    public Transform player;

    public float collectibleStartX = 13f;
    public float collectibleInterval = 22.5f; // Distance in X between spawns
    public float spawnAheadDistance = 15f; // How far in front of the player to spawn
    public float maxXOffset = 5f; // Random range added to player's X to avoid linear spawns

    private float nextCollectibleX;

    void Start()
    {
        nextCollectibleX = collectibleStartX;
    }

    private void Update()
    {
        if (player.position.x >= nextCollectibleX)
        {
            SpawnCollectible();
            nextCollectibleX += collectibleInterval;
        }
    }

    private void SpawnCollectible()
    {
        float xPos = player.position.x + spawnAheadDistance + Random.Range(0f, maxXOffset);
        float yPos = Random.Range(2f, 8f);
        Vector3 spawnPos = new Vector3(xPos, yPos, 0f);

        GameObject obj = Instantiate(collectiblePrefab, spawnPos, Quaternion.identity);
        obj.GetComponent<Collectible>().type = (Random.value > 0.5f) ? PowerUpType.Boost : PowerUpType.Debuff;
    }
}

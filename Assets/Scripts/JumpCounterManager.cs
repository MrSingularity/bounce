using TMPro;
using UnityEngine;

public class JumpCounterManager : MonoBehaviour
{
    public static JumpCounterManager Instance;

    public Transform player;
    private float fixedY;
    private float fixedZ;

    public TextMeshPro jumpText;
    private int jumpCount = 0;

    private void Awake()
    {
        Instance = this;
    }

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

    public void ObstaclePassed()
    {
        jumpCount++;
        jumpText.text = "Score: " + jumpCount;
    }

    public void ResetCounter()
    {
        jumpCount = 0;
        jumpText.text = "Score: " + jumpCount;

        ObstaclePassChecker[] obstacles = FindObjectsByType<ObstaclePassChecker>(FindObjectsSortMode.None);
        foreach (var obstacle in obstacles)
        {
            obstacle.ResetCounted();
        }
    }
}

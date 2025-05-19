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
    private int highscore = 0;

    private AudioSource audioSource;

    private void Awake()
    {
        Instance = this;
        audioSource = GetComponent<AudioSource>();
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

        highscore = PlayerPrefs.GetInt("Highscore", 0);
        // jumpText.text = jumpCount + " / " + highscore;
        jumpText.text = "Score: " + jumpCount;
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

        if (jumpCount == 1 || jumpCount % 5 == 0)
        {
            audioSource.Play();
        }

        if (jumpCount > PlayerPrefs.GetInt("Highscore", 0))
        {
            PlayerPrefs.SetInt("Highscore", jumpCount);
            PlayerPrefs.Save();
            highscore = jumpCount;
            // jumpText.text = "New record: " + highscore;
        }
        else
        {
            // jumpText.text = jumpCount + " / " + highscore;
        }
        jumpText.text = "Score: " + jumpCount;
    }

    public void ResetCounter()
    {
        jumpCount = 0;
        // jumpText.text = jumpCount + " / " + highscore;
        jumpText.text = "Score: " + jumpCount;

        ObstaclePassChecker[] obstacles = FindObjectsByType<ObstaclePassChecker>(FindObjectsSortMode.None);
        foreach (var obstacle in obstacles)
        {
            obstacle.ResetCounted();
        }
    }
}

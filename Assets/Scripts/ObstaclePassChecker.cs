using UnityEngine;
using UnityEngine.Audio;

public class ObstaclePassChecker : MonoBehaviour
{
    public Transform player;
    private bool counted = false;
    private AudioSource audioSource;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (!counted && player.position.x > transform.position.x)
        {
            counted = true;
            JumpCounterManager.Instance.ObstaclePassed();
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            audioSource.Play();
        }
    }

    public void ResetCounted()
    {
        counted = false;
    }
}

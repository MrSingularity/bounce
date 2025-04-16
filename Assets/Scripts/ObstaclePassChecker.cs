using UnityEngine;

public class ObstaclePassChecker : MonoBehaviour
{
    public Transform player;
    private bool counted = false;

    void Update()
    {
        if (!counted && player.position.x > transform.position.x)
        {
            counted = true;
            JumpCounterManager.Instance.ObstaclePassed();
        }
    }

    public void ResetCounted()
    {
        counted = false;
    }
}

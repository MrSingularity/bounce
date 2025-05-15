using UnityEngine;
using YourNamespace;

public enum PowerUpType { None, Boost, Debuff }

public class Collectible : MonoBehaviour
{
    public PowerUpType type;

    private void Start()
    {
        VFX_FireController fireController = GetComponent<VFX_FireController>();
        if (fireController != null)
        {
            if (type == PowerUpType.Boost)
            {
                fireController.SetFireColor(Color.green);
            }
            else if (type == PowerUpType.Debuff)
            {
                fireController.SetFireColor(Color.red);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerMovement>().ApplyPowerUp(type);
            Destroy(gameObject);
        }
    }
}

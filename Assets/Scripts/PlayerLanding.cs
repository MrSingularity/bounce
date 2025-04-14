using UnityEngine;

public class PlayerLandingFX : MonoBehaviour
{
    public ParticleSystem dustFX;

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            if (dustFX != null)
            {
                dustFX.transform.position = collision.contacts[0].point;
                dustFX.Play();
            }
        }
    }
}

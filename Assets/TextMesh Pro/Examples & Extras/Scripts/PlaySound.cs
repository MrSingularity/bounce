using UnityEngine;

public class PlaySound : MonoBehaviour
{
    public AudioSource audioSource;

    public void PlayClickSound()
    {
        audioSource.Play();
    }
}

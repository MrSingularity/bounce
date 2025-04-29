using UnityEngine;

public class TrampolineController : MonoBehaviour
{
    private Animator _anim;
    private AudioSource audioSource;

    void Awake()
    {
        _anim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            _anim.SetTrigger("Jump");
            audioSource.Play();
        }
    }
}

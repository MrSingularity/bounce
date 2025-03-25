using UnityEngine;

public class TrampolineController : MonoBehaviour
{
    private Animator _anim;

    void Awake()
    {
        _anim = GetComponent<Animator>();
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            _anim.SetTrigger("Jump");
        }
    }
}

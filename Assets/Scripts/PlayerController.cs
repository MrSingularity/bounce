using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float jumpForce = 10f;
    public float rotationSpeed = 100f;

    private Rigidbody rb;
    private Vector3 initialPosition;
    private bool isGrounded = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        initialPosition = transform.position;
    }

    void Update()
    {
       
        float horizontalInput = Input.GetAxis("Horizontal");
        transform.Rotate(0, 0, -horizontalInput * rotationSpeed * Time.deltaTime);

        if (isGrounded)
        {
            Jump();
        }

        if (transform.position.y < -10)
        {
            ResetPosition();
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }

    // TODO: fix mirrored bounce if head first (or restarte when bouncing over a threshold rotation because genickburch)
    void Jump()
    {
        Quaternion rotationBeforeForece = transform.rotation;

        // use z-axis rotation for x movement
        float zRotationRad = transform.eulerAngles.z * Mathf.Deg2Rad;
        Vector3 jumpDirection = new Vector3(-Mathf.Sin(zRotationRad), 1, 0).normalized;
        rb.linearVelocity = Vector3.zero;
        rb.AddForce(jumpDirection * jumpForce, ForceMode.Impulse);

        transform.rotation = rotationBeforeForece;

        isGrounded = false;
    }

    void ResetPosition()
    {
        transform.position = initialPosition;
        rb.linearVelocity = Vector3.zero;
        rb.rotation = Quaternion.identity;
    }
}

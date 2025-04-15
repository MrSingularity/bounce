using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header("Sprung-Einstellungen")]
    public float jumpForce = 10f;
    public float minJumpForce = 3f;
    public float maxJumpForce = 20f;
    public float chargeTimeMax = 2f;

    [Header("Tilt-Einstellungen")]
    public float rotationSpeed = 100f;

    private Rigidbody rb;
    private bool isGrounded = false;
    private bool isCharging = false;
    private float chargeStartTime = 0f;
    private float chargedForce = 0f;
    private Vector3 startPosition;

    private Vector2 tiltInput;

    private Animator _anim;

    private void Awake()
    {
        _anim = GetComponent<Animator>();
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        startPosition = transform.position;
    }

    public void OnTilt(InputValue value)
    {
        tiltInput = value.Get<Vector2>();
    }

    public void OnJump(InputValue value)
    {
        // gedrückt
        if (value.isPressed && !isGrounded)
        {
            isCharging = true;
            chargeStartTime = Time.time;
            chargedForce = minJumpForce;
        }
        // losgelassen
        else if (!value.isPressed)
        {
            isCharging = false;
        }
    }

    private void FixedUpdate()
    {
        // Tilt: nach links/rechts kippen
        float tilt = tiltInput.x;
        transform.Rotate(0, 0, -tilt * rotationSpeed * Time.deltaTime);

        // Falls geladen wird (in der Luft), Ladezeit hochzählen
        if (isCharging)
        {
            float heldTime = Time.time - chargeStartTime;
            float chargePercent = Mathf.Clamp01(heldTime / chargeTimeMax);
            chargedForce = Mathf.Lerp(minJumpForce, maxJumpForce, chargePercent);
        }

        // Wenn runtergefallen
        if (transform.position.y < -10)
        {
            ResetPosition();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            Jump(); // spring beim Landen sofort wieder
            _anim.SetTrigger("Jump");
        }

        if (collision.gameObject.CompareTag("Obstacle"))
        {
            ResetPosition();
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }

    void Jump()
    {
        Quaternion rotationBeforeForce = transform.rotation;

        float zRotationRad = transform.eulerAngles.z * Mathf.Deg2Rad;
        Vector3 jumpDirection = new Vector3(-Mathf.Sin(zRotationRad), 1, 0).normalized;

        rb.linearVelocity = Vector3.zero;

        float forceToUse = (chargedForce > 0f) ? chargedForce : jumpForce;

        rb.AddForce(jumpDirection * forceToUse, ForceMode.Impulse);

        // Reset
        transform.rotation = rotationBeforeForce;
        chargedForce = 0f;
        isCharging = false;
        isGrounded = false;
    }

    void ResetPosition()
    {
        transform.position = startPosition;
        rb.linearVelocity = Vector3.zero;
        rb.rotation = Quaternion.identity;

        isCharging = false;
        chargedForce = 0f;
    }
}

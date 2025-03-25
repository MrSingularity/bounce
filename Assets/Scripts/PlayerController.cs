using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float jumpForce = 10f;
    public float minJumpForce = 3f;        
    public float maxJumpForce = 20f;       
    public float chargeTimeMax = 2f;       
    public float rotationSpeed = 100f;

    private Rigidbody rb;
    private Vector3 initialPosition;
    private bool isGrounded = false;

    private float chargeStartTime;
    private bool isCharging = false;
    private float chargedForce;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        initialPosition = transform.position;
    }

    void FixedUpdate()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        transform.Rotate(0, 0, -horizontalInput * rotationSpeed * Time.fixedDeltaTime);

        // Aufladung starten, wenn ↑ in der Luft gedrückt wird
        if (!isGrounded && Input.GetKeyDown(KeyCode.UpArrow))
        {
            isCharging = true;
            chargeStartTime = Time.time;
            chargedForce = minJumpForce; // Initialwert setzen
        }

        // Aufladung fortsetzen solange Taste gehalten wird
        if (!isGrounded && isCharging && Input.GetKey(KeyCode.UpArrow))
        {
            float heldTime = Time.time - chargeStartTime;
            float chargePercent = Mathf.Clamp01(heldTime / chargeTimeMax);
            chargedForce = Mathf.Lerp(minJumpForce, maxJumpForce, chargePercent);
        }

        // Position resetten, falls runtergefallen
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

            // Sofort springen mit geladener oder Standard-Kraft
            Jump();
        }

        if (collision.gameObject.CompareTag("Obstacle"))
        {
            ResetPosition();
        }
    }

    void OnCollisionExit(Collision collision)
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

        // Wenn geladen wurde, nutze geladenen Wert, sonst Standard
        float forceToUse = (chargedForce > 0f) ? chargedForce : jumpForce;

        rb.AddForce(jumpDirection * forceToUse, ForceMode.Impulse);

        transform.rotation = rotationBeforeForce;
        isGrounded = false;

        // Nach dem Sprung alles zurücksetzen
        isCharging = false;
        chargedForce = 0f;
    }

    void ResetPosition()
    {
        transform.position = initialPosition;
        rb.linearVelocity = Vector3.zero;
        rb.rotation = Quaternion.identity;

        isCharging = false;
        chargedForce = 0f;
    }
}

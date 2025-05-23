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

    [Header("PowerUp")]
    public float boostJumpForce = 20f;
    public float debuffJumpForce = 5f;
    private Color defaultColor = Color.white;
    public Color boostColor = Color.green;
    public Color debuffColor = Color.red;
    public GameObject trampoline;
    private Renderer trampolineRenderer;
    private PowerUpType currentPowerUp = PowerUpType.None;
    private bool powerUpApplied = false;

    private Rigidbody rb;
    private bool isGrounded = false;
    private bool isCharging = false;
    private float chargeStartTime = 0f;
    private float chargedForce = 0f;
    private Vector3 startPosition;
    private Vector2 tiltInput;
    private Animator _anim;

    private void Start()
    {
        _anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        startPosition = transform.position;
        trampolineRenderer = trampoline.GetComponentInChildren<Renderer>();

        if (trampolineRenderer != null)
            defaultColor = trampolineRenderer.material.GetColor("_BaseColor");
            trampolineRenderer.material.color = defaultColor;
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
        if (transform.position.x < -10 || transform.position.y < -10)
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

        if (powerUpApplied)
        {
            if (currentPowerUp == PowerUpType.Boost)
                forceToUse = boostJumpForce;
            else if (currentPowerUp == PowerUpType.Debuff)
                forceToUse = debuffJumpForce;

            // Reset after this jump
            currentPowerUp = PowerUpType.None;
            powerUpApplied = false;

            if (trampolineRenderer != null)
                trampolineRenderer.material.color = defaultColor;
        }

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

        JumpCounterManager.Instance.ResetCounter();

        // Reset powerup state on respawn
        currentPowerUp = PowerUpType.None;
        powerUpApplied = false;

        if (trampolineRenderer != null)
            trampolineRenderer.material.color = defaultColor;
    }

    public void ApplyPowerUp(PowerUpType type)
    {
        currentPowerUp = type;
        powerUpApplied = true;

        if (trampolineRenderer != null)
        {
            switch (type)
            {
                case PowerUpType.Boost:
                    trampolineRenderer.material.color = boostColor;
                    break;
                case PowerUpType.Debuff:
                    trampolineRenderer.material.color = debuffColor;
                    break;
                default:
                    trampolineRenderer.material.color = defaultColor;
                    break;
            }
        }
    }
}

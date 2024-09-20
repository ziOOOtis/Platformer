using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Windows;

public class PhysicsJump : MonoBehaviour
{
    [SerializeField] float jumpForce = 2f; // Force applied for jump
    [SerializeField] float normalJumpHeight = 1.5f; // Normal jump height
    private float jumpHeight; // Current jump height
    [SerializeField] private float waterJumpForceMultiplier = 1.5f; // For higher jump on waterJump
    private Rigidbody rb;


    public bool isGround = false;
    public int jumpChance = 2; // Max number of jumps allowed
    [SerializeField] private LayerMask groundLayer; // Specify ground
    [SerializeField] private int maxJumpChance = 3; // Maximum jumps
    [SerializeField] public int waterJumpChance = 1; // Water jump chance
    public PickUpWater puw;


    public bool isWaterJumping = false; // Flag for water jump

    private bool isHoldingJump = false; // Flag to check if player is holding jump
    private bool isFrozenInMidAir = false; // Track if the player is frozen mid-air 

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        jumpHeight = normalJumpHeight; // Set initial jump height to normal
    }

    void Update()
    {
        // Check input in Update to handle user interaction
        if (UnityEngine.Input.GetButton("Jump") && waterJumpChance > 0 && !isGround && !isFrozenInMidAir)
        {
            isHoldingJump = true; // Set holding jump to true
        }

        if (UnityEngine.Input.GetButtonUp("Jump") && isFrozenInMidAir)
        {
            // When the player releases the button, execute water jump
            rb.isKinematic = false; // Re-enable physics
            Jump(1.5f); // Perform water jump with higher force
            isWaterJumping = true; // Start water jump state
            waterJumpChance--; // Decrease water jump chance
            isFrozenInMidAir = false; // Reset the freeze flag
            isHoldingJump = false; // Reset holding flag

            Debug.Log("Water jump performed.");
        }

        if (puw.getWater = true)
        {
            waterJumpChance++;
            puw.getWater = false;
            Debug.Log("WaterJump Chances Remaining: " + waterJumpChance);
        }
    }

    void FixedUpdate()
    {
        // Freeze the player in mid-air during water jump
        if (isHoldingJump && !isFrozenInMidAir)
        {
            rb.linearVelocity = Vector3.zero; // Stop any movement
            rb.isKinematic = true; // Freeze the Rigidbody
            isFrozenInMidAir = true; // Set the freeze flag
            Debug.Log("Frozen mid-air for water jump.");
        }


    }

    private void Jump(float speedMultiplier)
    {
        Vector3 jumpVelocity = new Vector3(0, Mathf.Sqrt(jumpHeight * -2f * Physics.gravity.y), 0);
        rb.linearVelocity = jumpVelocity * speedMultiplier; // Apply jump force based on multiplier
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (IsGrounded())
        {
            isGround = true;
            isWaterJumping = false;
            jumpChance = maxJumpChance; // Reset to max jumps when landing
            jumpHeight = normalJumpHeight; // Reset jump height to normal

            Debug.Log("Jump Chances Remaining: " + jumpChance);
        }
    }

    private void WaterJumpPlus() 
    {

    }


    private bool IsGrounded()
    {
        RaycastHit hit;
        bool result = Physics.SphereCast(transform.position, 0.15f, -transform.up, out hit, 1f);
        return result;
    }
}
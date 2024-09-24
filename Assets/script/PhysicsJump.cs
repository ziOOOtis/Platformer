using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;
using UnityEngine.Windows;

public class PhysicsJump : MonoBehaviour
{
    [SerializeField] float normalJumpHeight = 1.5f; // Normal jump height
    private float jumpHeight; // Current jump height
    [SerializeField] private float waterJumpForceMultiplier = 1.5f; // For higher jump on waterJump
    private Rigidbody rb;


    public bool isGround = false;
    float jumpIndex;
    public int jumpChance = 1; // Max number of jumps allowed
    [SerializeField] private LayerMask groundLayer; // Specify ground
    [SerializeField] private int maxJumpChance = 3; // Maximum jumps
    [SerializeField] public int waterJumpChance = 0; // Water jump chance
    public PickUpWater puw;
    public WarmAirDamage wad;
    public FryZone fz;


    public bool isWaterJumping = false; // Flag for water jump


    void Start()
    {
        rb = GetComponent<Rigidbody>();

        jumpHeight = normalJumpHeight; // Set initial jump height to normal
    }

    void Update()
    {
        if (UnityEngine.Input.GetButtonUp("Jump"))
        {
            if (jumpChance > 0)
            {
                Jump(1);
                jumpIndex++;
                isGround = false;
                jumpChance--;
                //Debug.Log("Jump Chances Remaining: " + jumpChance);
            }
            else
            {
                if (waterJumpChance > 0) //&& (UnityEngine.Input.GetButtonUp("Jump") )
                {
                    if (UnityEngine.Input.GetButton("Jump")) //freeze
                    {
                        rb.constraints = RigidbodyConstraints.FreezePosition | RigidbodyConstraints.FreezeRotation;


                    }

                    if (UnityEngine.Input.GetButtonUp("Jump")) //&& ( )
                    {
                        rb.constraints = RigidbodyConstraints.None; // Remove constraints to allow full movement
                        Jump(1.5f);
                        isWaterJumping = true; // Start water jump rotation

                        isGround = false;
                        waterJumpChance--;

                        //Debug.Log("WaterJump Chances Remaining: " + waterJumpChance);

                    }

                }
            }



        }
    }

        private void Jump(float speedMultiplier)
    {
        Vector3 jumpVelocity = new Vector3(0, Mathf.Sqrt(jumpHeight * -2f * Physics.gravity.y), 0);
        rb.linearVelocity = jumpVelocity * speedMultiplier; // Apply jump force based on multiplier
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (IsGrounded(collision))
        {
            isGround = true;
            isWaterJumping = false;
            jumpChance = maxJumpChance; // Reset to max jumps when landing
            jumpHeight = normalJumpHeight; // Reset jump height to normal



        }
    }

    public void GetWater(PickUpWater waterPicked)
    {
        puw = waterPicked;
        if (puw != null && puw.getWater)
        {
            waterJumpChance++;
            puw.getWater = false;
            //Debug.Log("WaterJump Chances Remaining: " + waterJumpChance);
        }
    }

    public void LostWater(WarmAirDamage waterLosted)
    {
        wad = waterLosted;

        if (wad != null && wad.lostWater)
        {
            waterJumpChance--;
            wad.lostWater = false;
            //Debug.Log("WaterJump Chances Remaining: " + waterJumpChance);
        }

    }

    public void LostWater(FryZone waterCooked)
    {

        fz = waterCooked;
        if (fz != null && fz.waterCooked)
        {
            waterJumpChance-= fz.cookDuration;
            fz.waterCooked = false;
            //Debug.Log("Water Remaining: " + waterJumpChance);
            GameManager.SetWatertScore(waterJumpChance);
            GameManager.SetJumpScore(jumpIndex);
        }
     }

private bool IsGrounded(Collision collision)
    {
        // Check if the object has the ground layer
        return (groundLayer.value & (1 << collision.gameObject.layer)) > 0;
    }
}
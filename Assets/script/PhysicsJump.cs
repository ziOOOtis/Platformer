using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;
using UnityEngine.Windows;

public class PhysicsJump : MonoBehaviour
{
    private Rigidbody rb;
    [SerializeField] private LayerMask groundLayer; // Specify ground
    public bool isGround = false;
    

    // New scripts for modify jumping and falling
    public float gravityScale = 1.0f;// SCALE
    [SerializeField] private float fallGravityScale = 3f;
    public static float globalGravity = -9.81f;
    private Vector3 gravity;

    //Jump height adjustments
    [SerializeField] float normalJumpHeight = 1.5f; // Normal jump height
    private float jumpHeight; // Current jump height
    [SerializeField] private float waterJumpForceMultiplier = 1.5f; // For higher jump on waterJump
    [SerializeField] private float buttonPressWindow = 0.5f;
    private float buttonPressedTime;
    float speedMultiplier = 1;
    [SerializeField] private float notEnoughtime = 0.3f;


    //WaterJump related

    float jumpIndex;
    public int jumpChance = 1; // Max number of jumps allowed
    [SerializeField] private int maxJumpChance = 3; // Maximum jumps
    [SerializeField] public int waterJumpChance = 0; // Water jump chance
    [SerializeField] public int waterIndex = 0;// Totoal Water jump chance


    public PickUpWater puw;
    public Spice spice;
    public WarmAirDamage wad;
    public FryZone fz;


    public bool isWaterJumping = false; // Flag for water jump
    public bool isDoubleJump = false;


    void Start()
    {
        rb = GetComponent<Rigidbody>();

        jumpHeight = normalJumpHeight; // Set initial jump height to normal
    }

    void Update()
    {
        if (isGround && UnityEngine.Input.GetButtonDown("Jump")) //jumping height adjustment before jump
        {

            buttonPressedTime = 0;



        }

        if (UnityEngine.Input.GetButton("Jump"))
        {

            buttonPressedTime += Time.deltaTime;


            // Neutralize downward momentum if falling //While Jump or while press?
            if (rb.linearVelocity.y < 0 && buttonPressedTime < 4 * buttonPressWindow && jumpChance + waterJumpChance > 0)
            {
                rb.linearVelocity = new Vector3(rb.linearVelocity.x, rb.linearVelocity.y * 0.3f, rb.linearVelocity.z); // Reset vertical velocity
            }

        }



        if (UnityEngine.Input.GetButtonUp("Jump"))
        {
            if (jumpChance > 0)
            {
                if (isGround)
                {
                    Jump();
                    jumpIndex++;
                    isGround = false;
                    jumpChance--;
                    //Debug.Log(buttonPressedTime);
                    //Debug.Log("Jump Chances Remaining: " + jumpChance);
                }
                else
                {
                    buttonPressedTime = buttonPressWindow + 1; //the steak is not on ground so it should be affected by pressing time
                    Jump();
                    jumpIndex++;
                    isGround = false;
                    isDoubleJump = true;
                    jumpChance--;
                }

            }
            else
            {
                if (waterJumpChance > 0) //&& (UnityEngine.Input.GetButtonUp("Jump") )
                {


                    if (UnityEngine.Input.GetButtonUp("Jump")) //&& ( )
                    {
                        rb.constraints = RigidbodyConstraints.None; // Remove constraints to allow full movement
                        jumpHeight = jumpHeight * waterJumpForceMultiplier; // Just affect height variable
                        buttonPressedTime = buttonPressWindow; //the steak is not on ground so it should be affected by pressing time
                        Jump();
                        isWaterJumping = true; // Start water jump rotation
                        isDoubleJump = true;


                        isGround = false;
                        waterJumpChance--;
                        waterIndex--;

                        //Debug.Log("WaterJump Chances Remaining: " + waterJumpChance);

                    }

                }
            }



        }
    }

    void FixedUpdate()
    {
        //Modification of gravity, to change the jumping and falling type.
        if (rb.linearVelocity.y > 0)
        {
            gravity = globalGravity * gravityScale * Vector3.up;

        }
        else
        {


            gravity = globalGravity * fallGravityScale * Vector3.up;
            

        }


        rb.AddForce(gravity, ForceMode.Acceleration);


    }

    private void Jump()
    {
        // Neutralize downward momentum if falling
        if (rb.linearVelocity.y < 0)
        {
            rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0, rb.linearVelocity.z); // Reset vertical velocity
        }

        float adjustedMultiplier = (buttonPressedTime < buttonPressWindow) ? notEnoughtime * speedMultiplier : speedMultiplier; // test the press button time
        float jumpForce = Mathf.Sqrt(jumpHeight * adjustedMultiplier * gravity.y * -2) * rb.mass;
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);

        jumpHeight = normalJumpHeight; // Reset jump height to normal after jump.
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (IsGrounded(collision))
        {
            isGround = true;
            isWaterJumping = false;
            isDoubleJump = false;
            jumpChance = maxJumpChance; // Reset to max jumps when landing
            jumpHeight = normalJumpHeight; // Reset jump height to normal
            speedMultiplier = 1; // reset speed multiplier every time when landed.



        }
    }

    public void GetWater(PickUpWater waterPicked)
    {
        puw = waterPicked;
        if (puw != null && puw.getWater)
        {
            waterJumpChance++;
            waterIndex++;
            puw.getWater = false;
            //Debug.Log("WaterJump Chances Remaining: " + waterJumpChance);
        }
    }

    public void GetSpice(Spice spicePicked)
    {
        spice = spicePicked;
        if (spice != null && spice.getSpice)
        {
            waterJumpChance--;
            maxJumpChance++;

            spice.getSpice = false;
            //Debug.Log("WaterJump Chances Remaining: " + waterJumpChance);
        }
    }

    public void LostWater(WarmAirDamage waterLosted)
    {
        wad = waterLosted;

        if (wad != null && wad.lostWater)
        {
            waterJumpChance--;
            waterIndex--;
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
            waterIndex -= (fz.cookDuration / 2);
            fz.waterCooked = false;
            //Debug.Log("Water Remaining: " + waterJumpChance);
            if (waterIndex < 0)
            {
                waterIndex = -1;
            }
            GameManager.SetWatertScore(waterIndex);
            
        }
     }

private bool IsGrounded(Collision collision)
    {
        // Check if the object has the ground layer
        return (groundLayer.value & (1 << collision.gameObject.layer)) > 0;
    }
}
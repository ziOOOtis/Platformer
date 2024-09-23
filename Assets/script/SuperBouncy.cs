using UnityEngine;
using System.Collections; // Add this line for IEnumerator and coroutines


public class SuperBouncy : MonoBehaviour
{
    private Rigidbody rb;
    private int jumpTimes = 0;
    private Vector3 originalScale;

    [SerializeField] private float bounceForce = 5f; // Force applied to the player when they jump on the tomato
    [SerializeField] private float rollForce = 5f; // Force to make the tomato roll
    [SerializeField] private Vector3 rollDirection = Vector3.right; // Direction to roll after the second jump

    [SerializeField] private float squeezeAmount = 0.7f; // How much to squeeze
    [SerializeField] private float stretchDuration = 0.3f; // How fast the squeeze/stretch happens


    public TimeManager timeManager;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        FreezeTomato(); // Freeze initially
        originalScale = transform.localScale; // Save the original scale of the tomato
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Increment jump count each time the player lands on the tomato
            jumpTimes++;

            // Get the Rigidbody component from the player
            Rigidbody playerRb = collision.gameObject.GetComponent<Rigidbody>();

            if (playerRb != null)
            {
                timeManager.DoSLowmotion(0.2f); //try to slow down.

                // Apply an upward force to the player's Rigidbody
                playerRb.AddForce(Vector3.up * bounceForce, ForceMode.Impulse);


                // Trigger squeeze and stretch effect
                StartCoroutine(SqueezeAndStretch());
            }

            if (jumpTimes == 1)
            {
                // Keep tomato still on the first jump
                FreezeTomato();
            }
            else if (jumpTimes == 2)
            {
                // Start rolling after the second jump
                UnfreezeTomato();
                RollTomato();
            }
        }
    }

    private IEnumerator SqueezeAndStretch()
    {
        // Squeeze the tomato (shrink on Y-axis, stretch on X/Z-axis)
        Vector3 squeezedScale = new Vector3(originalScale.x * 1.2f, originalScale.y * squeezeAmount, originalScale.z * 1.2f);
        transform.localScale = squeezedScale;

        // Wait for the squeeze duration
        yield return new WaitForSeconds(stretchDuration);

        // Return to original scale
        transform.localScale = originalScale;
    }

    // Freeze position and rotation to make the tomato stay still
    private void FreezeTomato()
    {
        rb.constraints = RigidbodyConstraints.FreezePosition | RigidbodyConstraints.FreezeRotation;
    }

    // Unfreeze to allow movement and rotation after the second jump
    private void UnfreezeTomato()
    {
        rb.constraints = RigidbodyConstraints.None; // Remove constraints to allow full movement
    }

    // Apply a rolling force and enable gravity to make the tomato fall
    private void RollTomato()
    {
        rb.AddForce(rollDirection * rollForce, ForceMode.Impulse); // Apply force to roll
        rb.useGravity = true; // Enable gravity to make it fall after rolling
    }
}

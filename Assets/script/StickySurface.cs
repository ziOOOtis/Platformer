using UnityEngine;

public class StickySurface : MonoBehaviour
{
    [SerializeField] private LayerMask stickyLayer; // Define what layer is considered "sticky"
    [SerializeField] private float unstickForce = 5f; // Force applied to unstick the steak

    private Rigidbody rb;
    private bool isStuck = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void OnCollisionStay(Collision collision)
    {
        // Continuously check if the steak is on a sticky surface
        if (((1 << collision.gameObject.layer) & stickyLayer) != 0)
        {
            // Stick the steak while holding the space key
            if (UnityEngine.Input.GetButton("Jump") && !isStuck)
            {
                StickToSurface();
            }
            // Unstick the steak immediately when the space key is released
            if (Input.GetButtonUp("Jump") && isStuck)
            {
                Unstick();
            }
        }
    }

    private void StickToSurface()
    {
        isStuck = true;

        // Freeze movement
        rb.isKinematic = true; // Disables physics so the steak stays stuck

        Debug.Log("Steak is stuck.");
    }

    private void Unstick()
    {
        if (isStuck)
        {
            isStuck = false;
            rb.isKinematic = false; // Re-enable physics
            rb.AddForce(Vector3.up * unstickForce, ForceMode.Impulse); // Push the steak upwards

            Debug.Log("Steak is unstuck.");
        }
    }
}

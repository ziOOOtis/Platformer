using UnityEngine;

public class AirPatrol : MonoBehaviour
{
    public Transform[] patrolPoints;
    public int targetPoint;
    public float speed;
    [SerializeField] private float flipSpeed = 180f; // Speed of rotation

    private bool isTurning = false; // To track if turning is needed
    private bool isMoving = false;
    private Quaternion targetRotation; // To store the target rotation

    void Start()
    {
        targetPoint = 0;
        targetRotation = transform.rotation; // Initialize with the current rotation
    }

    void Update()
    {
        // Move the enemy towards the next patrol point
        if (Vector3.Distance(transform.position, patrolPoints[targetPoint].position) > 0 && !isTurning)
        {
            isMoving = true;
            transform.position = Vector3.MoveTowards(transform.position, patrolPoints[targetPoint].position, speed * Time.deltaTime); 
        }

        // Check if we reached the patrol point
        if (Vector3.Distance(transform.position, patrolPoints[targetPoint].position) < 0.1f && !isTurning)
        {
            // Start turning around
            isTurning = true;
            isMoving = false;

            // Calculate the new target rotation by rotating 180 degrees around the Y-axis
            targetRotation = transform.rotation * Quaternion.Euler(0, 180, 0);

            // Increase the target index for the next patrol point
            increaseTargetIndex();
        }

        // Smoothly rotate towards the target rotation
        if (isTurning)
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, flipSpeed * Time.deltaTime);

            // Stop turning when the rotation is complete
            if (Quaternion.Angle(transform.rotation, targetRotation) < 0.1f)
            {
                isTurning = false;
            }
        }
    }

    void increaseTargetIndex()
    {
        targetPoint++;
        if (targetPoint >= patrolPoints.Length)
        {
            targetPoint = 0;
        }
    }
}

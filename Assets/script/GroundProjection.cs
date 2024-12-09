using UnityEngine;

public class GroundProjection : MonoBehaviour
{
    [SerializeField] private LayerMask groundLayer; // Specify what counts as "ground."
    [SerializeField] private GameObject projectionMarker; // Assign a marker prefab or GameObject.
    [SerializeField] private float maxScale = 1.2f; // Maximum size of the marker.
    [SerializeField] private float minScale = 0.5f; // Minimum size of the marker.

    

    private void Update()
    {
        // Cast a ray downward from the character
        if (Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit, Mathf.Infinity, groundLayer))
        {
            // Move the marker to the hit point
            projectionMarker.transform.position = hit.point;

            // Adjust the marker's size based on distance
            float distance = hit.distance; // Distance from character to ground
            float scale = Mathf.Lerp(maxScale, minScale, distance / 3.5f); // Scale smoothly based on distance
            projectionMarker.transform.localScale = new Vector3(scale, scale, scale);

            // Make the marker visible
            projectionMarker.SetActive(true);
        }
        else
        {
            // Hide the marker if no ground is detected
            projectionMarker.SetActive(false);
        }
    }
}

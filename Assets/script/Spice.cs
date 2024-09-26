using UnityEngine;
using System.Collections; // Add this line for IEnumerator and coroutines

public class Spice : MonoBehaviour
{
    public bool getSpice = false;
    public int spiceIndex;
    public GameObject player;
    public GameObject trigger;
    public GameObject lid;

    private Vector3 originalScale;
    private Vector3 originalPosition;
    [SerializeField] private float squeezeAmount = 0.5f; // How much to squeeze
    [SerializeField] private float stretchDuration = 0.05f; // How fast the squeeze/stretch happens

    [SerializeField] private GameObject smoke;
    public TimeManager timeManager;//slower time

    private void Start()
    {

        originalScale = lid.transform.localScale; // Save the original scale 
        originalPosition = lid.transform.localPosition; // Save the original scale 

        smoke.gameObject.SetActive(false);

    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if the player collides with the water
        if (other.CompareTag("Player"))
        {

            getSpice = true;
            spiceIndex++;
            GameManager.SetSpiceScore(spiceIndex);

            timeManager.DoSLowmotion(0.5f); //try to slow down.


            smoke.gameObject.SetActive(true);

            player.GetComponent<Transform>().rotation = Quaternion.Euler(0, 0, 0);

            other.GetComponent<PhysicsJump>().GetSpice(this);

            // Trigger squeeze and stretch effect
            StartCoroutine(SqueezeAndStretch());




        }
    }

    private IEnumerator SqueezeAndStretch()
    {
        // Squeeze the tomato (shrink on Y-axis, stretch on X/Z-axis)
        Vector3 squeezedScale = new Vector3(originalScale.x * 1.5f, originalScale.y * squeezeAmount, originalScale.z * 1.5f);
        Vector3 move = new Vector3(originalPosition.x , originalPosition.y - 0.05f, originalPosition.z);
        lid.transform.localScale = squeezedScale;
        lid.transform.localPosition = move;

        // Wait for the squeeze duration
        yield return new WaitForSeconds(stretchDuration);

        // Return to original scale
        lid.transform.localScale = originalScale;
        lid.transform.localPosition = originalPosition;

        Destroy(trigger);
    }
}

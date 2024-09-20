using UnityEngine;


public class CameraMovement : MonoBehaviour
{
    public GameObject player;
    [SerializeField] Vector3 offset = new Vector3(-0.9f, 0.8f, -0.20f);


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = player.transform.position + offset;
        
    }
}

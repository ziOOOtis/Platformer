using System.Drawing;
using Unity.VisualScripting;
using UnityEngine;

public class FryZone : MonoBehaviour
{

    public GameObject fryZone;
    public Timer tm;
    public HeatTrigger trigger;

    // [SerializeField] Gradient gradient;

    // [SerializeField] MeshRenderer rend;

    public bool isCooking;
    public bool waterCooked;
    public int cookDuration; // well-heated pan will have less cook duration, so that less water will be losted.



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        waterCooked=false;  

        //rend = GetComponent<MeshRenderer>();
 


    }

    // Update is called once per frame
    void Update()
    {
        //rend.material.SetColor("Color", gradient.Evaluate(tm.slider.normalizedValue));
    }


    private void OnTriggerEnter(Collider other)
    {
        
        if ((other.gameObject.tag == "Player") && trigger.turOn)
        {
            isCooking = true;
            switch (tm.heatStatus)
            {

                case 3:
                    waterCooked = true;

                    Debug.Log("warm good.");

                    cookDuration = 1;
                    other.GetComponent<PhysicsJump>().LostWater(this);
                    tm.countUp = false;

                    break;

                case 2:
                    waterCooked = true;

                    Debug.Log("WARM YET.");

                    cookDuration = 2;
                    other.GetComponent<PhysicsJump>().LostWater(this);
                    tm.countUp = false;
                    break;

                case 1:
                    Debug.Log("NOT WARM YET.");
                    break;

            }
        }

    }
}

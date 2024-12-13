using System.Drawing;
using Unity.VisualScripting;
using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class FryZone : MonoBehaviour
{

    public GameObject fryZone;
    private Spice spice;
    public Timer tm;
    public HeatTrigger trigger1, trigger2;



    public bool isCooking;
    public bool waterCooked;
    public int cookDuration; // well-heated pan will have less cook duration, so that less water will be losted.
    bool practice; // I think it is mentioned about practice level. SO if I wanna make more level something should be changed-----or do not toch the level number?

    [SerializeField] private Animator animator1,animator2;
    [SerializeField] private GameObject smoke;
    [SerializeField] private GameObject practicePage;
    [SerializeField] private GameObject projection;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        waterCooked=false;
        smoke.gameObject.SetActive(false);
        practicePage.gameObject.SetActive(false);
        projection.gameObject.SetActive(true);
        practice = false;

        //rend = GetComponent<MeshRenderer>();



    }

    // Update is called once per frame
    void Update()
    {
        //rend.material.SetColor("Color", gradient.Evaluate(tm.slider.normalizedValue));
    }


    private void OnTriggerEnter(Collider other)
    {
        bool tiggerON = trigger1.turOn || trigger2.turOn;


        if ((other.gameObject.tag == "Player") && tiggerON)
        {
            isCooking = true;
            smoke.gameObject.SetActive(true);
            projection.gameObject.SetActive(false);
            animator1.enabled = true;
            animator1.SetBool("isCook", true);
            animator2.SetBool("isCook", true);

            if (practice==false) // if it is the practice level? should be changed as the replay logic problem
            {
                StartCoroutine(LoadPage());
                practice = true;
                GameManager.SetSpiceScore(0);//resect the number of spice
                spice.spiceIndex = 0;//reset the spice index

            }

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

    private IEnumerator LoadPage()
    {
        yield return new WaitForSeconds(3f);
        practicePage.gameObject.SetActive(true);
        
    }
}



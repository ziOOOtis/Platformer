using UnityEngine;


public class JumpBarControl : MonoBehaviour
{
    // Transform Jump1, Jump2, Jump3, Jump4;
    [SerializeField] public GameObject Jump1, Jump2, Jump3, Jump4;

    public PhysicsJump pj;

    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
       
        Jump1.gameObject.SetActive(true);
        Jump2.gameObject.SetActive(false);
        Jump3.gameObject.SetActive(false);
        Jump4.gameObject.SetActive(false);
        
    }

    // Update is called once per frame
    void Update()
    {
        

        switch (pj.jumpChance)
        {


            case 1:
                Jump1.gameObject.SetActive(true);

                break;

            case 0:
                Jump1.gameObject.SetActive(false);

                break;
        }
        switch (pj.waterJumpChance)
        {

            case 3:
                Jump2.gameObject.SetActive(true);
                Jump3.gameObject.SetActive(true);
                Jump4.gameObject.SetActive(true);
                break;

            case 2:
                Jump2.gameObject.SetActive(true);
                Jump3.gameObject.SetActive(true);
                Jump4.gameObject.SetActive(false);
                break;

            case 1:
                Jump2.gameObject.SetActive(true);
                Jump3.gameObject.SetActive(false);
                Jump4.gameObject.SetActive(false);
                break;

            case 0:
                Jump2.gameObject.SetActive(false);
                Jump3.gameObject.SetActive(false);
                Jump4.gameObject.SetActive(false);
                break;
        }
    }
}

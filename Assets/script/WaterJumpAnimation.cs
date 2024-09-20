using UnityEngine;

public class WaterJumpAnimation : MonoBehaviour
{
    [SerializeField]private Animator animator;
    [SerializeField]private PhysicsJump pj;
    private int isJump;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        isJump = pj.jumpChance;

        //if (isJump==0)
        
            if (UnityEngine.Input.GetButton("Jump"))
            {
                animator.SetBool("Squeeze", true);
                Debug.Log("Squeeze");
            }
            else
            {
                animator.SetBool("Squeeze", false);
            }

        if (pj.isWaterJumping == true)
        {
            animator.SetBool("isWaterJumping", true);
            Debug.Log("waterJumping");
        }
        else
        {
            animator.SetBool("isWaterJumping", false);
        }



        if (pj.isGround == true)
        {
            animator.SetBool("isGround", true);
            //animator.SetBool("isWaterJumping", false);
        }
        animator.SetBool("isWaterJumping", pj.isWaterJumping);

    }
}

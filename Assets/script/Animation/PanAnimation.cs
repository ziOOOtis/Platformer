using UnityEngine;

public class PanAnimation : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private HeatTrigger ht;
    [SerializeField] private HeatTrigger ht2;
    private bool isHeated;

    void Update()
    {
        isHeated = ht.turOn && ht2.turOn;

        //if (isJump==0)

        if (isHeated)
        {
            animator.SetBool("warmUp", true);

        }
        else
        {
            animator.SetBool("warmUp", false);
        }
    }
}

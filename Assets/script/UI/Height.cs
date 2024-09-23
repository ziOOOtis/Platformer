using UnityEngine;
using TMPro;

public class Height : MonoBehaviour
{


    public JumpHeightScoring jhs;

    [Header("Component")]
    public TextMeshProUGUI heightText;

    [Header("Height Setting")]
    public float currentHeight;



    // Update is called once per frame
    void Update()
    {

        currentHeight = jhs.highestY;
        heightText.text = currentHeight.ToString("0.00");
        
    }




}





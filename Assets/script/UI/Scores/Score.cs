using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Score : MonoBehaviour
{
    float finalHeight;
    int waterChance;
    int spiceIndex;
    int finalSpice;
    int finaltotal;
    public TextMeshProUGUI heightScore;
    public TextMeshProUGUI waterScore;
    public TextMeshProUGUI spiceScore;
    public TextMeshProUGUI totalScore;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        finalHeight = GameManager.GetHeightScore();
        waterChance = GameManager.GetWaterScore();
        spiceIndex = GameManager.GetSpiceScore();
        finalSpice = spiceIndex * 15;

    }

    // Update is called once per frame
    void Update()
    {
        heightScore.text = finalHeight.ToString();
        waterScore.text = waterChance.ToString();
        spiceScore.text = finalSpice.ToString();
        Calculation();
        // Display the total score
        totalScore.text = finaltotal.ToString();


    }

    void Calculation()
    {
        finaltotal = Mathf.FloorToInt(finalHeight ) + waterChance + finalSpice;//+spice

    }
}

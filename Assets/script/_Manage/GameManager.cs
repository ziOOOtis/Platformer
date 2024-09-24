using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static float heightScore;
    public static int waterScore;
    public static int spiceScore;

    // This method is called when the player reaches the goal and records the height
    public static void SetHeightScore(float height)
    {
        heightScore = height;
    }

    // This method can be used in the end scene to retrieve the stored height score
    public static float GetHeightScore()
    {
        return heightScore;
    }

    public static void SetWatertScore(int water)
    {
        waterScore = water * 20;
    }

    // This method can be used in the end scene to retrieve the stored height score
    public static int GetWaterScore()
    {
        return waterScore;
    }

    public static void SetSpiceScore(int spice)
    {
        spiceScore = spice;
    }

    // This method can be used in the end scene to retrieve the stored height score
    public static int GetSpiceScore()
    {
        return spiceScore;
    }




}

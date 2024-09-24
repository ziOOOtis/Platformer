using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static float heightScore;
    public static int waterScore;
    public static float jumpScore;

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

    public static void SetJumpScore(float jump)
    {
        jumpScore = Mathf.FloorToInt(jump * 5); ;
    }

    // This method can be used in the end scene to retrieve the stored height score
    public static float GetJumpScore()
    {
        return jumpScore;
    }


}

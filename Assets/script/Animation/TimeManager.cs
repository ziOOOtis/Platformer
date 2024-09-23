using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public float slowdownFactor ;
    public float slowdownLength = 2f;

    private void Update()
    {
        Time.timeScale += (1f / slowdownLength)* Time.unscaledDeltaTime;
        Time.timeScale = Mathf.Clamp(Time.timeScale, 0f, 1f);

    }

    public void DoSLowmotion(float slowdownFactor) 
    { 
        Time.timeScale = slowdownFactor;
        Time.fixedDeltaTime = Time.timeScale * .02f ;
    }

}   

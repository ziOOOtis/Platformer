using System.Data;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;


public class Timer : MonoBehaviour
{
    public Slider slider;
    [SerializeField] Gradient gradient;
    [SerializeField] Image fill;

    public float heatingTime;
    [SerializeField] float maxHeatTime = 20;
    public bool countUp;
    public int heatStatus;

    float waitingTimes = 2f;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        slider.minValue = 0;
        slider.maxValue = maxHeatTime;

        fill.color = gradient.Evaluate(0); //set the color of beginning.
    }



    // Update is called once per frame
    void Update()
    {
        if (heatingTime <= 20)
        {
            HeatingUp();
            HeatingStatus();
        }
        else
        {
            heatingTime = countUp ? heatingTime += Time.deltaTime : heatingTime;
            //heatingTime = countUp ? heatingTime += Time.deltaTime : heatingTime;
            Debug.Log("ON FIRE!!!!");
            countUp = false;
            enabled = false; // stop the component runing
            StartCoroutine(LoadToEnd());

        }

        // Different color of status
        fill.color = gradient.Evaluate(slider.normalizedValue); // make the value 0-1.

        


    }

    public void HeatingUp()
    {
        heatingTime = countUp ? heatingTime += Time.deltaTime : heatingTime;
        slider.value = heatingTime;
    }

    public void HeatingStatus()
    {
        if (slider.normalizedValue < 0.4f)
        {
            heatStatus = 1; //the pan is not heat enough
        }
        else if(slider.normalizedValue > 0.75)
        {
            heatStatus = 3; //the pan is heat good!
        }
        else { heatStatus = 2; } //the pan is heat enough
    }

    private IEnumerator LoadToEnd()
    {
        yield return new WaitForSeconds(waitingTimes);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}

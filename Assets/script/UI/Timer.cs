using System.Data;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public Slider slider;
    [SerializeField] Gradient gradient;
    [SerializeField] Image fill;

    public float heatingTime;
    public bool countUp;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        slider.minValue = 0;
        slider.maxValue = 20;

        fill.color = gradient.Evaluate(0); //set the color of beginning.
    }



    // Update is called once per frame
    void Update()
    {
        HeatingUp();

        // Different color of status
        fill.color = gradient.Evaluate(slider.normalizedValue); // make the value 0-1.

        //the pan is not heat enough


    }

    public void HeatingUp()
    {
        heatingTime = countUp ? heatingTime += Time.deltaTime : heatingTime;
        slider.value = heatingTime;
    }
}

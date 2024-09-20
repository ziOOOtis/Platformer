using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public Slider slider;

    public float heatingTime;
    public bool countUp;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        heatingTime = countUp ? heatingTime += Time.deltaTime : heatingTime;
        slider.value = heatingTime;
    }
}

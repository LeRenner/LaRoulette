 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TIMER : MonoBehaviour
{
    float currentTime;
    public bool isTriggered = false;
    public bool decrease = true;
    public float startingTime = 10f;
    private float pot = 1;
    public Text hsLabel;  
    [SerializeField] Text countdownText;
    void Start()
    {
        currentTime = startingTime;
        if (!decrease) {pot = 0;}
    }
    void Update()
    {
        currentTime = currentTime + Mathf.Pow(-1, pot) * Time.deltaTime;
        countdownText.text = currentTime.ToString("0");

        if (currentTime <= 0)
        {
	
            currentTime = 0;
	    
	    isTriggered = true;
        }
    }
    public bool isTrigger()
    {
         return isTriggered;
    }
}


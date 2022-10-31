 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TIMER : MonoBehaviour
{
    float currentTime;
    public bool isTriggered = false;
    public float startingTime = 10f;
    public Text hsLabel;  
    [SerializeField] Text countdownText;
    void Start()
    {
        currentTime = startingTime;
    }
    void Update()
    {
        currentTime = currentTime -1 * Time.deltaTime;
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


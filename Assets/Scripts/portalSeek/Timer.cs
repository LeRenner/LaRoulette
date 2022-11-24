using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public int countdown;
    private int delta;
    private int interval;

    // Start is called before the first frame update
    void Start()
    {
        delta = 20;
        countdown = 120;
        interval = 0;
    }

    // Update is called once per frame
    void Update()
    {
        updateTimer();
    }

    // Updates timer and notifies on end of countdown
    void updateTimer() {
        if (interval == delta) {
            interval = 0;
            countdown--;
        }
        else if (countdown > 0)
            interval++;
    }

}

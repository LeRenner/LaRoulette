using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinManager : MonoBehaviour
{
    public Seeker seeker;
    private GameObject[] seekerList;
    private bool seekerWon;
    public string winner;
    public Timer timer;
    private int time;

    void Start()
    {
        time = GameObject.Find("Timer").GetComponent<Timer>().countdown;
        seekerList = GameObject.FindGameObjectsWithTag("Seeker");
        
        if (seekerList.Length != 0)
            seeker = seekerList[0].GetComponent<Seeker>();

        seekerWon = false;
        winner = null;
    }

    // Update is called once per frame
    void Update()
    {
        if (!seekerWon && time >= 0) {
            if (time > 0) {
                time = GameObject.Find("Timer").GetComponent<Timer>().countdown;
                checkForSeekerWin();
            }
            else if (time == 0) {
                winner = "Hider";
                time--;
            }
        }        
    }

    void checkForSeekerWin() {
        if (seeker) {
            seekerWon = seeker.seekerWon;
            if (seekerWon) {
                winner = "Seeker";
            }
        }
    }
}

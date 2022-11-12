using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinManager : MonoBehaviour
{
    public PlayerPortal seeker;
    private bool seekerWon;
    public string winner;
    public Timer timer;
    private int time;

    void Start()
    {
        time = GameObject.Find("Timer").GetComponent<Timer>().countdown;
        seeker = GameObject.FindGameObjectsWithTag("Seeker")[0].GetComponent<PlayerPortal>();
        seekerWon = false;
        winner = null;
    }

    // Update is called once per frame
    void Update()
    {
        if (!seekerWon) {
            if (time > 0) {
                time = GameObject.Find("Timer").GetComponent<Timer>().countdown;
                checkForSeekerWin();
            }
            else if (time == 0) { // pode dar problema aqui kk
                winner = "Hider";
                time--;
            }
        }        
    }

    void checkForSeekerWin() {
        if (seeker) {
            seekerWon = seeker.seekerWon;
            if (seekerWon)
                winner = "Seeker";
        }
    }
}

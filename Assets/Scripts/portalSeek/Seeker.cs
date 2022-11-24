using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seeker : MonoBehaviour
{

    [Header("Player Type")]
    private int total_hiders;
    private int points;
    public bool seekerWon;

    // Start is called before the first frame update
    void Start()
    {
        points = 0;
        seekerWon = false;
        gameObject.tag = "Seeker";
        total_hiders = GameObject.FindGameObjectsWithTag("Hider").Length;
        Debug.Log("Seekeer spawned");
    }

    void OnCollisionEnter(Collision collision) {

        if (collision.gameObject.tag == "Hider") {
            points++;

            if (points == total_hiders) {
                Debug.Log("Seeker Wins");
                seekerWon = true;
            }

            Debug.Log("mata");
        }
    }
}

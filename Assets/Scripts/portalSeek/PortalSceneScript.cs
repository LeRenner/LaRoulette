using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PortalSceneScript : MonoBehaviour
{
    public TMPro.TextMeshProUGUI txt;
    public Timer timer;
    private int time;
    private string winner;

    // Start is called before the first frame update
    void Start()
    {
        time = GameObject.Find("Timer").GetComponent<Timer>().countdown;
        winner = GameObject.Find("WinManager").GetComponent<WinManager>().winner;
    }

    // Update is called once per frame
    void Update()
    {

        if (winner == null)
            checkWinner();
    
        if (time > 0) {
            time = GameObject.Find("Timer").GetComponent<Timer>().countdown;
            txt.text = time.ToString();
        }
        
    }

    void checkWinner() {
        winner = GameObject.Find("WinManager").GetComponent<WinManager>().winner;
        if (winner != null)
            txt.text = winner + " wins!";
    }

}

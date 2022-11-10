using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class HiderPlayer : Player
{
    // Start is called before the first frame update

    public GameObject dummyPlayer;

    

    void Start(){
        dummyPlayer = GameObject.Find("PlayerHider");   
    
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

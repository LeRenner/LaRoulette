using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class Player : NetworkBehaviour
{
    // Start is called before the first frame update

    private GameManager gameManager;

    void Start(){
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        //gameManager.HandleNewPlayerEntrance(this);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

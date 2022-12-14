using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;


public class PlayerTransformer : ScriptableObject{

    private GameManager gameManager;

    CreatePlayerMessage cloneHiderPrefab(){
        return new CreatePlayerMessage{
            prefabPlayer = gameManager.hiderPlayer.ToString()        
        };
    }

    CreatePlayerMessage cloneSeekerPrefab(){
/*         var hiderPlayer = Instantiate(gameManager.hiderPlayer) as GameObject;
        player.transform.parent = hiderPlayer.transform; */
        //hiderPlayer.transform.parent = player.transform;
        return new CreatePlayerMessage {
            prefabPlayer = gameManager.seekerPlayer.ToString()         
        };
    }


    public CreatePlayerMessage instantiatePlayer(PlayerType type){
        if (type == PlayerType.HIDER){
            return cloneHiderPrefab();
        } else {
            return cloneSeekerPrefab();
        }
    }

    void Awake(){
        gameManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();

    }

}
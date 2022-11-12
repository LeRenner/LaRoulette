using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;


public enum PlayerType{
    HIDER,
    SEEKER
}

public class GameManager : MonoBehaviour
{
    public GameObject hiderPlayer;
    public GameObject seekerPlayer;

   /*  public GameObject hiderPlayerInstance;
    public GameObject seekerPlayerInstance; */
    private List<Player> listaDeJogadores = new List<Player>();

    public List<Player> GetListaDeJogadores(){
        return listaDeJogadores;
    }

    private PlayerTransformer playerTransformer;

    public ReplacePlayerMessage HandleNewPlayerEntrance(){

        var playerType = listaDeJogadores.Count == 0 ? PlayerType.SEEKER : PlayerType.HIDER;
        //listaDeJogadores.Add(newPlayer);
        return playerTransformer.instantiatePlayer(playerType);

    }

    void Start()
    {
        /* hiderPlayerInstance = Instantiate(hiderPlayer) as GameObject;
        seekerPlayerInstance = Instantiate(seekerPlayer) as GameObject;
        hiderPlayerInstance.SetActive(false);
        seekerPlayerInstance.SetActive(false); */
        playerTransformer = (PlayerTransformer) ScriptableObject.CreateInstance("PlayerTransformer");
    }

    // Update is called once per frame
    void Update(){
        
    }


}

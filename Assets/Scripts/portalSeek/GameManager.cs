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
    private List<PlayerType> listaDeJogadores = new List<PlayerType>();

    public List<PlayerType> GetListaDeJogadores(){
        return listaDeJogadores;
    }

    private PlayerTransformer playerTransformer;

    public ReplacePlayerMessage HandleNewPlayerEntrance(){

        var playerType = listaDeJogadores.Count == 0 ? PlayerType.SEEKER : PlayerType.HIDER;
        listaDeJogadores.Add(playerType);
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

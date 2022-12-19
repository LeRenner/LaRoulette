using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;


public struct CreatePlayerMessage : NetworkMessage
{
    public string prefabPlayer;
}


public class LRNetworkManager : NetworkManager{
        
    public override void OnStartServer()
    {
        base.OnStartServer();

        NetworkServer.RegisterHandler<CreatePlayerMessage>(OnCreateCharacter);
    }

    public override void OnClientConnect()
    {
        base.OnClientConnect();

        var gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        var characterMessage = gameManager.HandleNewPlayerEntrance();

        // you can send the message here, or wherever else you want
        

        NetworkClient.Send(characterMessage);
    }

    void OnCreateCharacter(NetworkConnectionToClient conn, CreatePlayerMessage message)
    {
        // playerPrefab is the one assigned in the inspector in Network
        // Manager but you can use different prefabs per race for example
        GameObject selected = null;
        foreach (var item in spawnPrefabs)
        {
            if (item.ToString() == message.prefabPlayer){
                selected = item;
            }
        }
        GameObject gameobject = Instantiate(selected);

        // Apply data from the message however appropriate for your game
        // Typically Player would be a component you write with syncvars or properties
        //Player player = gameobject.GetComponent();

        // call this to use this gameobject as the primary controller
        NetworkServer.AddPlayerForConnection(conn, gameobject);
    }

}

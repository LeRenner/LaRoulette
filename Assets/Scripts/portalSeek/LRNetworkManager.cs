using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using Unity.VisualScripting;

public struct ReplacePlayerMessage : NetworkMessage
{
    public string prefabPlayer;
}


public class LRNetworkManager : NetworkManager{
        
    public override void OnStartServer()
    {
        base.OnStartServer();

        NetworkServer.RegisterHandler<ReplacePlayerMessage>(ReplacePlayer);
    }

    public override async void OnServerSceneChanged(string sceneName)
    {
        base.OnServerSceneChanged(sceneName);
        Debug.Log("Client Scene changed server");
        await System.Threading.Tasks.Task.Delay(3000);
        var gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        if (gameManager != null) {
            var characterMessage = gameManager.HandleNewPlayerEntrance();
            NetworkClient.Send(characterMessage);
        }
    }
    public override async void OnClientSceneChanged()
    {
        base.OnClientSceneChanged();
        await System.Threading.Tasks.Task.Delay(3000);
        Debug.Log("Client Scene changed");
        var gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        if (gameManager != null) {
            var characterMessage = gameManager.HandleNewPlayerEntrance();
            NetworkClient.Send(characterMessage);
        }

    }

    public void ReplacePlayer(NetworkConnectionToClient conn, ReplacePlayerMessage message)
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
        GameObject oldPlayer = conn.identity.gameObject;
        //NetworkServer.Spawn(selected, conn);

        // Apply data from the message however appropriate for your game
        // Typically Player would be a component you write with syncvars or properties
        //Player player = gameobject.GetComponent();
        //Debug.Log(gameObject.ToString());
        
        // call this to use this gameobject as the primary controller
        //var player = GameObject.FindGameObjectWithTag("Player");
        //var camera = player.GetComponent("Camera");
        //await System.Threading.Tasks.Task.Delay(10000);
        var newPlayer = Instantiate(selected);
        NetworkServer.ReplacePlayerForConnection(conn, newPlayer, true);
    
        newPlayer.AddComponent<Camera>();
        //await System.Threading.Tasks.Task.Delay(10000);
        Destroy(oldPlayer, 1.0f);
        

        //NetworkServer.AddPlayerForConnection(conn, gameobject);
    }


    public void AddPlayer(NetworkConnectionToClient conn, ReplacePlayerMessage message)
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
        //GameObject oldPlayer = conn.identity.gameObject;
        //NetworkServer.Spawn(selected, conn);

        // Apply data from the message however appropriate for your game
        // Typically Player would be a component you write with syncvars or properties
        //Player player = gameobject.GetComponent();
        //Debug.Log(gameObject.ToString());
        
        // call this to use this gameobject as the primary controller
        //var player = GameObject.FindGameObjectWithTag("Player");
        //var camera = player.GetComponent("Camera");
        //await System.Threading.Tasks.Task.Delay(10000);
        var newPlayer = Instantiate(selected);
        NetworkServer.AddPlayerForConnection(conn, newPlayer);
    
        newPlayer.AddComponent<Camera>();
        //await System.Threading.Tasks.Task.Delay(10000);
        //Destroy(oldPlayer, 1.0f);
        

        //NetworkServer.AddPlayerForConnection(conn, gameobject);
    }

    public override void OnServerAddPlayer(NetworkConnectionToClient conn)
    {
        //await System.Threading.Tasks.Task.Delay(3000);
        base.OnServerAddPlayer(conn);
        /* var gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        if (gameManager != null) {
            var characterMessage = gameManager.HandleNewPlayerEntrance();
            NetworkClient.Send(characterMessage);
        } */        
    }

}

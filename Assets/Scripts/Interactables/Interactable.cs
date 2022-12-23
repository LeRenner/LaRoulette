using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public enum ButtonState : byte
{
    Open, Closed
}


public class Interactable : NetworkBehaviour
{
	public string sceneName;
    private bool clicked = false;
    [SyncVar]
    public ButtonState buttonState;
    [SyncVar]
    public int readys = 0;


    [Client]
    void OnMouseUpAsButton()
    {
	Debug.Log(clicked);
	clicked = !clicked;
        CmdSetButtonState(clicked);


    }

    [Command(requiresAuthority = false)]
    public void CmdSetButtonState(bool click,NetworkConnectionToClient sender = null)
    {


        if (buttonState == ButtonState.Open)
        {
	    buttonState = ButtonState.Closed;
        }

        if (buttonState == ButtonState.Closed)
            buttonState = ButtonState.Open;

	if (click)
		readys += 1;
	else
		readys -= 1;
	CmdStartMatch();


    }

    public void CmdStartMatch()
    {
        if (NetworkServer.connections.Count == readys)
        {
            RouletteBehaviour.Speed = Random.Range(600, 800);
            RouletteBehaviour.isSpinning = false;
        }
        return;
    }

    public static void startMatch(string sceneName)
    {
        NetworkManager.singleton.ServerChangeScene(sceneName);
        return;
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using Mirror;
using TMPro;

public class goaltrigger : NetworkBehaviour
{
	
	#region Singleton

	public static goaltrigger instance;
	
	private void Awake()
	{
		instance = this;
	}
	#endregion

	[SyncVar]
	public int p1Score = 0;
	[SyncVar]
	public int p2Score = 0;
	public TextMeshProUGUI p1ScoreText;
	public TextMeshProUGUI p2ScoreText;
	public TIMER TimerOffScript;
	public PopUpSystem popUp;
	bool trigger = false;
	bool trigger2 = false;
	string pop;
	float timer = 0;

	[Command(requiresAuthority = false)]
	private void Update()
	{

		if (TimerOffScript.isTrigger() && !trigger)
		{
			trigger = true;
			//
			if (p1Score > p2Score) {pop = "Player 1 Wins";}
			else if (p2Score > p1Score) {pop = "Player 2 Wins";}
			else if (p1Score == p2Score) {pop = "Tie";}

			popUp.PopUp(pop);

		}

		if (trigger){
			timer += Time.deltaTime;
			if(timer > 5 && !trigger2){
				popUp.PopClose();
				trigger2 = true;
			}
			if(timer > 7){
				NetworkManager.singleton.ServerChangeScene("Lobby");
			}
		}

		RpcScoreUp();
		
	}
	[ClientRpc]
	public void RpcScoreUp()
	{
		
		if (!isLocalPlayer)
		{
			p1ScoreText.text = p1Score.ToString();
			p2ScoreText.text = p2Score.ToString();
		}
	}
	

}

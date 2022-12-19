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

    public GameObject playerPrefab;
    public GameObject[] players;
	public List<GameObject> redTeam = new List<GameObject>();
	public List<GameObject> blueTeam = new List<GameObject>();
	int i;
	int lastSize;
	//public PlayerController sc;

	
	[Command(requiresAuthority = false)]
	private void Update()
	{
			if (players.Length != 2)
			{
				players = GameObject.FindGameObjectsWithTag("Player");
				if (players.Length > lastSize)
				{
						lastSize = players.Length;
						i = 0;
					while (i < players.Length){
						if (i % 2 == 0){
							if(!redTeam.Contains(players[i]))
								redTeam.Add(players[i]);
						}
						else{
							if(!blueTeam.Contains(players[i]))
								blueTeam.Add(players[i]);
						}
						i += 1;

					}
				}
			}

				foreach (GameObject player in players)
				{
					Debug.Log(player.GetComponent<PlayerScore>().Points);

					
				}

		if (TimerOffScript.isTrigger() && !trigger)
		{
			trigger = true;
			//
			if (p1Score > p2Score)
			{
				pop = "Player 1 Wins";
				foreach(GameObject player in redTeam){
					PlayerScore kObject = player.gameObject.GetComponent<PlayerScore>()	;
       				kObject.AddVictory();
				}
			}
			else if (p2Score > p1Score)
			{
				pop = "Player 2 Wins";
				foreach(GameObject player in blueTeam){
					PlayerScore kObject = player.gameObject.GetComponent<PlayerScore>()	;
       				kObject.AddVictory();
				}
			}
			else if (p1Score == p2Score)
			{
				pop = "Tie";
			}



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

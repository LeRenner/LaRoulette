using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class MazeManager : NetworkBehaviour
{
	public TIMER TimerOffScript;
	bool trigger = false;
	public GameObject Walls;
	private  PlayerScore kObject;

	private void OnTriggerEnter(Collider other)
	{
		if(other.CompareTag("Player"))
		{
			kObject = other.gameObject.GetComponent<PlayerScore>()	;
       		kObject.AddVictory();
			NetworkManager.singleton.ServerChangeScene("Lobby");
		}
	}
	void Update()
	{
		if (TimerOffScript.isTrigger() && !trigger)
		{
			trigger = true;
			Walls.SetActive(false);
		}
	}

}

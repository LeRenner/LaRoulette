using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class MazeManager : NetworkBehaviour
{
	public TIMER TimerOffScript;
	bool trigger = false;
	public GameObject Walls;
	private void OnTriggerEnter(Collider other)
	{
		if(other.CompareTag("Player"))
		{
			NetworkManager.singleton.ServerChangeScene("Lobby");
		}
	}
	void Update()
	{
		if (TimerOffScript.isTrigger() && !trigger)
		{
			Debug.Log("Oi");
			trigger = true;
			Walls.SetActive(false);
		}
	}

}

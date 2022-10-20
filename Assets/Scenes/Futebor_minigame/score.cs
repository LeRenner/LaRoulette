using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine;
using Mirror;

public class score : NetworkBehaviour
{
	
	public goaltrigger goalTrigger;
	private void Start()
	{
		goalTrigger = goaltrigger.instance;
	}
	
	private void OnTriggerEnter(Collider other)
	{
		if(other.CompareTag("P1Goal"))
		{
			setScore("P1Goal");
		}
		else if (other.CompareTag("P2Goal"))
		{
			setScore("P2Goal");
		}
	}
	
	[Command(requiresAuthority = false)]
	private void setScore(string Player)
	{
		if(Player == "P1Goal"){
			goalTrigger.p1Score++;
		}

		else if (Player == "P2Goal")
		{
			goalTrigger.p2Score++;
		}
		Debug.Log(goalTrigger.p1Score + " to " + goalTrigger.p2Score);
		transform.localPosition = new Vector3(0, 20, 0);
	}		
}

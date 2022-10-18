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
			goalTrigger.p1Score++;
			Debug.Log(goalTrigger.p1Score + " to " + goalTrigger.p2Score);
			transform.localPosition = new Vector3(0, 20, 0);
		}

		else if (other.CompareTag("P2Goal"))
		{
			goalTrigger.p2Score++;
			Debug.Log(goalTrigger.p1Score + " to " + goalTrigger.p2Score);
			transform.position = new Vector3(0, 20, 0);
		}
	}
}

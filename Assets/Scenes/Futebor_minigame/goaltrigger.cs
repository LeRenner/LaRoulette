using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine;
using Mirror;

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

}

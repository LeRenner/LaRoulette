using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
	[Command]
	private void Update()
	{
		p1ScoreText.text = p1Score.ToString();
		p2ScoreText.text = p2Score.ToString();
		
	}

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class Boundaries : NetworkBehaviour
{
	private void OnTriggerEnter(Collider other)
	{

        if (other.CompareTag("Out"))
		{
		    other.transform.localPosition = new Vector3(0, 20, 0);
		}        
        else if (other.CompareTag("Player"))
		{
            Debug.Log("Oi");
		    other.transform.localPosition = new Vector3(0, 10, 0);
		} 
        other.GetComponent<Rigidbody>().velocity = Vector3.zero;

	}
	
}

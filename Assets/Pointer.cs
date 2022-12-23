using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pointer : MonoBehaviour
{
    void OnTriggerEnter(Collider col)
    {
        Debug.Log(col.gameObject.name);
        if (Equals(col.gameObject.name, "1"))
        {
            RouletteBehaviour.sceneName = 1;
        }
        else if (Equals(col.gameObject.name, "2"))
        {
            RouletteBehaviour.sceneName = 2;
        }
    }
}
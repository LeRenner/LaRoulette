using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RouletteBehaviour : MonoBehaviour
{
    public static float Speed = 0;
    public static bool isSpinning = true;
    public static int sceneName = 0;
    public GameObject Pointer;

    void Update()
    {
        Rotate();
    }

    void Rotate()
    {
        transform.Rotate(0, 0, Speed * Time.deltaTime);

        if (isSpinning == false && Speed > 0)
        {
            Stop();
        }

        if (isSpinning == false && Speed == 0)
        {
            if (sceneName == 1)
            {
                Interactable.startMatch("minigame1");
                isSpinning = true;
            }
            else if (sceneName == 2)
            {
                Interactable.startMatch("minigame2");
                isSpinning = true;
            }
        }
    }

    public void Stop()
    {
        Speed = Speed - 0.1f;
        if (Speed <= 0)
        {
            Pointer.GetComponent<BoxCollider>().enabled = true;
            Speed = 0;
        }
    }
}

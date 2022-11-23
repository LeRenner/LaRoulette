using Mirror;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerScore : NetworkBehaviour
{
    public int Points;
    public static int Score;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.J)){
            Points += 1;
            Score += Points;
        }

        if (Input.GetKeyDown(KeyCode.K)){
            Debug.Log("Points" + Points);
            Debug.Log("Score" + Score);

        }

        
    }

    public void AddVictory()
    {
        Score += 1;
    }

}

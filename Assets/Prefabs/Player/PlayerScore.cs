using Mirror;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerScore : NetworkBehaviour
{
    public int Points;
    public static int Score;
    public string Team;

    void Update()
    {
 

        if (Input.GetKeyDown(KeyCode.K)){
            Debug.Log("Points" + Points);
            Debug.Log("Score" + Score);

        }

        
    }

    public void AddVictory()
    {
        Score += 1;
    }

    public void AddTeam(string teamName)
    {
        Team = teamName;
    }  
}

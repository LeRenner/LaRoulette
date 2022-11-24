using UnityEngine;

// namespace Mirror.Examples.AdditiveLevels 
public class PortalGate : MonoBehaviour
{
    // Type ==  true, in, left mouse click
    public bool inPortal;
    public string playerName;
    public PortalManager portalMan;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Portal created");
        // se existir outro portal, este eh o out;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision collision) {

        if (collision.gameObject.name == playerName && inPortal) {
            Transform pos = portalMan.GetComponent<PortalManager>().checkPortal(inPortal);

            var player = GameObject.Find(playerName);
            player.transform.position = pos.position;
        }
    }

}


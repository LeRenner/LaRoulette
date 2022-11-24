using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// namespace Mirror.Examples.AdditiveLevels
public class Bullet : MonoBehaviour
{        
    public string playerName;
    public int playerID;

    [Header("Portal Creation")]
    public PortalGate portal;
    public PortalManager portalMan;


    [Header("Type")]
    public bool Type; // false == OUT

    void Start() {
    }

    // Update is called once per frame
    void Update()
    {
    }

    void OnCollisionEnter(Collision collision) {

        if (collision.gameObject.name == "Plane"  || collision.gameObject.name == "PolyShape")
        {
            
            ContactPoint contact = collision.contacts[0];
            Quaternion rotation = Quaternion.FromToRotation(Vector3.up, contact.normal);

            Destroy(gameObject);

            PortalGate p = Instantiate(portal, transform.position, rotation);
            p.GetComponent<PortalGate>().playerName = playerName;
            p.GetComponent<PortalGate>().inPortal = Type;

            if (Type) 
                    p.GetComponent<Renderer>().material.color = Color.red;
            else p.GetComponent<Renderer>().material.color = Color.blue;

            portalMan.GetComponent<PortalManager>().addPortal(p, Type);

        }
        

    }

    
}


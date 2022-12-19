using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// namespace Mirror.Examples.AdditiveLevels 
public class PortalManager : MonoBehaviour
{

    private static List<PortalGate> portal_right = new List<PortalGate>();
    private static List<PortalGate> portal_left = new List<PortalGate>();

    [Header("Portal Logic")]
    public PortalGate inPortal;
    public PortalGate outPortal;

    void Awake() {

        // Deletion
        deleteObjectsList(portal_right);
        deleteObjectsList(portal_left);

        // Creation
        portal_right = new List<PortalGate>();
        portal_left = new List<PortalGate>();
        
    }

    public Transform checkPortal(bool is_in) {
        Transform pos = null;

        if (is_in && portal_right != null) {
            pos = portal_right[0].gameObject.transform;
        }
        else if (is_in == false && portal_left != null) {
            pos = portal_left[0].gameObject.transform;
        }

        return pos;
    }

    private void deleteObjectsList(List<PortalGate> objects) {
        if(objects != null) {
            for (int i = 0; i < objects.Count; i++) 
                Destroy(objects[i].gameObject);

            objects.Clear();
            objects = null;
        }
    }

    public void addPortal(PortalGate p, bool type) {
        if (type) {
            deleteObjectsList(portal_left);

            portal_left = new List<PortalGate>();
            portal_left.Add(p);
        }
        else {
            deleteObjectsList(portal_right);

            portal_right = new List<PortalGate>();
            portal_right.Add(p);
        }

    }

    void onDestroy() {
        deleteObjectsList(portal_left);
        deleteObjectsList(portal_right);
    }
}

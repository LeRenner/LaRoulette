using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
//using UnityEditor;

public class MovementTests
{
    // A Test behaves as an ordinary method
 

    [UnityTest]
    public IEnumerator AssertTimerProperties()
    {

        GameObject gameGameObject = MonoBehaviour.Instantiate(Resources.Load<GameObject>("Timer"));
        yield return null;
        //Debug.Log(gameGameObject.layer);
        Assert.AreEqual(gameGameObject.layer, 5);
        // Use the Assert class to test conditions
    }

    [UnityTest]
    public IEnumerator AssertPropertiesOfDifferentPlayerPrefabs()
    {

        
        yield return null;       
     
        // Use the Assert class to test conditions
    }

    [UnityTest]
    public IEnumerator AssertPortalGateProperties()
    {

        GameObject gameGameObject = MonoBehaviour.Instantiate(Resources.Load<GameObject>("PortalGate"));
        yield return null;       
        Assert.NotNull(gameGameObject.GetComponent<SphereCollider>());
        // Use the Assert class to test conditions
    }


    [UnityTest]
    public IEnumerator AssertPlayerHasMoved()
    {

        
        yield return null;       
        
        // Use the Assert class to test conditions
    }


    [UnityTest]
    public IEnumerator AssertButtonProperties()
    {

        GameObject gameGameObject = MonoBehaviour.Instantiate(Resources.Load<GameObject>("Button"));
        yield return null;       
        Assert.NotNull(gameGameObject.GetComponent<BoxCollider>());
        // Use the Assert class to test conditions
    }

    // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    // `yield return null;` to skip a frame.
    [UnityTest]
    public IEnumerator CheckPlayerHasRigidBody()
    {
        // Use the Assert class to test conditions.
        // Use yield to skip a frame.
        GameObject gameGameObject = MonoBehaviour.Instantiate(Resources.Load<GameObject>("Player"));
        yield return null;       
        Assert.IsNotNull(gameGameObject.GetComponent<Rigidbody>());
    }
}

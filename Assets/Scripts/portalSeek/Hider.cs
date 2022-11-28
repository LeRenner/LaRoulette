using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror.Examples.AdditiveLevels;

public class Hider : MonoBehaviour
{

    private class hidersLinkedList {
        private Hider[] hiders;
        private int currentHiderIndex;
        
        public void setHiders(Hider[] h) {
            hiders = h;
            currentHiderIndex = 0;
        }

        public void nextIndex() {
            if (currentHiderIndex < hiders.Length - 1)
                currentHiderIndex = currentHiderIndex + 1;
            else
                currentHiderIndex = 0;
        }

        public Transform getTransform() {
            return hiders[currentHiderIndex].transform;
        }
    }

    [Header("Fire")]
    public Weapon weapon;
    public PortalManager portalMan;

    public bool isDead;
    private hidersLinkedList hiderLinkedList;
    private string winner;

    // Start is called before the first frame update
    void Start()
    {
        isDead = false;
        Debug.Log("Hider spawned");

        var foundHiders = FindObjectsOfType<Hider>();
        if (foundHiders.Length != 0) {
            Debug.Log(foundHiders + ":" +  foundHiders.Length);
            hiderLinkedList = new hidersLinkedList();
            hiderLinkedList.setHiders(foundHiders);
        }

    }

    // Update is called once per frame
    void Update()
    {
        GetFireInput();
    }


    private void GetFireInput()
    {
        // checar se o player eh hider tbm

        if (Input.GetButtonDown("Fire1")) {
            Debug.Log("Left click!");

            GameObject bullet = Instantiate(weapon.weaponBullet, weapon.weaponFirePosition.position, weapon.weaponFirePosition.rotation);
            bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * weapon.weaponSpeed;
            gameObject.name = GetInstanceID().ToString();
            bullet.GetComponent<Bullet>().playerName = gameObject.name;
            bullet.GetComponent<Bullet>().Type = true;

            Destroy(bullet, weapon.weaponLife);
        }
        if (Input.GetButtonDown("Fire2")) {
            Debug.Log("Right click!");

            GameObject bullet = Instantiate(weapon.weaponBullet, weapon.weaponFirePosition.position, weapon.weaponFirePosition.rotation);

            bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * weapon.weaponSpeed;
            gameObject.name = GetInstanceID().ToString();
            bullet.GetComponent<Bullet>().playerName = gameObject.name;
            bullet.GetComponent<Bullet>().Type = false;

            Destroy(bullet, weapon.weaponLife);
        }
    }

    void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.tag == "Seeker") {
            // Destroy(gameObject); // talvez so n deixar mover
            Debug.Log("morre");
            Destroy(GetComponent("PlayerController")); 
            isDead = true;
        }
    }

    void changeCamera() {
        if (Input.GetButtonDown("L") && isDead) {
            Debug.Log("Changing camera for Hider.");
            
            hiderLinkedList.nextIndex();
            var temporaryTransform = hiderLinkedList.getTransform();

            Camera.main.transform.position = temporaryTransform.position;
            Camera.main.transform.rotation = temporaryTransform.rotation;
        }
    }

}

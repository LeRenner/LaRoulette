using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hider : MonoBehaviour
{

    [Header("Fire")]
    public Weapon weapon;
    public PortalManager portalMan;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.tag = "Hider";
        // seekerWon = false;
        Debug.Log("Hider spawned");
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
            // Debug.Log(weapon.weaponFirePosition.position);
            // Debug.Log(weapon.weaponFirePosition.rotation);
            bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * weapon.weaponSpeed;
            // bullet.GetComponent<Rigidbody>().velocity = Vector3.zero;
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
            Destroy(gameObject); // talvez so n deixar mover
            Debug.Log("morre");
        }
    }

}

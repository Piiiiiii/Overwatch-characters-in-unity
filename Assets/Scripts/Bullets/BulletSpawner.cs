using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSpawner : MonoBehaviour {

    public Bullet bullet;
    public Transform cam;
    public Transform spawner;

    void FixedUpdate () 
	{
        if (Input.GetKey(KeyCode.Mouse0))
        {
            Bullet newBullet = Instantiate(bullet,transform.position,transform.rotation);
        }
	}
}

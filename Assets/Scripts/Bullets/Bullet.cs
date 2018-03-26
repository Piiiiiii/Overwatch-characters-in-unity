using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    public float bulletSpeed;
    void FixedUpdate () 
	{
        transform.Translate(transform.forward * bulletSpeed * Time.deltaTime, Space.World);
	}
}

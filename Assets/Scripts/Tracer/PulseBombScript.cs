using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PulseBombScript : MonoBehaviour {

    private Vector3 explosionPos;
    private Transform objectStuckTo;

    public float explosionTimer = 1.5f;
    public float power = 10f;
    public float radius = 5f;

    private void OnTriggerEnter(Collider collider)
    {
        transform.parent = collider.transform;
        Destroy(GetComponent<Rigidbody>());
        StartCoroutine(BombTimer());
    }

    IEnumerator BombTimer()
    {
        yield return new WaitForSeconds(1.5f);
        Explode();
    }

    void Explode()
    {
        explosionPos = transform.position;

        Collider[] colliders = Physics.OverlapSphere(transform.position, radius);

        foreach(Collider nearbyObject in colliders)
        {
            Rigidbody rb = nearbyObject.GetComponent<Rigidbody>();

            if(rb != null)
            {
                rb.AddExplosionForce(power, explosionPos, radius, 1f);
            }
        }
        Destroy(gameObject);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Player))]
public class BlinkAbility : MonoBehaviour {

    public float collisionHeight = 0.5f;
    public float range;
    //public bool isBlinking = false;

    private Rigidbody RB;
    private Player playerScript;
    //public LayerMask obstacleMask;

    void Start()
    {
        RB = GetComponent<Rigidbody>();
        playerScript = GetComponent<Player>();
    }

    void Update()
    {
        if (playerScript.useAbility2)
        {
            Blink();
            playerScript.useAbility2 = false;
        }
    }

    void Blink()
    {
        Vector3 movementInput = playerScript.moveDirection;
        //Ray ray = new Ray(new Vector3(transform.position.x, transform.position.y - collisionHeight, transform.position.z), transform.forward);
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray.origin,ray.direction, out hit, range))
        {
            if (movementInput == Vector3.zero)
            {
                RB.MovePosition(RB.position + transform.forward * (hit.distance - transform.localScale.z * 0.5f));
            } else
            {
                RB.MovePosition(RB.position + transform.TransformDirection(movementInput) * (hit.distance - transform.localScale.z * 0.5f));
            }
        }
        else if (movementInput == Vector3.zero)
        {
            RB.MovePosition(RB.position + transform.forward * range);
        }
        else
        {
            RB.MovePosition(RB.position + transform.TransformDirection(movementInput) * range);
        }
    }
}

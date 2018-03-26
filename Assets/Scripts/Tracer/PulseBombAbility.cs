using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Player))]
public class PulseBombAbility : MonoBehaviour {

    public GameObject bombPrefab;
    private Camera playerCam;
    private Player playerScript;

    public float throwAngle = 45f;
    public float throwForce = 5f;

    private void Start()
    {
        playerCam = GetComponentInChildren<Camera>();
        playerScript = GetComponent<Player>();
    }

    void Update ()
    {
        if (playerScript.useAbility4)
        {
            ThrowBomb();
            playerScript.useAbility4 = false;
        }
	}

    void ThrowBomb()
    {
        GameObject spawnedBomb = Instantiate(bombPrefab, playerCam.transform.position + playerCam.transform.forward, transform.rotation);
        Physics.IgnoreCollision(spawnedBomb.GetComponent<Collider>(), gameObject.GetComponent<Collider>());
        spawnedBomb.GetComponent<Rigidbody>().AddForce(new Vector3(playerCam.transform.forward.x, playerCam.transform.forward.y + throwAngle, playerCam.transform.forward.z) * throwForce, ForceMode.Impulse);
    }
}

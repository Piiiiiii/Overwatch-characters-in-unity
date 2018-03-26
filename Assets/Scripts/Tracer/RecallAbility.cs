using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Player))]
public class RecallAbility : MonoBehaviour {

    public Transform[] movementPoints;
    private Player playerScript;

    float speed = 0.5f;
    private bool isRecalling;

	void Start ()
    {
        playerScript = GetComponent<Player>();
        StartCoroutine(UpdatePoints());
	}
	
	void Update ()
    {
        if (playerScript.useAbility3)
        {
            Recall();
            playerScript.useAbility3 = false;
        }
	}

    IEnumerator UpdatePoints()
    {
        float refreshRate = 1f;
        while(!isRecalling)
        {
            for(int i = 0; i < movementPoints.Length - 1; i++)
            {
                Transform temporaryTransform = movementPoints[movementPoints.Length - 1];

                for (int j = movementPoints.Length - 1; j > 0; j--)
                {
                    movementPoints[j] = movementPoints[j - 1];
                    movementPoints[j - 1] = temporaryTransform;
                }
                movementPoints[0].position = transform.position;

                yield return new WaitForSeconds(refreshRate);
            }
        }
    }

    IEnumerator MoveToPoints()
    {
        gameObject.GetComponent<Collider>().enabled = false;

        int targetPointIndex = 0;
        Vector3 targetPoint = movementPoints[targetPointIndex].position;

        while (isRecalling)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPoint, speed * Time.deltaTime);
            if(targetPointIndex >= movementPoints.Length - 1)
            {
                isRecalling = false;
            }
            else if(transform.position == targetPoint)
            {
                targetPointIndex += 1;
                targetPoint = movementPoints[targetPointIndex].position;
                yield return null;
            }
        }
        //foreach(Transform p in movementPoints)
        //{
        //    transform.position = Vector3.MoveTowards(transform.position, p.position, speed * Time.deltaTime);
        //    yield return null;
        //}
        gameObject.GetComponent<Collider>().enabled = true;
    }
    void Recall()
    {
        isRecalling = true;
        StartCoroutine(MoveToPoints());

    }
}

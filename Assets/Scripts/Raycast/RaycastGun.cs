using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastGun : MonoBehaviour {

    public float damage = 10f;
    public float range = 100f;
	public float fireRate = 15f;
	public float maxSpread = 5f;
	public float spreadRate = 1f;
	public float hitForce;

	private float fireTime = 0.0f;
	private float currentSpread = 0.0f;

	private bool isReloading = false;

	private Quaternion fireRotation;

	private Player playerStats;
	private Camera fpsCam;
	private LineRenderer laserLine;
	public Transform gunBarrel;
	public GameObject hitEffect;

	private float nextTimeToFire = 0f;
	IEnumerator currentCoroutine;

	private void Start()
	{
		laserLine = GetComponent<LineRenderer>();
		fpsCam = GetComponentInParent<Camera>();
		laserLine.SetPosition(0, gunBarrel.localPosition);
		playerStats = GetComponentInParent<Player>();
	}

	void Update () 
	{
		if (isReloading)
		{
			return;
		}
		if(playerStats.currentAmmo <= 0 || (Input.GetKeyDown(KeyCode.R) && playerStats.currentAmmo < playerStats.maxAmmo))
		{
			StartCoroutine(Reload());
			return;
		}
		if (Input.GetButton("Fire1"))
        {
			fireTime += Time.deltaTime;

			if (Time.time >= nextTimeToFire)
			{
				nextTimeToFire = Time.time + 1f / fireRate;

				Shoot();

				if (currentCoroutine != null)
				{
					StopCoroutine(currentCoroutine);
				}
				currentCoroutine = ShotEffect();
				StartCoroutine(currentCoroutine);
			}
		}
		else
		{
			fireTime = 0.0f;
		}
	}

	IEnumerator Reload()
	{
		isReloading = true;
		yield return new WaitForSeconds(playerStats.reloadTime);
		playerStats.currentAmmo = playerStats.maxAmmo;
		isReloading = false;
	}

    void Shoot()
    {

		currentSpread = Mathf.Lerp(0.0f, maxSpread, fireTime / spreadRate);

		RaycastHit hit;
		Ray ray = new Ray(fpsCam.transform.position, fpsCam.transform.forward);

		fireRotation = Quaternion.RotateTowards(transform.localRotation, Random.rotationUniform, Random.Range(0, currentSpread));
		
		if (Physics.Raycast(ray.origin, fireRotation * ray.direction, out hit, range))
		{
			laserLine.SetPosition(1, transform.InverseTransformPoint(hit.point));
			GameObject hitGO = Instantiate(hitEffect, hit.point, Quaternion.LookRotation(hit.normal));
			Destroy(hitGO, 1f);

			if(hit.rigidbody != null)
			{
				hit.rigidbody.AddForce(-hit.normal * hitForce);
			}
		}
		else
		{
			laserLine.SetPosition(1, transform.InverseTransformVector(fireRotation * ray.direction)* range);
		}
		playerStats.currentAmmo -= 1;
	}

	IEnumerator ShotEffect()
	{
		laserLine.enabled = true;
		yield return new WaitForSeconds(1/(fireRate * 2));
		laserLine.enabled = false;
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(PlayerController))]
public class Player : MonoBehaviour {

    public int maxAmmo;
    public int currentAmmo;
    public float reloadTime;
    [HideInInspector]
    public bool useAbility4, useAbility3, useAbility2, useAbility1;

    public float movementSpeed;
	public float jumpForce;

	private float mouseX;
	private float mouseY;

    private PlayerController controller;

    [HideInInspector]
    public Vector3 moveDirection;
    private Vector3 moveAmount;
	private Vector3 smoothMoveVelocity;

    public float rayCastLength;

	void Start ()
    {

        controller = GetComponent<PlayerController>();
		Cursor.lockState = CursorLockMode.Locked;
	}
	void Update()
	{
        // Mouse input
		mouseX += Input.GetAxisRaw("Mouse X");
		mouseY -= Input.GetAxisRaw("Mouse Y");
		mouseY = Mathf.Clamp(mouseY, -90f, 90f);
        controller.MouseLook(mouseY);

        // Movement input
        moveDirection = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")).normalized;
		Vector3 moveVelocity = moveDirection * movementSpeed;
		moveAmount = Vector3.SmoothDamp(moveAmount, moveVelocity, ref smoothMoveVelocity, 0.05f);
        controller.CharacterMovement(moveAmount, mouseX);

        // Jump
        if (Input.GetKeyDown(KeyCode.Space))
		{
            Ray ray = new Ray(transform.position, -transform.up);
            RaycastHit hit;
            Debug.DrawRay(ray.origin, ray.direction, Color.red);
            if (Physics.Raycast(ray, out hit, rayCastLength))
            {
                controller.Jump(jumpForce);
            }
		}

        // Ability inputs
        if (Input.GetKeyDown(KeyCode.Q))
        {
            useAbility4 = true;
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            useAbility3 = true;
        }
        if (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetMouseButtonDown(1))
        {
            useAbility2 = true;
        }
        if (Input.GetMouseButtonDown(0))
        {
            useAbility1 = true;
        }
    }
}

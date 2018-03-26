using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour {

    Rigidbody playerRigidbody;
    Camera playerCamera;

    Vector3 velocity;
    Vector3 moveAmount;

    float mouseX;
    float mouseY;

	void Start ()
    {
        playerRigidbody = GetComponent<Rigidbody>();
        playerCamera = GetComponentInChildren<Camera>();
	}

    void FixedUpdate()
    {
        playerRigidbody.MovePosition(playerRigidbody.position + transform.TransformDirection(moveAmount) * Time.deltaTime);
        playerRigidbody.MoveRotation(Quaternion.Euler(new Vector3(0, mouseX)));
        playerCamera.transform.localEulerAngles = new Vector3(mouseY, 0);
    }

    public void MouseLook(float mouseVerticalInput)
    {
        mouseY = mouseVerticalInput;
    }

    public void CharacterMovement(Vector3 _moveAmount, float mouseHorizontalInput)
    {
        moveAmount = _moveAmount;
        mouseX = mouseHorizontalInput;
    }

    public void Jump(float _jumpForce)
    {
        playerRigidbody.AddForce(transform.up * _jumpForce, ForceMode.Impulse);
    }
}

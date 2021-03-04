using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Movement : MonoBehaviour
{

	public float speed = 4.0f;
	public float gravity = -9.8f;

	private CharacterController _charCont;

	// Use this for initialization
	void Start()
	{
		//hide mouse whilst playing
		Cursor.visible = false;

		_charCont = GetComponent<CharacterController>();
	}

	// Update is called once per frame
	void Update()
	{
		float deltaX = Input.GetAxis("Horizontal") * speed;
		float deltaZ = Input.GetAxis("Vertical") * speed;
		Vector3 movement = new Vector3(deltaX, 0, deltaZ);
		movement = Vector3.ClampMagnitude(movement, speed); //Limits the max speed of the player

		// movement.y = gravity;

		movement *= Time.deltaTime;     //Ensures the speed the player moves does not change based on frame rate
		movement = transform.TransformDirection(movement);
		_charCont.Move(movement);
	}
}
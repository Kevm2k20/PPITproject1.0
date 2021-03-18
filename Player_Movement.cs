using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System.Text;

public class Player_Movement : MonoBehaviour
{

	public float speed = 4.0f;
	public float gravity = -9.8f;

	private CharacterController _charCont;
	private PlayerData playerData;

	// Use this for initialization
	void Start()
	{
		//hide mouse whilst playing
		Cursor.visible = false;

		_charCont = GetComponent<CharacterController>();
		playerData = GetComponent<PlayerData>();
		playerData.health = 100;
		StartCoroutine(Upload(playerData.Stringify()));
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

	void OnCollision3D(Collision health)
	{
		playerData.health--;
		Debug.Log(playerData.health);
	}

	IEnumerator Upload(string profile)
	{
		// after the http:// on the next line add the localhost:3000/(databaseName)
		using (UnityWebRequest request = new UnityWebRequest("http://localhost:3000/PPIT", "POST"))
		{
			request.SetRequestHeader("Content-Type", "application/json");
			byte[] rawBody = Encoding.UTF8.GetBytes(profile);

			//request.uploadHandler = new UploadHandler(rawBody);
			//request.downloadHandler = new DownloadhandlerBuffer();

			yield return request.SendWebRequest();

			if (request.result == UnityWebRequest.Result.ProtocolError)
			{
				Debug.Log(request.error);
			}
			else
			{
				Debug.Log(request.downloadHandler.text);
			}
		}
	}
}
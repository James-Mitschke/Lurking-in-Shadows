using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_PlayerCamera : MonoBehaviour 
{
	public GameObject PB;
	GameObject PCamera;
	Vector3 Offset;
	float yRot = 0.0f;
	float xRot = 0.0f;


	// Use this for initialization
	void Start () 
	{
		PCamera = gameObject;
		Offset = (gameObject.transform.position - PB.transform.position);
	}

	void Update ()
	{
		float MouseX = Input.GetAxis("Mouse X") * 5.0f;
		float MouseY = Input.GetAxis("Mouse Y") * 2.5f;

		yRot += MouseX;
		xRot -= MouseY;
		xRot = Mathf.Clamp(xRot, -90, 90);

		transform.eulerAngles = new Vector3(xRot, yRot, 0.0f);
	}

	// Update is called once per frame
	void LateUpdate () 
	{
		if(PCamera.transform.position != PB.transform.position + Offset)
		{
			PCamera.transform.position = PB.transform.position + Offset;
		}
	}
}

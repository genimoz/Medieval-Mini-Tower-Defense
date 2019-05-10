/*
* Author : onogenio
* Copyright (c) 2017 Patriano Genio
* All Rights Reserved.
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ScrollBoundary
{
	public float min = 10f;
	public float max = 60f;

	public float scrollSpeed = 1f;
}

[System.Serializable]
public class PanBoundary
{
	public float panSpeed = 50f;
	public float panBorder = 10f;

	public float minY = 10f;
	public float maxY = 60f;

	public Vector2 panLimit;
}

public class CameraController : MonoBehaviour 
{
	public ScrollBoundary scrolling;

	public PanBoundary panning;

	private Vector3 cameraPositionOrigin;

	void Start()
	{
		cameraPositionOrigin = transform.position;
	}

	void Update()
	{
		if(GameManager.isGameEnded)
		{
			this.enabled = false;
			return;
		}

		// Camera Panning
		Panning();

		//cameraPositionOrigin.x = Mathf.Clamp(transform.position.x, -panning.panLimit.x, panning.panLimit.x);
		//cameraPositionOrigin.z = Mathf.Clamp(transform.position.z, -panning.panLimit.y, panning.panLimit.y);


		// Camera Scrolling
		Scrolling();

		// Reset the position of the Main Camera
		if(Input.GetKeyDown(KeyCode.F1))
		{
			ResetCameraPosition();
		}
	}

	void Panning()
	{
		// if mouse pointer hit top edge of the screen, and then pan up
		if(Input.GetKey(KeyCode.W) || Input.mousePosition.y >= Screen.height - panning.panBorder)
		{
			transform.Translate(Vector3.forward * panning.panSpeed * Time.deltaTime, Space.World);
		}
		// if mouse pointer hit bottom edge of the screen, and then pan down
		if(Input.GetKey(KeyCode.S) || Input.mousePosition.y <= panning.panBorder)
		{
			transform.Translate(Vector3.back * panning.panSpeed * Time.deltaTime, Space.World);
		}
		// if mouse pointer hit right edge of the screen, and then pan right
		if(Input.GetKey(KeyCode.D) || Input.mousePosition.x >= Screen.width - panning.panBorder)
		{
			transform.Translate(Vector3.right * panning.panSpeed * Time.deltaTime, Space.World);
		}
		// if mouse pointer hit left edge of the screen, and then pan left
		if(Input.GetKey(KeyCode.A) || Input.mousePosition.x <= panning.panBorder)
		{
			transform.Translate(Vector3.left * panning.panSpeed * Time.deltaTime, Space.World);
		}

		// Clamp Camera (damn I'm doing it so bad!)
		Vector3 clampPos = cameraPositionOrigin;
		clampPos.x = Mathf.Clamp(transform.position.x, -panning.panLimit.x + 145f, panning.panLimit.x);
		clampPos.y = transform.position.y;
		clampPos.z = Mathf.Clamp(transform.position.z, -panning.panLimit.y, panning.panLimit.y - 100f);
		transform.position = clampPos;
	}

	void Scrolling()
	{
		Vector3 position = transform.position;

		float scroll = Input.GetAxis("Mouse ScrollWheel");
		position = transform.position;

		position.y -= scroll * (1000 * scrolling.scrollSpeed) * Time.deltaTime;
		position.y = Mathf.Clamp(position.y, scrolling.min, scrolling.max);

		transform.position = position;
	}

	void ResetCameraPosition()
	{
		transform.position = cameraPositionOrigin;
	}
}

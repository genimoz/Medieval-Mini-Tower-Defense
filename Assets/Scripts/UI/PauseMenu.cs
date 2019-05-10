/*
* Author : onogenio
* Copyright (c) 2017 Patriano Genio
* All Rights Reserved.
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
	public GameObject pauseUI;

	void Start ()
	{
		
	}
	
	void Update ()
	{
		if(Input.GetKeyDown(KeyCode.Escape))
		{
			Toggle();
		}
	}

	void Toggle()
	{
		pauseUI.SetActive(!pauseUI.activeSelf); // SetActive is false

		if(pauseUI.activeSelf)
		{
			Time.timeScale = 0f; // timeScale 0 means no movement, no script running, just paused
			GetComponent<AudioSource>().Pause();
		}
		else
		{
			Time.timeScale = 1f; // set it back to normal
			GetComponent<AudioSource>().Play();
		}
	}

	public void ContinueGame()
	{
		pauseUI.SetActive(false);
		Time.timeScale = 1f;
		GetComponent<AudioSource>().Play();
	}
}

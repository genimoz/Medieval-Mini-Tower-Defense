/*
* Author : onogenio
* Copyright (c) 2017 Patriano Genio
* All Rights Reserved.
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
	public SceneFader scenefader;
	public GameObject bulletPrefab;
	public Transform firePoint;
	public GameObject canvas;

	public Bullet bullet;

	public void Play()
	{
		// SceneManager.LoadScene("Gameplay");
		// FindObjectOfType<SceneFader>().FadeTo("Gameplay");

		//GameObject bulletObject = (GameObject)Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
		//bullet.speed = 25f;

		canvas.SetActive(false);
		scenefader.FadeTo("Gameplay_Vanilla");
	}

	public void Quit()
	{
		Application.Quit();
	}
}

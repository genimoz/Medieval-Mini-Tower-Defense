/*
* Author : onogenio
* Copyright (c) 2017 Patriano Genio
* All Rights Reserved.
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour 
{
	public static bool isGameEnded = false;
	public static bool isLevelCompleted = false;

	public GameObject gameOverUI;
	public GameObject levelCompleteUI;

	public static int totalRoundthisGame = 5; // number of rounds that have to be finished to complete a level

	void Start()
	{
		isGameEnded = false;
		isLevelCompleted = false;
	}

	void Update()
	{
		if(PlayerStats.health <= 0 || Input.GetKeyDown(KeyCode.E))
		{
			//PlayerStats.health = 0; // make sure the homebase health has not a negative value
			EndGame();
		}

		if(isLevelCompleted)
		{
			CompleteLevel();
		}
	}

	void EndGame()
	{
		isGameEnded = true;
		Debug.Log("Game Over!");

		gameOverUI.SetActive(true);
	}

	void CompleteLevel()
	{
		levelCompleteUI.SetActive(true);
	}
}

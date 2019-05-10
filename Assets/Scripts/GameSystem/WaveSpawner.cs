/*
* Author : onogenio
* Copyright (c) 2017 Patriano Genio
* All Rights Reserved.
*/

using UnityEngine;
using System.Collections;

public class WaveSpawner : MonoBehaviour 
{
	public static int enemiesAlive = 0;

	public Wave[] waves;
	public Transform spawnPoints;
	public float timeBetweenWaves = 5f; // enemy wave spawn delay

	private float countdown = 2f;
	private int waveIndex = 0; // how many waves spawned

	private int round; // represents a set of couples of waves

	void Update()
	{
		if(PlayerStats.health > 0) // if player is still alive
		{
			if(enemiesAlive > 0)
			{
				return;
			}

			if(countdown <= 0f)
			{
				StartCoroutine(SpawnWave());
				countdown = timeBetweenWaves;
				return;
			}

			countdown -= Time.deltaTime;
			countdown = Mathf.Clamp(countdown, 0f, Mathf.Infinity);
		}
		else // if the player base destroyed, then stop spawning enemy
		{
			StopCoroutine(SpawnWave());
		}

		// this block has a wave spawn control functionality
		if(waveIndex == waves.Length) // if wave array index is equal to total type of enemy that has been released
		{
			round++;
			//Debug.Log("Round " + round + " Completed.");
			waveIndex = 0; // reset
		}
		else if(round == GameManager.totalRoundthisGame && EnemyDetection.enemiesDetected == 0)
		{
			//Debug.Log("Congratulation, You win this Level!");
			GameManager.isLevelCompleted = true; // set this static variable in Game Manager to true
			this.enabled = false; // disable this script in order to stop spawning enemy waves
		}
		// end of block
	}

	IEnumerator SpawnWave()
	{
		
		PlayerStats.waveCount++; // counting waves spawned

		Wave wave = waves[waveIndex];
		enemiesAlive = wave.quantity; // total enemies are alive is equal to number of enemies spawned in a wave

		// after bunch of a type of enemy released, it will stop and then spawn another one.
		for(int i = 0; i < wave.quantity; i++) // check if enemies spawned are less than total quantity of enemies spawned in a wave
		{
			// Spawning!
			SpawnEnemy(wave.enemy);
			yield return new WaitForSeconds(1f / wave.rate);
		}

		waveIndex++;
		wave.quantity++;

//		UNUSED
//		// after a set of waves have defeated or ended, then go to the next round by resetting the waveNumber
//		if(waveIndex == waves.Length && round < GameManager.totalRoundthisGame)
//		{
//			round++; // add round counted
//			waveIndex = 0; // reset wave number
//		}
//
//		// set a condition to make the player win and stop spawning any waves
//		if(round > GameManager.totalRoundthisGame)
//		{
//			
//			Debug.Log("You Won this Level!");
//		}
	}

	void SpawnEnemy(GameObject enemy)
	{
		Instantiate(enemy, spawnPoints.position, spawnPoints.rotation);
	}
}

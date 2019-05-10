/*
* Author : onogenio
* Copyright (c) 2017 Patriano Genio
* All Rights Reserved.
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyMovement : MonoBehaviour 
{
	public Transform target; // refference to a waypoint
	private int wavepointIndex = 0;

	private Enemy enemy;

	void Start()
	{
		enemy = GetComponent<Enemy>();

		target = Waypoints.points[0];
	}

	void Update()
	{
		Vector3 direction = target.position - transform.position; // set enemy to pursue a waypoint
		transform.Translate(direction.normalized * enemy.speed * Time.deltaTime);

		// if enemies have reached a waypoint
		if(Vector3.Distance(transform.position, target.position) <= 0.5f)
		{
			GetNextWaypoint(); // go to another waypoint.
		}

		enemy.speed = enemy.startSpeed; // set enemy speed back to its initial speed
	}

	void GetNextWaypoint() // Enemy will pursue the next waypoint sequentially based on waypoint ordered in a hierarchy
	{
		// if enemies have reached the end of waypoint length which is the player's base
		if(wavepointIndex >= Waypoints.points.Length - 1)
		{
			// player's base take damage, destroying enemies etc.
			ReachPlayerBase();
			return;
		}

		wavepointIndex++;
		target = Waypoints.points[wavepointIndex];
	}

	void ReachPlayerBase()
	{
		PlayerStats.health--;
		Destroy(gameObject);
		WaveSpawner.enemiesAlive--;
	}
}

/*
* Author : onogenio
* Copyright (c) 2017 Patriano Genio
* All Rights Reserved.
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDetection : MonoBehaviour
{
	public static int enemiesDetected = 0;

	void Start ()
	{
		InvokeRepeating("DetectTarget", 0, 1f);
	}

	void Update()
	{
		if(enemiesDetected == null)
		{
			enemiesDetected = 0;
		}
	}

	void DetectTarget()
	{
		Debug.Log(enemiesDetected);
		GameObject[] targets = GameObject.FindGameObjectsWithTag("Enemy");
		enemiesDetected = targets.Length;
	}
}

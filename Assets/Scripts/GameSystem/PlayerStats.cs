/*
* Author : onogenio
* Copyright (c) 2017 Patriano Genio
* All Rights Reserved.
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
	public static int coin;
	public int startCoin = 10;

	public static int health;
	public int startHealth = 100;

	public static float startHealthToDisplay;

	public static int waveCount = 0;

	void Start()
	{
		startHealthToDisplay = startHealth;

		coin = startCoin;
		health = startHealth;

		waveCount = 0;
	}
}

/*
* Author : onogenio
* Copyright (c) 2017 Patriano Genio
* All Rights Reserved.
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TurretBlueprint
{
	// LOGIC ERROR! THIS VARIABLE AFFECTS ALL TURRETS WHEN UPGRADING ANY OF THEM
	public static int upgradedCount = 1; // a level 1 turret has an upgradeCount = 1

	// turret level 1
	public GameObject buildPrefab;
	public int buildCost;

	// turret level 2
	public GameObject upgradedPrefab;
	public int upgradeCost;

	// turret level 3, 4, 5 and so on

	// A function to calculate coin refund if the player sells a turret
	public int GetSellAmout()
	{
		//return buildCost / 2;

		// Coins refund by selling a turret is based on how many times a turret has been upgraded
		// it is counted as turret level, higher level means more refund coins
		int worthAmount = 0;

		worthAmount = (buildCost * upgradedCount) / 2;
		return worthAmount;
	}
}

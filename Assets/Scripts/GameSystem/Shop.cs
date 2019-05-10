/*
* Author : onogenio
* Copyright (c) 2017 Patriano Genio
* All Rights Reserved.
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour 
{
	public TurretBlueprint standardTurret;
	public TurretBlueprint missileLauncher;
	public TurretBlueprint laserBeamer;

	BuildingManager buildingManager;

	void Start()
	{
		buildingManager = BuildingManager.instance;
	}

	public void SelectStandardTurret()
	{
		Debug.Log("Standard Turret Selected");
		buildingManager.SelectTurretToBuild(standardTurret);
	}

	public void SelectMissileLauncher()
	{
		Debug.Log("Missile Launcher Selected");
		buildingManager.SelectTurretToBuild(missileLauncher);
	}

	public void SelectLaserBeamer()
	{
		Debug.Log("Laser Beamer Selected");
		buildingManager.SelectTurretToBuild(laserBeamer);
	}
}

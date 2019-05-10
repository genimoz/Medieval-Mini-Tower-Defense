/*
* Author : onogenio
* Copyright (c) 2017 Patriano Genio
* All Rights Reserved.
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Node : MonoBehaviour
{
	public Color hoverColor;
	public Color notEnoughCoinColor;
	public Vector3 positionOffset;

	[HideInInspector]
	public GameObject turret;
	[HideInInspector]
	public TurretBlueprint turretBlueprint;
	[HideInInspector]
	public bool isUpgraded = false;

	private Renderer nodeRenderer;
	private Color startColor;

	BuildingManager buildingManager;

	void Start()
	{
		nodeRenderer = GetComponent<Renderer>();
		startColor = nodeRenderer.material.color;

		nodeRenderer.enabled = false; // node won't appears at the start of the game

		buildingManager = BuildingManager.instance;
	}

	public Vector3 GetBuildPosition()
	{
		return transform.position + positionOffset;
	}

	void OnMouseDown()
	{
		if(EventSystem.current.IsPointerOverGameObject())
		{
			return;
		}

		if(turret != null)
		{
			buildingManager.SelectedNode(this);
			return;
		}

		if(!buildingManager.canBuild)
		{
			return;
		}

		BuildTurret(buildingManager.GetTurretToBuild()); // start building a turret
		buildingManager.DeselectTurretToBuild(); // deselect current turret to build

//		buildingManager.BuildTurretOn(this);
//
//		GameObject turretToBuild = BuildingManager.instance.GetTurretToBuild();
//		turret = (GameObject)Instantiate(turretToBuild, transform.position + positionOffset, transform.rotation);
	}

	// BUILD A TURRET
	void BuildTurret(TurretBlueprint blueprint)
	{
		if(PlayerStats.coin < blueprint.buildCost)
		{
			Debug.Log("You have not enough coin to build that.");
			return;
		}

		PlayerStats.coin -= blueprint.buildCost; // turret bought

		GameObject _turret = (GameObject)Instantiate(blueprint.buildPrefab, GetBuildPosition(), Quaternion.identity);
		turret = _turret;

		turretBlueprint = blueprint;

		GameObject effect = (GameObject)Instantiate(buildingManager.buildEffect, GetBuildPosition(), Quaternion.identity);
		Destroy(effect, 5f);
	}

	// UPGRADE A TURRET
	public void UpgradeTurret()
	{
		if(PlayerStats.coin < turretBlueprint.upgradeCost)
		{
			Debug.Log("You have not enough coin to upgrade that.");
			return;
		}
		PlayerStats.coin -= turretBlueprint.upgradeCost; // turret upgraded

		Destroy(turret); // remove the old turret

		// instantiate the new one
		GameObject _turret = (GameObject)Instantiate(turretBlueprint.upgradedPrefab, GetBuildPosition(), Quaternion.identity);
		turret = _turret;

		GameObject effect = (GameObject)Instantiate(buildingManager.buildEffect, GetBuildPosition(), Quaternion.identity);
		Destroy(effect, 5f);

		isUpgraded = true;
	}

	// SELL A TURRET
	public void SellTurret()
	{
		// PlayerStats.coin += turretBlueprint.buildCost * 0.5f; // sell price is a half of build price
		PlayerStats.coin += turretBlueprint.GetSellAmout();
		Destroy(turret);
		turretBlueprint = null;
	}

	void OnMouseEnter()
	{
		if(EventSystem.current.IsPointerOverGameObject())
		{
			return;
		}

		// check if the player can build any turret, tower etc. on a specific node
		if(!buildingManager.canBuild)
		{
			return;
		}

		// check if the player has enough coin to build any defense tower such as turret, launcher etc
		if(buildingManager.haveCoin)
		{
			nodeRenderer.enabled = true;
			nodeRenderer.material.color = hoverColor;
		}
		else
		{
			nodeRenderer.enabled = true;
			nodeRenderer.material.color = notEnoughCoinColor;
		}
	}

	void OnMouseExit()
	{
		nodeRenderer.enabled = false;
		nodeRenderer.material.color = startColor;
	}
}

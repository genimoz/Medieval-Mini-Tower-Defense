/*
* Author : onogenio
* Copyright (c) 2017 Patriano Genio
* All Rights Reserved.
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingManager : MonoBehaviour 
{
	// singleton pattern
	public static BuildingManager instance;

	void Awake()
	{
		if(instance != null)
		{
			Debug.LogError("More than one Building Manager in scene!");
			return;
		}

		instance = this;
	}


	public GameObject standardTurretPrefab;
	public GameObject missileLauncherPrefab;
	public GameObject lasserBeamerPrefab;

	public GameObject buildEffect;

	private TurretBlueprint turretToBuild;
	private Node selectedNode;

	public NodeUI nodeUI;

//	void Start()
//	{
//		turretToBuild = standardTurretPrefab;
//	}

//	public GameObject GetTurretToBuild()
//	{
//		
//		return turretToBuild;
//	}

	// these are simplified functions for return a bool value
	public bool canBuild{get {return turretToBuild != null;}}
	public bool haveCoin{get {return PlayerStats.coin >= turretToBuild.buildCost;}}

	public void SelectedNode(Node node)
	{
		if(selectedNode == node)
		{
			DeselectNode();
			return;
		}

		selectedNode = node;
		turretToBuild = null;

		nodeUI.SetTarget(node);
	}

	public void DeselectNode()
	{
		selectedNode = null;
		nodeUI.Hide();
	}

	public void SelectTurretToBuild(TurretBlueprint turret)
	{
		turretToBuild = turret;
		nodeUI.Hide();
	}

	public void DeselectTurretToBuild()
	{
		turretToBuild = null;

		// this block is to make sure that SelectedArrowImage is inactive when succesfully built a turret
		GameObject arrowimage;
		arrowimage = GameObject.Find("SelectedMark");
		arrowimage.SetActive(false);
	}

	public TurretBlueprint GetTurretToBuild()
	{
		return turretToBuild;
	}
}
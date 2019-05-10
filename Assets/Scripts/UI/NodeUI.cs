using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeUI : MonoBehaviour 
{
	public GameObject UserInterface;

	private Node target;

	public void SetTarget(Node _target)
	{
		target = _target;

		transform.position = target.GetBuildPosition();

		UserInterface.SetActive(true);
	}

	public void Hide()
	{
		UserInterface.SetActive(false);
	}

	public void Upgrade()
	{
		target.UpgradeTurret();
		BuildingManager.instance.DeselectNode();

		//TurretBlueprint.upgradedCount++;
	}

	public void Sell()
	{
		target.SellTurret();
		BuildingManager.instance.DeselectNode();
	}
}

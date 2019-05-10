/*
* Author : onogenio
* Copyright (c) 2017 Patriano Genio
* All Rights Reserved.
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopUI : MonoBehaviour
{
	public Text priceText;
	public Shop shop;

	public string turretName = "";

	BuildingManager buildManager;


	public GameObject selectedMarkImage;
	Animator selectAnimation;

	void Start()
	{
		selectAnimation = selectedMarkImage.GetComponent<Animator>();
	}

	void Update ()
	{
		if(turretName == "Standard Turret")
		{
			priceText.text = shop.standardTurret.buildCost.ToString();
		}
		if(turretName == "Missile Launcher")
		{
			priceText.text = shop.missileLauncher.buildCost.ToString();
		}
		if(turretName == "Laser Beamer")
		{
			priceText.text = shop.laserBeamer.buildCost.ToString();
		}
	}
}

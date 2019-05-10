/*
* Author : onogenio
* Copyright (c) 2017 Patriano Genio
* All Rights Reserved.
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootUI : MonoBehaviour
{
	public GameObject target;
	public LineRenderer laserRenderer;
	public ParticleSystem impactEffect;
	public Light impactLight;
	public Transform firePoint;

	//private float dmg = 30f;
	private bool isShot = false;

	void Update ()
	{
		if(target == null)
		{
			laserRenderer.enabled = false;
			impactEffect.Stop(); // turn on laser impact particle system
			impactLight.enabled = false;
		}

		if(isShot == true)
		{
			laserRenderer.enabled = true;
			impactEffect.Play(); // turn on laser impact particle system
			impactLight.enabled = true;

			Destroy(target, 3f);
		}
	}

	public void Shoot()
	{

		laserRenderer.SetPosition(0, firePoint.position); // start position of the laser
		laserRenderer.SetPosition(1, target.transform.position); // end position of the laser

		Vector3 impactDirection = firePoint.position - target.transform.position;

		impactEffect.transform.position = target.transform.position + impactDirection.normalized * 0.5f; // set impact effect stick to enemy target

		impactEffect.transform.rotation = Quaternion.LookRotation(impactDirection);

		isShot = true;
	}
}

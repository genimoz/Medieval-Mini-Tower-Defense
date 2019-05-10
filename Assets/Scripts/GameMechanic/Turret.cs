/*
* Author : onogenio
* Copyright (c) 2017 Patriano Genio
* All Rights Reserved.
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
	private Transform target;
	private Enemy targetEnemy;

	[Header("Turret Movement")]
	public Transform partToRotate;
	public float range = 15f;
	public float turnSpeed = 10f;

	[Header("Turret Shooting")]
	public float fireRate = 1f;
	private float fireCountdown = 0f;

	[Header("Using Bullet (Default)")]
	public GameObject bulletPrefab;
	public Transform firePoint;

	[Header("Using Laser")]
	public bool useLaser = false;
	public float snareAmount = 0.5f;

	public int damageOverTime = 30;

	public LineRenderer laserRenderer;
	public ParticleSystem impactEffect;
	public Light impactLight;

	void Start()
	{
		InvokeRepeating("UpdateTarget", 0f, 0.5f);
	}

	void UpdateTarget()
	{
		GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
		float shortestDistance = Mathf.Infinity;
		GameObject nearestEnemy = null;

		foreach(GameObject enemy in enemies)
		{
			float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);

			if(distanceToEnemy < shortestDistance)
			{
				shortestDistance = distanceToEnemy;
				nearestEnemy = enemy;
			}
		}

		// check if there's an enemy appears and it is in range
		if(nearestEnemy != null && shortestDistance <= range)
		{
			target = nearestEnemy.transform; // enemy detected
			targetEnemy = nearestEnemy.GetComponent<Enemy>(); // get script component of the Enemy
		}
		else
		{
			target = null; // enemy undetected
		}
	}

	void Update()
	{
		// check if there's no target appears
		if(target == null)
		{
			if(useLaser)
			{
				if(laserRenderer.enabled)
				{
					laserRenderer.enabled = false; // disable laser LineRenderer
					impactEffect.Stop(); // turn off laser impact particle system
					impactLight.enabled = false;
				}
			}

			return;
		}

		LookOnTarget(); // call the method

		// type of projectile that turret use
		if(useLaser)
		{
			Laser();
		}
		else // if a defense tower or turret doesn't use laser to attack
		{
			// determine how fast a turret can shoot
			if(fireCountdown <= 0f)
			{
				Shoot();
				fireCountdown = 1f / fireRate;
			}

			fireCountdown -= Time.deltaTime;
		}
	}

	void LookOnTarget()
	{
		Vector3 direction = target.position - transform.position; // assign direcion from turret to enemy
		Quaternion lookRotation = Quaternion.LookRotation(direction); // assign the rotation of the turret to enemy
		Vector3 rotation = Quaternion.Lerp(partToRotate.rotation, lookRotation, turnSpeed * Time.deltaTime).eulerAngles;
		partToRotate.rotation = Quaternion.Euler(0f, rotation.y, 0f); // rotate y axis of partToRotate transform
	}

	void Shoot()
	{
		GameObject bulletObject = (GameObject)Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
		Bullet bullet = bulletObject.GetComponent<Bullet>();

		GetComponent<AudioSource>().Play(); // Play sound effect when a turret is shooting

		if(bullet != null)
		{
			bullet.Seek(target);
		}
	}

	void Laser()
	{
		//GetComponent<AudioSource>().Play();

		targetEnemy.TakeDamage(damageOverTime * Time.deltaTime);
		targetEnemy.Snare(snareAmount);

		if(!laserRenderer.enabled)
		{
			laserRenderer.enabled = true;
			impactEffect.Play(); // turn on laser impact particle system
			impactLight.enabled = true;
		}

		laserRenderer.SetPosition(0, firePoint.position); // start position of the laser
		laserRenderer.SetPosition(1, target.position); // end position of the laser

		Vector3 impactDirection = firePoint.position - target.position;

		// set impact effect stick to enemy target
		impactEffect.transform.position = target.position + impactDirection.normalized * 1f; // 1f represent range the effect to he pivot of the target
		impactEffect.transform.rotation = Quaternion.LookRotation(impactDirection);
	}

	 
	void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(transform.position, range);
	}
}

/*
* Author : onogenio
* Copyright (c) 2017 Patriano Genio
* All Rights Reserved.
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour 
{
	private Transform target;

	public int damage = 50; // damage value stored to a public variable to handle any different type of bullet. Can be assigned in the Inspector
	public float speed = 50f;
	public float explodeRadius = 0f;
	public GameObject explodeEffect;

	public void Seek(Transform _target) // Transform _target to store multiple targets
	{
		target = _target;
	}
		
	void Update()
	{
		if(target == null)
		{
			Destroy(gameObject);
			return;
		}

		Vector3 direction = target.position - transform.position;
		float distanceThisFrame = speed * Time.deltaTime;

		if(direction.magnitude <= distanceThisFrame)
		{
			HitTarget();
			return;
		}

		transform.Translate(direction.normalized * distanceThisFrame, Space.World);
		transform.LookAt(target); // set the bullet rotation to an enemy target
	}

	void HitTarget()
	{
		GameObject explosionInstance = (GameObject)Instantiate(explodeEffect, transform.position, transform.rotation);
		Destroy(explosionInstance, 1f);

		if(explodeRadius > 0f)
		{
			Explode();
		}
		else
		{
			Damaging(target);
		}

		Destroy(gameObject);
	}

	void Explode()
	{
		Collider[] colliders = Physics.OverlapSphere(transform.position, explodeRadius);

		foreach(Collider collider in colliders)
		{
			if(collider.tag == "Enemy")
			{
				Damaging(collider.transform);
			}
		}
	}

	// Turrets deal some amount of damage to an enemy
	void Damaging(Transform enemyObject)
	{
		Enemy e = enemyObject.GetComponent<Enemy>(); // refference to enemy script

		if(e != null)
		{
			e.TakeDamage(damage); // assign value of damage
		}
	}

	// Draws Gizmo that shows area of bullet explosion
	void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(transform.position, explodeRadius);
	}
}

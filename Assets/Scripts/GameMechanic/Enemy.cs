/*
* Author : onogenio
* Copyright (c) 2017 Patriano Genio
* All Rights Reserved.
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour 
{
	public float startSpeed = 10f; // initialize starting speed
	[HideInInspector]
	public float speed; // current enemy movement speed

	public float maxHealth = 100; // starting enemy health amount
	private float currentHealth;

	public int coinGain = 20; // coin value gained after killed an enemy. different enemy gives different coin. Can be assigned in the Inspector
	public GameObject deathEffect;

	[Header("HUD")]
	public Image healthBar;

	void Start()
	{
		speed = startSpeed;
		currentHealth = maxHealth;
	}

	// enemies will take damage from a bullet
	public void TakeDamage(float amount)
	{
		currentHealth -= amount;

		healthBar.fillAmount = currentHealth / maxHealth; // fillAmount has a value between 0-1. so it divided by 100 according to enemyhealth.

		if(currentHealth <= 0)
		{
			Dead();
		}
	}

	public void Snare(float amount)
	{
		speed = startSpeed * (1f - amount);
	}

	// when an enemy dead, player get some amount of coin which is used for build another defense building
	void Dead()
	{
		GameObject effect = (GameObject)Instantiate(deathEffect, transform.position, Quaternion.identity);
		Destroy(effect, 3f);

		PlayerStats.coin += coinGain;
		Destroy(gameObject);

		WaveSpawner.enemiesAlive--;
	}
}

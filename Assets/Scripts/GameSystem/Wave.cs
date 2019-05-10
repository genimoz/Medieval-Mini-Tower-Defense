/*
* Author : onogenio
* Copyright (c) 2017 Patriano Genio
* All Rights Reserved.
*/

using UnityEngine;

[System.Serializable]
public class Wave
{
	public GameObject enemy; // refference to enemy game object
	public int quantity; // number of enemy spawned in a wave
	public float rate;
}

/*
* Author : onogenio
* Copyright (c) 2017 Patriano Genio
* All Rights Reserved.
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoints : MonoBehaviour 
{
	public static Transform[] points;

	void Awake()
	{
		points = new Transform[transform.childCount];

		for(int i = 0; i < points.Length; i++)
		{
			points[i] =  transform.GetChild(i);
		}
	}
}

/*
* Author : onogenio
* Copyright (c) 2017 Patriano Genio
* All Rights Reserved.
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneFader : MonoBehaviour
{
	public Image imageSource;
	public float fadeDuration = 2f;
	public AnimationCurve curve;

	void Start ()
	{
		StartCoroutine(FadeIn());
	}

	public void FadeTo(string scene)
	{
		StartCoroutine(FadeOut(scene));
	}

	IEnumerator FadeIn() // I use coroutine because I don't want to the "time" variable to decrease in 1 frame. So it will decrease over time
	{
		float time = 1f;

		while(time > 0f)
		{
			time -= Time.deltaTime * fadeDuration; // substract value of "time" by (delta time multiple by fadeDuration);
			float alpha = curve.Evaluate(time);
			imageSource.color = new Color(0f, 0f, 0f, alpha); // decrease the alpha value over time
			yield return 0; // wait until the next frame
		}
	}

	IEnumerator FadeOut(string scene)
	{
		float time = 0f;

		while(time < 1f)
		{
			time += Time.deltaTime * fadeDuration;
			float alpha = curve.Evaluate(time);
			imageSource.color = new Color(0f, 0f, 0f, alpha);
			yield return 0;
		}

		SceneManager.LoadScene(scene);
	}
}
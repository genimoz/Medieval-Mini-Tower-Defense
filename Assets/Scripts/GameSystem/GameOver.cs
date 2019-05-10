/*
* Author : onogenio
* Copyright (c) 2017 Patriano Genio
* All Rights Reserved.
*/

using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
	public Text waveRoundText;

	void OnEnable()
	{
		waveRoundText.text = PlayerStats.waveCount.ToString();
	}

	public void Retry()
	{
		// SceneManager.LoadScene("Gameplay");
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}

	public void Exit()
	{
		Debug.Log("You Exit the Game!");
		// Application.Quit();
	}

	IEnumerator AnimateText()
	{
		waveRoundText.text = "0";
		int waveRound = 0;

		yield return new WaitForSeconds(0.75f);

		while(waveRound < PlayerStats.waveCount)
		{
			waveRound++;
			waveRoundText.text = waveRound.ToString();
			yield return new WaitForSeconds(0.5f);
		}
	}
}

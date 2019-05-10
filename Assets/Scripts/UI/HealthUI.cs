using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour 
{
	public Image healthFill;

	void Update()
	{
		healthFill.fillAmount = PlayerStats.health / PlayerStats.startHealthToDisplay;
	}
}

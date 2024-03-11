using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class YourScriptName : MonoBehaviour
{
	public TextMeshProUGUI textboxHP;
	public TextMeshProUGUI textboxEnergy;

	void Start()
	{

	}

	void Update()
	{
		UInt16 hp = GameManager.Instance.getBaseHP();
		int energy = GameManager.Instance.currency;
		textboxHP.text = "Base HP : " + hp;
		textboxEnergy.text = "Energy : " + energy;
	}

	public void setSelected() {
		
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour {
	public static BuildManager main;

	[Header("References")]
	[SerializeField] private GameObject[] towersPrefabs;

	private int selectedTower;

	private void Awake() {
		main = this;
	}

	public GameObject getSelectedTower() {
		return towersPrefabs[selectedTower];
	}
}

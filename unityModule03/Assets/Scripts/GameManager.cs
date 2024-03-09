using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	private static GameManager _instance;
	public static GameManager Instance {
		get {
			if (_instance == null)
				Debug.LogError("Game Manager is null");
			return _instance;
		}
	}

	private bool	_isGameOver = false;
	private UInt16	_BaseHP = 5;

	private void Awake() {
		_instance = this;
		DontDestroyOnLoad(this.gameObject); // When a new scene is load, the object can't be destroyed
	}
   
	public void GameOver() {
		_isGameOver = true;
		Debug.Log("Game Over");
		var clones = GameObject.FindGameObjectsWithTag ("Enemy");
		foreach (var clone in clones)
			Destroy(clone);
		GameObject.Find("Spawner").GetComponent<EnemySpawner>().enabled = false;
	}

	public bool isGameOver() {
		return _isGameOver;
	}

	public void DecreaseBaseHP(int hitPoints) {
		_BaseHP -= (ushort)hitPoints;
		Debug.Log("Base HP remaining : " + _BaseHP);
		if(_BaseHP <= 0) {
			GameOver();
		}
	} 
}

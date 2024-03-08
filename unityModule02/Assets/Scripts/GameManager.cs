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
	}
   
	public void GameOver(bool flag) {
		_isGameOver = flag;
	}

	public bool isGameOver() {
		return _isGameOver;
	}
}

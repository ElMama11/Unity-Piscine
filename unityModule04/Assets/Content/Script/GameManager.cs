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

	private UInt16	_PlayerHP = 3;
	public GameObject playerObj;

	private void Awake() {
		_instance = this;
		DontDestroyOnLoad(this.gameObject); // When a new scene is load, the object can't be destroyed
	}

	private void Start() {
	}

	public void takeDamage(UInt16 dmg) {
		_PlayerHP -= dmg;
		Debug.Log("Mob HP: " + _PlayerHP);
		if (_PlayerHP <= 0) {
			Destroy(gameObject);
		}
	}
}
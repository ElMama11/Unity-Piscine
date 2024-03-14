using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.SceneManagement;

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

	public GameObject playerObj;
	private PlayerController _playerScript;
	public int CollectiblePoints;

	private void Awake() {
		_instance = this;
		DontDestroyOnLoad(this.gameObject);
	}

	private void Start() {
		 _playerScript = playerObj.GetComponent<PlayerController>();
	}

	public void endOfTheStage() {
		Debug.Log("The Stage is finished, congratulations !");
		// if j'ai les leaf
		if (SceneManager.GetActiveScene().buildIndex + 1 < SceneManager.sceneCountInBuildSettings)
			SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
		else
			SceneManager.LoadScene(0);
	}
}
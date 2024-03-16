using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
	private static UIManager _instance;
	public static UIManager Instance {
		get {
			if (_instance == null)
				Debug.LogError("UI Manager is null");
			return _instance;
		}
	}
	public TextMeshProUGUI TextHP;
	public TextMeshProUGUI TextScore;
	public PlayerController playerScript;
    private void Awake() {
        _instance = this;
		DontDestroyOnLoad(this.gameObject);
    }

    void Update() {
        int hp = GameManager.Instance.hp;
		int score = GameManager.Instance.collectiblePoints;
		TextHP.text = "HP : " + hp;
		TextScore.text = "Score : " + score;
    }
}

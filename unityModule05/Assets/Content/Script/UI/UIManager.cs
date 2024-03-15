using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BarUI : MonoBehaviour
{
	public TextMeshProUGUI TextHP;
	public TextMeshProUGUI TextScore;
	public PlayerController playerScript;
    void Start() {
        
    }

    void Update() {
        UInt16 hp = GameManager.Instance.hp;
		int score = GameManager.Instance.collectiblePoints;
		TextHP.text = "HP : " + hp;
		TextScore.text = "Score : " + score;
    }
}

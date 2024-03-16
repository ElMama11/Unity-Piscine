using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour
{
	 public int collectibleID;

    void Start() {
    }

    void Update() {
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.CompareTag("Player")) {
			PlayerPrefs.SetInt("CollectibleKey_" + collectibleID, 0);
			PlayerPrefs.Save();
			gameObject.SetActive(false);
            GameManager.Instance.increaseScore();
        }
    }
}

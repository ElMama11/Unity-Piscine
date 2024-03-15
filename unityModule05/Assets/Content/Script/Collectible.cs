using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    void Start() {
    }

    void Update() {
        if (GameManager.Instance.isDead == true) {
			gameObject.SetActive(true);
		}
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.CompareTag("Player")) {
			gameObject.SetActive(false);
            GameManager.Instance.increaseScore();
        }
    }
}

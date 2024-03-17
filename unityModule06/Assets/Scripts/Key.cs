using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
	private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player")) {
			GameManager.Instance.keycount += 1;
			Destroy(gameObject);
			Debug.Log("You found a key !");
		}
    }
}

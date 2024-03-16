using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class CollectiblesParent : MonoBehaviour
{

    void Start() {
		for (int i = 0; i < 7; i++) //Assign ID of all collectibles
			transform.GetChild(i).gameObject.GetComponent<Collectible>().collectibleID = i;
        for (int i = 0; i < 7; i++) {
			if(PlayerPrefs.GetInt("CollectibleKey_" + i, 1) == 0) // Check collectibles state in user profile
				transform.GetChild(i).gameObject.SetActive(false);
		}
    }

    void Update() {
         if (GameManager.Instance.isDead == true)
			StartCoroutine(LeafRespawn());
	}

	private IEnumerator LeafRespawn() {
		yield return new WaitForSeconds(2f);
		for (int i = 0; i < transform.childCount; i++)
       		transform.GetChild(i).gameObject.SetActive(true);
	}
}

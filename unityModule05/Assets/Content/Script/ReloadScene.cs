using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NewBehaviourScript : MonoBehaviour
{
	
    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.R)) {
			SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
			for (int i = 0; i < 7; i++)
				PlayerPrefs.SetInt("CollectibleKey_" + i, 1);
			PlayerPrefs.SetInt("PlayerScore", 0);
			PlayerPrefs.SetInt("PlayerHP", 3);
		}
    }
}
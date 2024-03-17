using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Endpoint : MonoBehaviour
{
	private AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	void OnTriggerEnter(Collider other) {
		audioSource.Play();
		Debug.Log("YOU WON");
		SceneManager.LoadScene(0);
	}

}

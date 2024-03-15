using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Liana : MonoBehaviour
{
	public Animator animator;
	public PlayerController playerScript;
	private AudioSource audioSource;
    void Start() {
        animator = gameObject.GetComponent<Animator>();
		audioSource = GetComponent<AudioSource>();
    }

    void Update() {
        
    }

	void OnCollisionEnter2D(Collision2D other) {
		animator.SetBool("IsAttacking", true);
		if (other.gameObject.name == "Player") {
			GameManager.Instance.decreaseHP(1);
			audioSource.Play();
		}
	}

	void OnCollisionExit2D(Collision2D other) {
		animator.SetBool("IsAttacking", false);
	}
}

using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class PlayerBehaviour : MonoBehaviour
{
	private Rigidbody _activeRb = null;
	private GameObject _activeGameObj;
	public	GameObject thomas;
	public	GameObject john;
	public	GameObject claire;
	public Camera mainCamera;
	private float _speedThomas = 4f;
    private float _speedJohn = 6f;
    private float _speedClaire = 3f;
	private float _jumpSpeedThomas = 4f;
    private float _jumpSpeedJohn = 5f;
    private float _jumpSpeedClaire = 3f;
	private Vector3 _playerMovementInput = Vector3.zero;
	public bool isGrounded;
	public bool isThomasFinish;
	public bool isJohnFinish;
	public bool isClaireFinish;
	public bool colidePositive = false;
	public bool colideNegative = false;

	void Start() {
		thomas = GameObject.Find("PlayerThomas");
		john = GameObject.Find("PlayerJohn");
		claire = GameObject.Find("PlayerClaire");
		mainCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
		isGrounded = true;
		isThomasFinish = false;
		isJohnFinish = false;
		isClaireFinish = false;
	}

	void Update() {
		handlePlayerSwitch();
		handlePlayerMovement();
		endOfTheStage();
	}

	void handlePlayerMovement() {
		if (_activeGameObj != null && gameObject.name ==_activeGameObj.name) {
			float speed = (_activeRb == thomas.GetComponent<Rigidbody>()) ? _speedThomas : (_activeRb == john.GetComponent<Rigidbody>()) ? _speedJohn : _speedClaire;
			float jumpSpeed = (_activeRb == thomas.GetComponent<Rigidbody>()) ? _jumpSpeedThomas : (_activeRb == john.GetComponent<Rigidbody>()) ? _jumpSpeedJohn : _jumpSpeedClaire;
			float input = Input.GetAxis("Horizontal");
			if ((colidePositive && input > 0) || (colideNegative && input < 0)) {
				handleCamera();
				return ;
			}
			_playerMovementInput = new Vector3(0, 0, input);
			Vector3 playerPosition =  _activeRb.transform.TransformDirection(_playerMovementInput) * speed;
			_activeRb.velocity = new Vector3(playerPosition.x, _activeRb.velocity.y, playerPosition.z);
			if (Input.GetButton("Jump") && isGrounded)
				_activeRb.velocity = new Vector3(0,jumpSpeed,0);
			handleCamera();
		}
	}

	void handlePlayerSwitch() {
		if (Input.GetKey(KeyCode.Alpha1)) {
			_activeRb = thomas.GetComponent<Rigidbody>();
			_activeGameObj = thomas;
		}
		else if (Input.GetKey(KeyCode.Alpha2)) {
			_activeRb = john.GetComponent<Rigidbody>();
			_activeGameObj = john;
		}
		else if (Input.GetKey(KeyCode.Alpha3)) {
			_activeRb = claire.GetComponent<Rigidbody>();
			_activeGameObj = claire;
		}
	}

	void handleCamera() {
		mainCamera.transform.position = new Vector3(mainCamera.transform.position.x, _activeRb.transform.position.y + 1, _activeRb.transform.position.z);
	}

	void endOfTheStage() {
		if (thomas.GetComponent<PlayerBehaviour>().isThomasFinish && claire.GetComponent<PlayerBehaviour>().isClaireFinish && john.GetComponent<PlayerBehaviour>().isJohnFinish) {
			Debug.Log("The Stage is finished, congratulations !");
			if (SceneManager.GetActiveScene().buildIndex + 1 < SceneManager.sceneCountInBuildSettings)
				SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
			else
				SceneManager.LoadScene(0);
		}
	}

	void OnCollisionStay(Collision collision) {
    	ContactPoint[] contacts = collision.contacts;
		if (_activeGameObj != null && gameObject.name ==_activeGameObj.name) {
			foreach (ContactPoint contact in contacts) {
				Vector3 normal = contact.normal.normalized; // Get the normal vector of the collision contact point
				if (normal == Vector3.up)
					isGrounded = true;
				if (normal == Vector3.back && !isGrounded)
					colidePositive = true;
				else
					colidePositive = false;
				if (normal == Vector3.forward && !isGrounded) 
					colideNegative = true;
				else
					colideNegative = false;

			}
		}
	} 

	void OnCollisionExit() {
		if (_activeGameObj != null && gameObject.name ==_activeGameObj.name) {
			isGrounded = false;
		}
    }

	private void OnTriggerEnter(Collider other) {
        if (_activeGameObj == thomas && other.gameObject.tag == "Finish_t") {
			isThomasFinish = true;
		}
		else if (_activeGameObj == john && other.gameObject.tag == "Finish_j") {
			isJohnFinish = true;
		}
		else if (_activeGameObj == claire && other.gameObject.tag == "Finish_c") {
			isClaireFinish = true;
		}
    }

	void OnTriggerExit(Collider other) {
        if (_activeGameObj == thomas && other.gameObject.tag == "Finish_t") {
			isThomasFinish = false;
		}
		else if (_activeGameObj == john && other.gameObject.tag == "Finish_j") {
			isJohnFinish = false;
		}
		else if (_activeGameObj == claire && other.gameObject.tag == "Finish_c") {
			isClaireFinish = false;
		}
    }
}

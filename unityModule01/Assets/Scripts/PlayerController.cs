using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
	private Rigidbody _activeRb = null;
	public	GameObject thomas;
	public	GameObject john;
	public	GameObject claire;
	public Camera mainCamera;
	[SerializeField] private float _speed = 1.0f;
	private Vector3 _playerMovementInput = Vector3.zero;

	void Start() {
		thomas = GameObject.Find("PlayerThomas");
		john = GameObject.Find("PlayerJohn");
		claire = GameObject.Find("PlayerClaire");
		mainCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
	}

	void Update() {
		handlePlayersSwitch();
		if (_activeRb != null) {
			_playerMovementInput = new Vector3(0, 0, Input.GetAxis("Horizontal"));
			Vector3 playerPosition =  _activeRb.transform.TransformDirection(_playerMovementInput) * _speed;
			_activeRb.velocity = new Vector3(playerPosition.x, _activeRb.velocity.y, playerPosition.z);
			if (Input.GetButton("Jump") && _activeRb.velocity.y == 0)
				_activeRb.AddForce(Vector3.up * 1.5f, ForceMode.Impulse);
			mainCamera.transform.position = new Vector3(mainCamera.transform.position.x, _activeRb.transform.position.y + 5, _activeRb.transform.position.z);
		}
	}

	void handlePlayersSwitch() {
		if (Input.GetKey(KeyCode.Alpha1)) 
			_activeRb = thomas.GetComponent<Rigidbody>();
		else if (Input.GetKey(KeyCode.Alpha2)) 
			_activeRb = john.GetComponent<Rigidbody>();
		else if (Input.GetKey(KeyCode.Alpha3))
			_activeRb = claire.GetComponent<Rigidbody>();
	}
}

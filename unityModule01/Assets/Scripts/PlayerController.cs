using UnityEngine;
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

	void Start() {
		thomas = GameObject.Find("PlayerThomas");
		john = GameObject.Find("PlayerJohn");
		claire = GameObject.Find("PlayerClaire");
		mainCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
		isGrounded = true;
		isThomasFinish = false;
		isJohnFinish = false;
		isClaireFinish = false;
		SaveCharacterStates();
		
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
			_playerMovementInput = new Vector3(0, 0, Input.GetAxis("Horizontal"));
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

	private void SaveCharacterStates() {
        // Save character positions or any other relevant state
        PlayerPrefs.SetFloat("ThomasX",thomas.transform.position.x);
        PlayerPrefs.SetFloat("ThomasY", thomas.transform.position.y);
        PlayerPrefs.SetFloat("ThomasZ", thomas.transform.position.z);

        PlayerPrefs.SetFloat("JohnX", john.transform.position.x);
        PlayerPrefs.SetFloat("JohnY", john.transform.position.y);
        PlayerPrefs.SetFloat("JohnZ", john.transform.position.z);

        PlayerPrefs.SetFloat("ClaireX", claire.transform.position.x);
        PlayerPrefs.SetFloat("ClaireY", claire.transform.position.y);
        PlayerPrefs.SetFloat("ClaireZ", claire.transform.position.z);
    }

	private void TeleportCharacters() {
		Debug.Log("TELEPORT");
		float thomasX = PlayerPrefs.GetFloat("ThomasX");
		float thomasY = PlayerPrefs.GetFloat("ThomasY");
		float thomasZ = PlayerPrefs.GetFloat("ThomasZ");

		float johnX = PlayerPrefs.GetFloat("JohnX");
		float johnY = PlayerPrefs.GetFloat("JohnY");
		float johnZ = PlayerPrefs.GetFloat("JohnZ");

		float claireX = PlayerPrefs.GetFloat("ClaireX");
		float claireY = PlayerPrefs.GetFloat("ClaireY");
		float claireZ = PlayerPrefs.GetFloat("ClaireZ");

		Debug.Log("Thomas position: (" + thomasX + ", " + thomasY + ", " + thomasZ + ")");
		Debug.Log("John position: (" + johnX + ", " + johnY + ", " + johnZ + ")");
		Debug.Log("Claire position: (" + claireX + ", " + claireY + ", " + claireZ + ")");
        thomas.transform.position = new Vector3(PlayerPrefs.GetFloat("ThomasX"), PlayerPrefs.GetFloat("ThomasY"), PlayerPrefs.GetFloat("ThomasZ"));
        john.transform.position = new Vector3(PlayerPrefs.GetFloat("JohnX"), PlayerPrefs.GetFloat("JohnY"), PlayerPrefs.GetFloat("JohnZ"));
        claire.transform.position = new Vector3(PlayerPrefs.GetFloat("ClaireX"), PlayerPrefs.GetFloat("ClaireY"), PlayerPrefs.GetFloat("ClaireZ"));
    }
}

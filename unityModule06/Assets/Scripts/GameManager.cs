
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
	private static GameManager _instance;

	public static GameManager Instance {
		get {
			if (_instance == null)
				Debug.LogError("Game Manager is null");
			return _instance;
		}
	}

	public int keycount = 0;
	public bool alert = false;
	public Vector3 posAlert;
	GameObject playerobj;
	public Vector3 playerPos;
	private void Awake() {
		_instance = this;
		DontDestroyOnLoad(this.gameObject);
	}

	private void Start() {
		playerobj = GameObject.FindWithTag("Player");
		
	}

	private void Update() {
		if (playerobj != null) {
			PlayerMovement playerMovement = playerobj.GetComponent<PlayerMovement>();
			if (playerMovement != null)
				playerPos = playerMovement.transform.position;
		}	
	}	

	public Vector3 GetPlayerPos() {
		return playerPos;
	}

	public bool getAlert() {
		return alert;
	}

	public Vector3 getposAlert() {
		return posAlert;
	}


}
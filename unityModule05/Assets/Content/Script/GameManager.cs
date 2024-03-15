
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System;

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

	public GameObject playerObj;
	private PlayerController _playerScript;
	public TextMeshProUGUI textboxLeafSufficient;
	public int collectiblePoints = 0;
	public bool isDead = false;
	public UInt16 hp = 3;

	private void Awake() {
		_instance = this;
		DontDestroyOnLoad(this.gameObject);
	}

	private void Start() {
		 _playerScript = playerObj.GetComponent<PlayerController>();
	}

	public void increaseScore() {
		collectiblePoints += 5;
	}

	public void resetScore() {
		collectiblePoints = 0;
	}

	public void endOfTheStage() {
		Debug.Log("The Stage is finished, congratulations !");
		if (collectiblePoints >= 25) {
			collectiblePoints = 0;
			if (SceneManager.GetActiveScene().buildIndex + 1 < SceneManager.sceneCountInBuildSettings)
				SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
			else
				SceneManager.LoadScene(0);
		}
		else
			StartCoroutine(displayLeafSufficientText());
	}

	public void decreaseHP(UInt16 dmg) {
		hp -= dmg;
		_playerScript.HandleDeathAndHit();
		if (hp <= 0)
			isDead = true;
	}

	public void resetHP() {
		isDead = false;
		hp = 3;
	}

	private IEnumerator displayLeafSufficientText() {
		textboxLeafSufficient.gameObject.SetActive(true);
		textboxLeafSufficient.text = "You don't have enough points. Your score : " + collectiblePoints;
		yield return new WaitForSeconds(2f);
		textboxLeafSufficient.gameObject.SetActive(false);
		yield return null;
	}
}
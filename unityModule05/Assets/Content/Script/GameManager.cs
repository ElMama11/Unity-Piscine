
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
	public int collectiblePoints;
	public int lastStageUnlock;
	public bool isDead = false;
	public int hp;

	private void Awake() {
		_instance = this;
		DontDestroyOnLoad(this.gameObject);
	}

	private void Start() {
		_playerScript = playerObj.GetComponent<PlayerController>();
		hp = PlayerPrefs.GetInt("PlayerHP", 3);
		collectiblePoints = PlayerPrefs.GetInt("PlayerScore", 0);
	}

	public void increaseScore() {
		collectiblePoints += 5;
		int leafCount = PlayerPrefs.GetInt("DiaryLeaf");
		PlayerPrefs.SetInt("DiaryLeaf", leafCount + 5);
	}

	public void resetScore() {
		collectiblePoints = 0;
	}

	public void endOfTheStage() {
		Debug.Log("The Stage is finished, congratulations !");
		if (collectiblePoints >= 25) {
			collectiblePoints = 0;
			if (SceneManager.GetActiveScene().buildIndex + 1 < SceneManager.sceneCountInBuildSettings) {
				if (SceneManager.GetActiveScene().buildIndex <= 3)
					SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
				else
					SceneManager.LoadScene("MainMenu");
				for (int i = 0; i < 7; i++) 
					PlayerPrefs.SetInt("CollectibleKey_" + i, 1);
				int lastStageUnlock = PlayerPrefs.GetInt("LastStageUnlockKey");
				if (lastStageUnlock != 3) {
					PlayerPrefs.SetInt("LastStageUnlockKey", lastStageUnlock + 1);
					PlayerPrefs.Save();
				}
			}
			else
				SceneManager.LoadScene(0);
		}
		else
			StartCoroutine(displayLeafSufficientText());
	}

	public void decreaseHP(UInt16 dmg) {
		hp -= dmg;
		_playerScript.HandleDeathAndHit();
		if (hp <= 0) {
			int deathCount = PlayerPrefs.GetInt("DeathKey");
			PlayerPrefs.SetInt("DeathKey", deathCount + 1);
			isDead = true;
		}
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
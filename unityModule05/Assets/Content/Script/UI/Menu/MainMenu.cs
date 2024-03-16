using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
	public void NewGame() {
		PlayerPrefs.SetInt("PlayerHP", 3);
        PlayerPrefs.SetInt("PlayerScore", 0);
		PlayerPrefs.SetInt("LastStageUnlockKey", 1);
		PlayerPrefs.SetInt("DeathKey", 0);
		PlayerPrefs.SetInt("DiaryLeaf", 0);
		for (int i = 0; i < 7; i++)
			PlayerPrefs.SetInt("CollectibleKey_" + i, 1);
		SceneManager.LoadScene("Stage1");
	}

	public void Resume() {
		if (UserProfile.Instance == null)
			return ;
		UserProfile.Instance.LoadUserProfile();
		int lastStage = PlayerPrefs.GetInt("LastStageUnlockKey", 1);
		if (lastStage == 1)
			SceneManager.LoadScene("Stage1");
		else if (lastStage == 2)
			SceneManager.LoadScene("Stage2");
		else if (lastStage == 3)
			SceneManager.LoadScene("Stage3");
		else
			SceneManager.LoadScene("MainMenu");
	}

	public void Diary() {
		SceneManager.LoadScene("Diary");
	}
}
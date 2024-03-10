using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
	public static bool gameIsPaused = false;
	public GameObject pauseMenuUI;
	public GameObject ConfirmationQuitUI;
    void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
			if (gameIsPaused) {
				resume();
			}
			else {
				pause();
			}
		}
    }

	public void resume() {
		pauseMenuUI.SetActive(false);
		Time.timeScale = 1f;
		gameIsPaused = false;
	}

	void pause() {
		pauseMenuUI.SetActive(true);
		Time.timeScale = 0f;
		gameIsPaused = true;
	}

	public void quitButton() {
		pauseMenuUI.SetActive(false);
		ConfirmationQuitUI.SetActive(true);
	}

	public void noButton() {
		ConfirmationQuitUI.SetActive(false);
		pauseMenuUI.SetActive(true);
	}

	public void yesButton() {
		Time.timeScale = 1f;
		SceneManager.LoadScene(0);
	}
}

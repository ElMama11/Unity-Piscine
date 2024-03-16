using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine;

public class DiaryMenu : MonoBehaviour
{
	public TextMeshProUGUI TextLeaf;
	public TextMeshProUGUI TextDeath;
	public TextMeshProUGUI TextStage1;
	public TextMeshProUGUI TextStage2;
	public TextMeshProUGUI TextStage3;

	void Start() {
		int LastStageUnlock = PlayerPrefs.GetInt("LastStageUnlockKey", 1);
	}

    void Update() {
		int DeathCount = PlayerPrefs.GetInt("DeathKey", 0);
		int LeafCount = PlayerPrefs.GetInt("DiaryLeaf", 0);
		int LastStageUnlock = PlayerPrefs.GetInt("LastStageUnlockKey", 1);
		TextLeaf.text = "Score : " + LeafCount;
		TextDeath.text = "Death : " + DeathCount;
		if (LastStageUnlock == 1) {
			TextStage1.text = "Stage 1 unlock";
			TextStage2.text = "Stage 2 lock";
			TextStage3.text = "Stage 3 lock";
		}
		else if (LastStageUnlock == 2) {
			TextStage1.text = "Stage 1 unlock";
			TextStage2.text = "Stage 2 unlock";
			TextStage3.text = "Stage 3 lock";
		}
		else if (LastStageUnlock >= 3) {
			TextStage1.text = "Stage 1 unlock";
			TextStage2.text = "Stage 2 unlock";
			TextStage3.text = "Stage 3 unlock";
		}
    }

	public void Return() {
		SceneManager.LoadScene("MainMenu");
	}
}

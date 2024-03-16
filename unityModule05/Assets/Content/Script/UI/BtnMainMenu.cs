using UnityEngine.SceneManagement;
using UnityEngine;

public class BtnMainMenu : MonoBehaviour {
    public void BtnMain() {
		UserProfile.Instance.SaveUserProfile();
        SceneManager.LoadScene(0);
    }
}

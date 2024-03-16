using UnityEngine;

public class UserProfile : MonoBehaviour
{
  private static UserProfile _instance;
	public static UserProfile Instance {
		get {
			if (_instance == null)
				Debug.LogError("User profil is null");
			return _instance;
		}
	}

    private int hp;
    private int score;
	private int lastStageUnlock;
	private int collectible;
    private const string PlayerHpKey = "PlayerHP";
    private const string PlayerScoreKey = "PlayerScore";
	private const string LastStageUnlockKey = "LastStageUnlockKey";
	private const string Collectible_Key = "CollectibleKey_";
	private const string DeathKey = "DeathKey";
	private const string DiaryLeafKey = "DiaryLeaf";

    private void Awake() {
        _instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start() {
        LoadUserProfile();
    }

    // Save user profile data
    public void SaveUserProfile() {
		hp = GameManager.Instance.hp;
		score = GameManager.Instance.collectiblePoints;
        PlayerPrefs.SetInt(PlayerHpKey, hp);
        PlayerPrefs.SetInt(PlayerScoreKey, score);
        PlayerPrefs.Save();
    }

    // Load user profile data
    public void LoadUserProfile() {
        hp = PlayerPrefs.GetInt(PlayerHpKey, 3);
        score = PlayerPrefs.GetInt(PlayerScoreKey, 0);
		lastStageUnlock = PlayerPrefs.GetInt(LastStageUnlockKey, 50);
	}

    public int Hp {
        get { return hp; }
        set
        {
            hp = value;
            SaveUserProfile();
        }
    }

    public int Score {
        get { return score; }
        set
        {
            score = value;
            SaveUserProfile();
        }
    }

	    public int LastStageUnlock {
        get { return score; }
        set
        {
            lastStageUnlock = value;
            SaveUserProfile();
        }
    }
}

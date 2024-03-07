using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject thomasPrefab;
    public GameObject johnPrefab;
    public GameObject clairePrefab;
    public static GameObject thomas;
    public static GameObject john;
    public static GameObject claire;

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "Stage2") {
            Instantiate(thomasPrefab, GetSpawnPosition("Thomas"), Quaternion.identity);
            Instantiate(johnPrefab, GetSpawnPosition("John"), Quaternion.identity);
            Instantiate(clairePrefab, GetSpawnPosition("Claire"), Quaternion.identity);
        }
    }
    
    Vector3 GetSpawnPosition(string character)
    {
        return new Vector3(PlayerPrefs.GetFloat(character + "X"), PlayerPrefs.GetFloat(character + "Y"), PlayerPrefs.GetFloat(character + "Z"));
    }

    public static void SetCharacterReferences(GameObject thomasObj, GameObject johnObj, GameObject claireObj)
    {
        thomas = thomasObj;
        john = johnObj;
        claire = claireObj;
    }
}

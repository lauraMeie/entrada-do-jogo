using UnityEngine;
using UnityEngine.SceneManagement;

public class UiManager : MonoBehaviour
{
    public static UiManager Instance { get; private set; }

    [SerializeField] private GameObject HudPrefab;
    [SerializeField] private string[] ScenesWithoutHUD;
    private GameObject currentHUD;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (System.Array.Exists(ScenesWithoutHUD, element => element.ToLower().Normalize() == scene.name.ToLower().Normalize()))
        {
            DisableHUD();
        }
        else
        {
            EnableHUD();
        }
    }

    private void EnableHUD()
    {
        if (currentHUD == null)
        {
            currentHUD = Instantiate(HudPrefab);
        }
        else
        {
            currentHUD.SetActive(true);
        }
    }

    private void DisableHUD()
    {
        if (currentHUD != null)
        {
            currentHUD.SetActive(false);
        }
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}

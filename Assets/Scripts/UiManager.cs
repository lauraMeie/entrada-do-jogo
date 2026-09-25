using UnityEngine;
using UnityEngine.SceneManagement;

public class UiManager : MonoBehaviour
{
    public static UiManager Instance { get; private set; }

    [SerializeField] private GameObject hudPrefab;
    [SerializeField] private string[] scenesWithoutHUD = { "Entrada do Jogo.", "cenário2", "GameOver" };
    private int hudOrderInLayer;

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
        if (System.Array.Exists(scenesWithoutHUD, element => element == scene.name.ToLower()))
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
            currentHUD = Instantiate(hudPrefab);
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

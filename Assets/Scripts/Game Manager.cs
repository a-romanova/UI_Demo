using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [Header("Scenes")]
    [SerializeField] private string mainMenuScene = "MainMenu";

    [SerializeField] private string inventoryScene = "Inventory";

    [Header("Transition Settings")]
    [SerializeField] private float fadeDuration = 0.5f;

    [SerializeField] private CanvasGroup fadeOverlay;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        LoadMainMenu();
    }

    /// <summary>
    /// Публичный метод для загрузки сцены по имени
    /// </summary>
    public void LoadScene(string sceneName)
    {
        if (string.IsNullOrEmpty(sceneName))
        {
            Debug.LogError("GameManager: Scene name is empty!");
            return;
        }

        StartCoroutine(SwitchScene(sceneName));
    }

    public void LoadInventory()
    {
        StartCoroutine(SwitchScene(inventoryScene));
    }

    public void LoadMainMenu()
    {
        StartCoroutine(SwitchScene(mainMenuScene));
    }

    private IEnumerator SwitchScene(string sceneName)
    {
        // 1. Затемнение
        yield return StartCoroutine(FadeOut());

        // 2. Выгружаем текущие игровые сцены (кроме Bootstrap)
        for (int i = 1; i < SceneManager.sceneCount; i++)
        {
            Scene scene = SceneManager.GetSceneAt(i);
            if (scene.isLoaded)
            {
                yield return SceneManager.UnloadSceneAsync(scene);
            }
        }

        // 3. Загружаем новую сцену аддитивно
        yield return SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);

        // 4. Проявление
        yield return StartCoroutine(FadeIn());
    }

    private IEnumerator FadeOut()
    {
        if (fadeOverlay == null) yield break;

        float elapsed = 0f;
        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            fadeOverlay.alpha = Mathf.Clamp01(elapsed / fadeDuration);
            yield return null;
        }
        fadeOverlay.alpha = 1f;
    }

    private IEnumerator FadeIn()
    {
        if (fadeOverlay == null) yield break;

        float elapsed = 0f;
        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            fadeOverlay.alpha = 1f - Mathf.Clamp01(elapsed / fadeDuration);
            yield return null;
        }
        fadeOverlay.alpha = 0f;
    }
}
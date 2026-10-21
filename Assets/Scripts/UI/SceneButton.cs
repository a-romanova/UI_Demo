using UnityEngine;
using UnityEngine.UI;

public class SceneButton : MonoBehaviour
{
    [Header("Target Scene")]
    [SerializeField] private string targetSceneName;

    [Header("Optional: Loading Screen")]
    [SerializeField] private bool showLoadingScreen = false;

    private Button _button;

    private void Awake()
    {
        _button = GetComponent<Button>();

        if (_button == null)
        {
            Debug.LogError($"SceneButton: Button component not found on {gameObject.name}");
            return;
        }

        _button.onClick.AddListener(OnButtonClick);
    }

    private void OnButtonClick()
    {
        if (string.IsNullOrEmpty(targetSceneName))
        {
            Debug.LogError($"SceneButton: Target scene name is empty on {gameObject.name}");
            return;
        }

        if (GameManager.Instance == null)
        {
            Debug.LogError("SceneButton: GameManager.Instance is null!");
            return;
        }

        // Вызываем загрузку сцены
        GameManager.Instance.LoadScene(targetSceneName);
    }

    private void OnDestroy()
    {
        if (_button != null)
            _button.onClick.RemoveListener(OnButtonClick);
    }
}
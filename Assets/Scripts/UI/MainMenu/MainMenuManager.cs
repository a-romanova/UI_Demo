using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
    [Header("Buttons")]
    [SerializeField] private MenuAnimator[] menuButtons;

    [Header("Logo")]
    [SerializeField] private RectTransform logoTransform;

    [SerializeField] private float logoFadeDuration = 0.5f;

    private CanvasGroup _logoCanvasGroup;

    private void Awake()
    {
        if (logoTransform != null)
        {
            _logoCanvasGroup = logoTransform.GetComponent<CanvasGroup>();
            if (_logoCanvasGroup == null)
                _logoCanvasGroup = logoTransform.gameObject.AddComponent<CanvasGroup>();

            _logoCanvasGroup.alpha = 0;
        }
    }

    private void Start()
    {
        if (logoTransform != null)
        {
            StartCoroutine(AnimateLogo());
        }

        for (int i = 0; i < menuButtons.Length; i++)
        {
            float delay = 0.3f + (i * 0.1f);
            menuButtons[i].AnimateShow(delay);
        }
    }

    private System.Collections.IEnumerator AnimateLogo()
    {
        yield return new WaitForSeconds(0.1f);

        float elapsed = 0f;

        while (elapsed < logoFadeDuration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / logoFadeDuration);

            _logoCanvasGroup.alpha = t;

            yield return null;
        }

        _logoCanvasGroup.alpha = 1f;
    }
}
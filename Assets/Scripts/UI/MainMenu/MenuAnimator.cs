using UnityEngine;

public class MenuAnimator : MonoBehaviour
{
    [Header("Animation Settings")]
    [SerializeField] private float startDelay = 0f;

    [SerializeField] private float animationDuration = 0.3f;

    [SerializeField] private float startScale = 0.95f;
    [SerializeField] private float endScale = 1f;

    private CanvasGroup _canvasGroup;
    private Vector3 _originalScale;
    private bool _isAnimated = false;

    [Header("Hover Effect")]
    [SerializeField] private float hoverScale = 1.05f;

    [SerializeField] private float hoverDuration = 0.15f;

    private Vector3 _targetScale;
    private bool _isHovering = false;

    private void Awake()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
        _originalScale = transform.localScale;

        if (_canvasGroup != null)
            _canvasGroup.alpha = 0;

        transform.localScale = _originalScale * startScale;
    }

    public void AnimateShow(float delay)
    {
        if (_isAnimated) return;
        _isAnimated = true;

        StartCoroutine(AnimateFadeIn(delay));
    }

    private System.Collections.IEnumerator AnimateFadeIn(float delay)
    {
        yield return new WaitForSeconds(delay);

        float elapsed = 0f;
        Vector3 startScaleVector = _originalScale * startScale;
        Vector3 endScaleVector = _originalScale * endScale;

        while (elapsed < animationDuration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / animationDuration);
            t = EaseOutBack(t);

            if (_canvasGroup != null)
                _canvasGroup.alpha = t;

            transform.localScale = Vector3.Lerp(startScaleVector, endScaleVector, t);

            yield return null;
        }

        if (_canvasGroup != null)
            _canvasGroup.alpha = 1f;

        transform.localScale = endScaleVector;
    }

    private float EaseOutBack(float t)
    {
        float s = 1.70158f;
        t = t - 1f;
        return t * t * ((s + 1f) * t + s) + 1f;
    }

    public void OnButtonHover(bool isHovering)
    {
        _isHovering = isHovering;
        _targetScale = isHovering ? _originalScale * hoverScale : _originalScale;

        StopCoroutine(nameof(AnimateHover));
        StartCoroutine(AnimateHover());
    }

    private System.Collections.IEnumerator AnimateHover()
    {
        float elapsed = 0f;
        Vector3 currentScale = transform.localScale;

        while (elapsed < hoverDuration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / hoverDuration);

            transform.localScale = Vector3.Lerp(currentScale, _targetScale, t);

            yield return null;
        }

        transform.localScale = _targetScale;
    }
}
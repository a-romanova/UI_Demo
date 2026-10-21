using UnityEngine;
using DG.Tweening;
using System.Threading.Tasks;

public class MainMenuAnimator_DOTween : MonoBehaviour
{
    [Header("Buttons")]
    [SerializeField] private CanvasGroup[] menuButtons;

    [Header("Buttons Animation Settings")]
    [SerializeField] private float startDelay = 0f;

    [SerializeField] private float animationDuration = 0.3f;

    [SerializeField] private float startButtonScale = 0.95f;
    [SerializeField] private float endButtonScale = 1f;

    [Header("Logo")]
    [SerializeField] private RectTransform logoTransform;

    [SerializeField] private float logoFadeDuration = 0.5f;

    private CanvasGroup _logoCanvasGroup;

    private void Awake()
    {
        SetLogoCanvasGroup();
        SetButtonsStartState();
    }

    private void Start()
    {
        AnimateLogo().OnComplete(() => { AnimateButtons(); });
    }

    private void SetLogoCanvasGroup()
    {
        if (logoTransform != null)
        {
            _logoCanvasGroup = logoTransform.GetComponent<CanvasGroup>();
            if (_logoCanvasGroup == null)
                _logoCanvasGroup = logoTransform.gameObject.AddComponent<CanvasGroup>();

            _logoCanvasGroup.alpha = 0;
        }
    }

    private void SetButtonsStartState()
    {
        foreach (var bttn in menuButtons)
        {
            bttn.alpha = 0;
            var startScale = bttn.gameObject.transform.localScale * startButtonScale;
            bttn.transform.localScale = startScale;
        }
    }

    private Tween AnimateLogo()
    {
        return _logoCanvasGroup.DOFade(1, logoFadeDuration);
    }

    private Sequence AnimateButtons()
    {
        var buttonsSequence = DOTween.Sequence();
        foreach (var bttn in menuButtons)
        {
            var transform = bttn.gameObject.transform;
            var targetScale = transform.localScale / startButtonScale * endButtonScale;

            buttonsSequence.Append(transform.DOScale(targetScale, animationDuration).SetEase(Ease.OutBack));
            buttonsSequence.Join(bttn.DOFade(1, animationDuration));
        }
        return buttonsSequence;
    }
}
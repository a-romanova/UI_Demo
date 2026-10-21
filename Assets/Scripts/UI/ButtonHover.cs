using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private MenuAnimator _menuAnimator;

    private void Awake()
    {
        _menuAnimator = GetComponent<MenuAnimator>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (_menuAnimator != null)
            _menuAnimator.OnButtonHover(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (_menuAnimator != null)
            _menuAnimator.OnButtonHover(false);
    }
}
using UnityEngine;
using UnityEngine.EventSystems;

public class SlotGridBackgroundClickHandler : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        SlotGrid.CloseAllContextMenus();
    }
}
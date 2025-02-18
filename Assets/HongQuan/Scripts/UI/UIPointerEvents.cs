using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class UIPointerEvents : MonoBehaviour,
    IPointerClickHandler, IPointerDownHandler, IPointerUpHandler, IPointerEnterHandler, IPointerExitHandler
{
    public UnityEvent<PointerEventData> onPointerEnter;
    public UnityEvent<PointerEventData> onPointerExit;
    public UnityEvent<PointerEventData> onPointerClick;
    public UnityEvent<PointerEventData> onPointerDown;
    public UnityEvent<PointerEventData> onPointerUp;
    public UnityEvent onPointerPressing;

    public void OnPointerClick(PointerEventData eventData)
    {
        onPointerClick.Invoke(eventData);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        onPointerEnter?.Invoke(eventData);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
       onPointerExit?.Invoke(eventData);
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        onPointerPressingCorotine = OnPointerPressing();
        StartCoroutine(onPointerPressingCorotine);
        onPointerDown?.Invoke(eventData);
    }

    IEnumerator onPointerPressingCorotine = null;
    public IEnumerator OnPointerPressing()
    {
        while (true)
        {
            onPointerPressing?.Invoke();
            yield return null;
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        StopCoroutine(onPointerPressingCorotine);
        onPointerUp?.Invoke(eventData);
    }

    private void OnDestroy()
    {
        onPointerClick?.RemoveAllListeners();
        onPointerEnter?.RemoveAllListeners();
        onPointerExit?.RemoveAllListeners();
        onPointerDown?.RemoveAllListeners();
        onPointerUp?.RemoveAllListeners();
        onPointerPressing?.RemoveAllListeners();
    }
}

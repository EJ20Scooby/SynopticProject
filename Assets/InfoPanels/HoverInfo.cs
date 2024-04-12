using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class HoverInfo : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public string optionName;
    //public string optionInfo;
    private float wait = 0.5f;
    public void OnPointerEnter(PointerEventData eventData)
    {
        StopAllCoroutines();
        StartCoroutine(startTimer());
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        StopAllCoroutines();
        HoverManager.OnMouseLoseFocus();
    }

    private void showMessage()
    {
        HoverManager.OnMouseHover(optionName, Input.mousePosition);
    }

    private IEnumerator startTimer()
    {
        yield return new WaitForSeconds(wait);

        showMessage();
    }
}

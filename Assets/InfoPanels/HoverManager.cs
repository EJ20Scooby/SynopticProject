using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HoverManager : MonoBehaviour
{
    public Text nameText;
    //public Text infoText;
    public RectTransform infoWindow;

    public static Action <string, Vector2>OnMouseHover;
    public static Action OnMouseLoseFocus;

    private void OnEnable()
    {
        OnMouseHover += showInfo;
        OnMouseLoseFocus += hideInfo;
    }

    private void OnDisable()
    {
        OnMouseHover -= showInfo;
        OnMouseLoseFocus -= hideInfo;
    }

    // Start is called before the first frame update
    void Start()
    {
        hideInfo();
    }    

    private void showInfo(string name, Vector2 mousePos)
    {
        nameText.text = name;
        //infoText.text = info;
        infoWindow.sizeDelta = new Vector2(nameText.preferredWidth > 200 ? 200 : nameText.preferredWidth, nameText.preferredHeight);

        infoWindow.gameObject.SetActive(true);
        infoWindow.transform.position = new Vector2(mousePos.x, mousePos.y - infoWindow.sizeDelta.y);
    }

    private void hideInfo()
    {
        nameText.text = default;
        //infoText.text = default;
        infoWindow.gameObject.SetActive(false);
    }
}

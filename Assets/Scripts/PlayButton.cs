using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using TMPro;

public class MenuButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public TMP_Text theText;

    public void OnPointerEnter(PointerEventData eventData)
    {
        theText.color = new Color(0.078f, 0.639f, 0.780f);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        theText.color = new Color(0.133f, 0.133f, 0.133f);
    }
}
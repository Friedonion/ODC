using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CardClick : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public bool clicked = false, clicking = false;
    public void OnPointerDown(PointerEventData data)
    {
        clicking = true;
    }
    public void OnPointerUp(PointerEventData data)
    {
        clicked = true;
        clicking = false;
    }
}

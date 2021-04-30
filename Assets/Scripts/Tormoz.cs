using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Tormoz : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public void OnPointerDown(PointerEventData eventData)
    {
        GameManager.instance.AddGaz(-1);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        GameManager.instance.AddGaz(0);
    }

   
}

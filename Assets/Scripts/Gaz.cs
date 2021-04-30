using UnityEngine;
using UnityEngine.EventSystems;

public class Gaz : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public void OnPointerDown(PointerEventData eventData)
    {
        GameManager.instance.AddGaz(1);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        GameManager.instance.AddGaz(0);
    }


}

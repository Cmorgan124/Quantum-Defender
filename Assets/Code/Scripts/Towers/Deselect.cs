using UnityEngine;
using UnityEngine.EventSystems;

public class Deselect : MonoBehaviour, IPointerDownHandler
{
    public void OnPointerDown(PointerEventData eventData)
    {
        MenuManager.Instance.Deselect();
    }
    

}

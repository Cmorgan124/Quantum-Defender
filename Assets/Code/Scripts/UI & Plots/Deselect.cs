using UnityEngine;
using UnityEngine.EventSystems;

//Method that allows the player to click off of the tower. 

public class Deselect : MonoBehaviour, IPointerDownHandler
{
    public void OnPointerDown(PointerEventData eventData)
    {
        SelectManager.Instance.Deselect();
    }
}

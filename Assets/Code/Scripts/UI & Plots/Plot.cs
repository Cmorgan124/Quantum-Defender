using UnityEngine;
using UnityEngine.EventSystems;

//Allows the player the click the plots to place towers.

public class Plot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
{
    [Header("Refrences")]
    [SerializeField] private SpriteRenderer sr;
    [SerializeField] private Color hoverColor;

    public GameObject placedTower = null;

    private Color startColor;

    private void Start()
    {
        startColor = sr.color;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        sr.color = hoverColor;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        sr.color = startColor;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        BuildManager.Instance.PlaceTower(this);
    }
}

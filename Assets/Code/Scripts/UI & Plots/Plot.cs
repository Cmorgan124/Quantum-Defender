using UnityEngine;
using UnityEngine.EventSystems;

//Allows the player the click the plots to place towers.

public class Plot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
{
    [Header("Refrences")]
    [SerializeField] private SpriteRenderer sr;
    [SerializeField] private Color hoverColor;

    private GameObject towerObj;
    public Turret turret;
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
        if (SelectManager.Instance != null && SelectManager.Instance.IsMovingTower)
        {
            SelectManager.Instance.CompleteMove(transform.position);
            return; 
        }

        if (towerObj != null)
        {
            return;
        }

        Tower towerToBuild = BuildManager.Instance.GetSelectedTower();

        if(towerToBuild.cost > LevelManager.Instance.currency)
        {
            Debug.Log("Can't afford");
            return;
        }

        LevelManager.Instance.SpendCurrency(towerToBuild.cost);

        towerObj = Instantiate(towerToBuild.prefab, transform.position, Quaternion.identity);
        turret = towerObj.GetComponent<Turret>();
    }
}

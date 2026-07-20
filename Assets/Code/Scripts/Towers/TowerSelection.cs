using System;
using UnityEngine;
using UnityEngine.EventSystems;

//Highlights the tower you are hovering over and procs the selected tower logic. 

public class TowerSelection : MonoBehaviour, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler
{
    [Header("Refrences")]

    [SerializeField] private Material outline;
    [SerializeField] private Renderer _renderer;
    [SerializeField] private TowerData parentfab;

    private Material defaultMaterial;

    void Start()
    {
        parentfab = GetComponentInParent<TowerData>();
        defaultMaterial = _renderer.material;

    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        _renderer.material = outline;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _renderer.material = defaultMaterial;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if(parentfab != null)
        {
            SelectManager.Instance.SelectTower(parentfab);        
        }

    }

}

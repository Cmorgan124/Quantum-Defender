using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class TurretSelection : MonoBehaviour, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler
{
    [Header("Refrences")]

    [SerializeField] private Material outline;
    [SerializeField] private Material normal;
    [SerializeField] private Renderer _renderer;
    [SerializeField] private TurretData parentfab;

    public void OnPointerEnter(PointerEventData eventData)
    {
        _renderer.material = outline;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _renderer.material = normal;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
            MenuManager.Instance.SelectTurret(parentfab);
    }

}
